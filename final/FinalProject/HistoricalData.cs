using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Threading;

public class HistoricalData
{

    private const string ApiBaseUrl = "https://www.alphavantage.co/query";
    private readonly string _apiKey;
    private const string databaseConnection = "Data Source=StocksData.db;Version=3";

    public void ExtractDataFromDatabase()
    {
        using (var connection = new SQLiteConnection(databaseConnection))
        {
            connection.Open();

            string query = "SELECT Symbol, Timestamp,  Open, High, Low, Close, Volume FROM table_name WHERE condition;";
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var value1 = reader["Symbol"];
                    var value2 = reader["Timestamp"];
                    var value3 = reader["Open"];
                    var value4 = reader["High"];
                    var value5 = reader["Low"];
                    var value6 = reader["Close"];
                    var value7 = reader["Volume"];

                    Console.WriteLine($"Value1: {value1}, Value2: {value2}");
                }
            }
        }

    }
    public HistoricalData(string apiKey)
    {
        _apiKey = apiKey;
    }


    public async Task FetchAndStoreDataForSymbolsAsync(List<string> symbols, string databasePath)
    {
        int requestCount = 0; // Tracks the number of requests made in a day

        foreach (var symbol in symbols)
        {
            if (requestCount >= 500)
            {
                Console.WriteLine("Daily request limit reached. Stopping further requests.");
                break;
            }

            Console.WriteLine($"Fetching data for {symbol}...");
            await FetchAndStoreFullIntradayDataAsync(symbol, databasePath);

            requestCount++;
            if (requestCount % 5 == 0)
            {
                Console.WriteLine("Reached 5 requests, waiting for 1 minute to respect API limits...");
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(12));
            }
        }
    }

    public async Task FetchAndStoreFullIntradayDataAsync(string symbol, string databasePath, bool fetchFull = true, string month = null)
    {
        string interval = "5min"; // ADJUST JUST IN CASE TO NOT EXCEED 500 PER DAY (MAYBE 15min)
        string adjusted = "true"; // Use "false" for raw as-traded data
        string extendedHours = "true"; // Include pre- and post-market data

        string url = $"{ApiBaseUrl}?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={interval}&apikey={_apiKey}&adjusted={adjusted}&extended_hours={extendedHours}";

        if (fetchFull) url += "&outputsize=full";
        if (!string.IsNullOrEmpty(month)) url += $"&month={month}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonData = JsonDocument.Parse(jsonResponse);

                // Validate JSON structure
                if (!jsonData.RootElement.TryGetProperty($"Time Series ({interval})", out var timeSeries))
                {
                    Console.WriteLine($"No valid data found for symbol {symbol}. Response might be malformed.");
                    Console.WriteLine($"API Response: {jsonResponse}");
                    return;
                }

                using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
                {
                    connection.Open();

                    foreach (var record in timeSeries.EnumerateObject())
                    {
                        string timestamp = record.Name;
                        var stockData = record.Value;

                        try
                        {
                            string insertQuery = @"
                        INSERT OR REPLACE INTO StockData (Symbol, Timestamp, Open, High, Low, Close, Volume)
                        VALUES (@Symbol, @Timestamp, @Open, @High, @Low, @Close, @Volume);";

                            using (var command = new SQLiteCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Symbol", symbol);
                                command.Parameters.AddWithValue("@Timestamp", timestamp);
                                command.Parameters.AddWithValue("@Open", ParseDecimal(stockData.GetProperty("1. open").GetString()));
                                command.Parameters.AddWithValue("@High", ParseDecimal(stockData.GetProperty("2. high").GetString()));
                                command.Parameters.AddWithValue("@Low", ParseDecimal(stockData.GetProperty("3. low").GetString()));
                                command.Parameters.AddWithValue("@Close", ParseDecimal(stockData.GetProperty("4. close").GetString()));
                                command.Parameters.AddWithValue("@Volume", ParseLong(stockData.GetProperty("5. volume").GetString()));

                                command.ExecuteNonQuery();
                            }

                            Console.WriteLine($"Inserted data for {symbol} at {timestamp}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error inserting data for {symbol} at {timestamp}: {ex.Message}");
                        }
                    }
                }

                Console.WriteLine($"Full intraday data for {symbol} has been successfully fetched and stored.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching data for {symbol}: {ex.Message}");
        }
    }


    private decimal ParseDecimal(string value)
    {
        return decimal.TryParse(value, out var result) ? result : 0m;
    }

    private long ParseLong(string value)
    {
        return long.TryParse(value, out var result) ? result : 0L;
    }


}