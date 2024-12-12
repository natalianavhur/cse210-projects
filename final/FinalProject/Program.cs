using System;
using System.Collections.Generic;
using System.IO;
using OxyPlot;
using OxyPlot.ImageSharp;

public class Program
{
    public static void Main()
    {
        Market market = new Market();

        var stock1 = new Stock("AAPL", "Apple Inc.")
        {
            Timestamp = DateTime.Now,
            Open = 150.00,
            High = 155.00,
            Low = 149.00,
            Close = 152.00,
            Volume = 1000000
        };

        market.AddStock(stock1);

        var stock2 = new Stock("GOOGL", "Alphabet Inc.")
        {
            Timestamp = DateTime.Now.AddDays(-1),
            Open = 2700.00,
            High = 2750.00,
            Low = 2680.00,
            Close = 2730.00,
            Volume = 500000
        };

        market.AddStock(stock2);

        ChartManager chartManager = new ConcreteChartManager();

        PlotModel stockPriceChart = chartManager.CreateStockPriceChart(market, "AAPL", "Apple Stock Prices");

        var pngExporter = new PngExporter(800, 600, 96);
        using (var stream = File.Create("StockPriceChart.png"))
        {
            pngExporter.Export(stockPriceChart, stream);
        }

        Console.WriteLine("Chart saved as StockPriceChart.png");
    }
}

public class ConcreteChartManager : ChartManager
{
}






// using System;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Main(string[] args)
//     {
//         string apiKey = Environment.GetEnvironmentVariable("STOCKS_APIKEY"); //REMEMBER TO USE FILES AND NOT INSERT IT DIRECTLY.
//         string databasePath = "StocksData.db";

//         DatabaseInitializer dbInitializer = new DatabaseInitializer();
//         dbInitializer.InitializeDatabase(databasePath);

//         HistoricalData dataFetcher = new HistoricalData(apiKey);

//         string[] symbols = { "IBM", "AAPL", "MSFT", "GOOGL", "TSLA" };

//         foreach (string symbol in symbols)
//         {
//             Console.WriteLine($"Fetching data for {symbol}...");

//             await dataFetcher.FetchAndStoreFullIntradayDataAsync(symbol, databasePath, fetchFull: true);

//             await Task.Delay(12000);
//         }

//         Console.WriteLine("Data fetching and storing completed for all symbols.");
//     }
// }