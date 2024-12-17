using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class HistoricalData : IDisposable
{
    private const string API_BASE_URL = "https://www.alphavantage.co/query";
    private readonly string _apiKey;
    private const string DATABASE_CONNECTION = "Data Source=StocksData.db;Version=3";

    public HistoricalData(string apiKey)
    {
        _apiKey = apiKey;
    }

    public Dictionary<string, List<Stock>> ExtractDataFromDatabase()
    {
        using (var connection = new SQLiteConnection(DATABASE_CONNECTION))
        {
            connection.Open();
            string query = "SELECT Symbol, Timestamp, Open, High, Low, Close, Volume FROM StockData LIMIT 100;";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                Dictionary<string, List<Stock>> stockData = new Dictionary<string, List<Stock>>();
                while (reader.Read())
                {
                    string symbol = reader["Symbol"].ToString();
                    var stock = new Stock(
                        symbol: symbol,
                        date: DateTime.Parse(reader["Timestamp"].ToString()),
                        open: Convert.ToDouble(reader["Open"]),
                        high: Convert.ToDouble(reader["High"]),
                        low: Convert.ToDouble(reader["Low"]),
                        close: Convert.ToDouble(reader["Close"]),
                        volume: Convert.ToInt64(reader["Volume"])
                    );

                    if (!stockData.ContainsKey(symbol))
                    {
                        stockData[symbol] = new List<Stock>();
                    }
                    stockData[symbol].Add(stock);
                }

                Console.WriteLine($"Extracted data for {stockData.Count} stocks from the database.");
                return stockData;
            }
        }
    }
    public async Task FetchAndStoreDataForSymbolsAsync(List<string> symbols, string databasePath)
    {
        int requestCount = 0;

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

    public void UpdateStockChange(string symbol, double change)
    {
        using (var connection = new SQLiteConnection(DATABASE_CONNECTION))
        {
            connection.Open();
            string query = "UPDATE StockData SET Change = @Change WHERE Symbol = @Symbol;";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Symbol", symbol);
                command.Parameters.AddWithValue("@Change", change);
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Stock> GetStocks()
    {
        using (var connection = new SQLiteConnection(DATABASE_CONNECTION))
        {
            connection.Open();
            string query = "SELECT Symbol, CompanyName, Close, Volume, Change FROM StockData;";
            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                List<Stock> stocks = new List<Stock>();
                while (reader.Read())
                {
                    string symbol = reader["Symbol"].ToString();
                    string companyName = reader["CompanyName"].ToString();
                    double close = Convert.ToDouble(reader["Close"]);
                    long volume = Convert.ToInt64(reader["Volume"]);
                    double change = Convert.ToDouble(reader["Change"]);
                    var stock = new Stock(symbol, companyName, close, volume, change, DateTime.Now);
                    stocks.Add(stock);
                }
                Console.WriteLine($"Loaded {stocks.Count} stocks from the database.");
                return stocks;
            }
        }
    }

    public async Task FetchAndStoreFullIntradayDataAsync(string symbol, string databasePath, bool fetchFull = true, string month = null)
    {
        string interval = "5min"; // Adjust to avoid exceeding API limit of requests
        string adjusted = "true";
        string extendedHours = "true";

        string url = $"{API_BASE_URL}?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={interval}&apikey={_apiKey}&adjusted={adjusted}&extended_hours={extendedHours}";

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
    public void Dispose()
    {

    }
}

