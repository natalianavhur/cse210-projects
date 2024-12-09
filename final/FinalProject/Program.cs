using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = Environment.GetEnvironmentVariable("STOCKS_APIKEY"); //REMEMBER TO USE FILES AND NOT INSERT IT DIRECTLY.
        string databasePath = "StocksData.db";

        DatabaseInitializer dbInitializer = new DatabaseInitializer();
        dbInitializer.InitializeDatabase(databasePath);

        HistoricalData dataFetcher = new HistoricalData(apiKey);

        string[] symbols = { "IBM", "AAPL", "MSFT", "GOOGL", "TSLA" };

        foreach (string symbol in symbols)
        {
            Console.WriteLine($"Fetching data for {symbol}...");

            await dataFetcher.FetchAndStoreFullIntradayDataAsync(symbol, databasePath, fetchFull: true);

            await Task.Delay(12000);
        }

        Console.WriteLine("Data fetching and storing completed for all symbols.");
    }
}