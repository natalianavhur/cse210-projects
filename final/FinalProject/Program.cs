using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize HistoricalData with your API key
        string apiKey = "YOUR_API_KEY";
        HistoricalData historicalData = new HistoricalData(apiKey);

        // Extract data from the database
        Dictionary<string, List<double>> stockData = historicalData.ExtractDataFromDatabase();

        foreach (var stock in stockData)
        {
            string symbol = stock.Key;
            List<double> closingPrices = stock.Value;

            Console.WriteLine($"\nTesting calculations for stock: {symbol}");

            if (closingPrices.Count >= 5)
            {
                // Test MovingAverages
                MovingAverages movingAverages = new MovingAverages(closingPrices, period: 5, numPeriods: 3);
                Console.WriteLine("Moving Averages Calculation:");
                movingAverages.PerformCalculation();

                // Test NextPrice
                NextPrice nextPrice = new NextPrice(closingPrices);
                Console.WriteLine("Next Price Calculation:");
                nextPrice.PerformCalculation();

                // Test Trend
                Trend trend = new Trend(closingPrices);
                Console.WriteLine("Trend Calculation:");
                trend.PerformCalculation();
                Console.WriteLine($"Trend Direction: {trend.GetTrendDirection()}");

                // Assuming you have additional data points for opening and closing prices for DailyReturns
                double openingPrice = 100;  // Example value
                double closingPrice = 110;  // Example value
                double stocksNumber = 1000; // Example value

                // Test DailyReturns
                DailyReturns dailyReturns = new DailyReturns(closingPrices, openingPrice, closingPrice, stocksNumber);
                Console.WriteLine("Daily Returns Calculation:");
                dailyReturns.PerformCalculation();
                Console.WriteLine("Daily Returns: " + string.Join(", ", dailyReturns.GetDailyReturns()));
                Console.WriteLine("Daily Return Percentages: " + string.Join(", ", dailyReturns.GetDailyReturnPercentages()));

                // Test LinearRegressionPrediction
                int totalPeriods = closingPrices.Count; // Example total periods
                LinearRegressionPrediction linearRegression = new LinearRegressionPrediction(closingPrices, totalPeriods);
                Console.WriteLine("Linear Regression Prediction Calculation:");
                linearRegression.PerformCalculation();
            }
            else
            {
                Console.WriteLine($"Not enough data points to perform calculations for {symbol}.");
            }
        }
    }
}



// using System;
// using System.Collections.Generic;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         // Initialize HistoricalData with your API key
//         string apiKey = "YOUR_API_KEY";
//         HistoricalData historicalData = new HistoricalData(apiKey);

//         // Extract data from the database
//         Dictionary<string, List<double>> stockData = historicalData.ExtractDataFromDatabase();

//         foreach (var stock in stockData)
//         {
//             string symbol = stock.Key;
//             List<double> closingPrices = stock.Value;

//             if (closingPrices.Count >= 5)  // Ensure there are enough data points
//             {
//                 // Create an instance of MovingAverages
//                 MovingAverages movingAverages = new MovingAverages(closingPrices, period: 5, numPeriods: 3);

//                 // Perform the calculation
//                 movingAverages.PerformCalculation();
//             }
//             else
//             {
//                 Console.WriteLine($"Not enough data points to calculate Simple Moving Averages for {symbol}.");
//             }
//         }
//     }
// }

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