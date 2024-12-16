using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        string apiKey = "YOUR_API_KEY";
        HistoricalData historicalData = new HistoricalData(apiKey);

        Dictionary<string, List<double>> stockData = historicalData.ExtractDataFromDatabase();

        // List<string> symbols = new List<string>
        // {
        //     "AAPL", "MSFT", "AMZN", "GOOGL", "FB", "TSLA", "BRK.B", "NVDA", "JNJ", "JPM",
        //     "V", "PG", "UNH", "HD", "DIS", "MA", "PYPL", "NFLX", "INTC", "KO",
        //     "PEP", "CSCO", "VZ", "PFE", "MRK", "T", "ABT", "ABBV", "BMY", "CRM",
        //     "WMT", "HD", "MCD", "NKE", "ORCL", "DELL", "HPQ", "IBM", "GE", "GM"
        // };

        // historicalData.FetchAndStoreDataForSymbolsAsync(symbols, "StocksData.db").Wait();

        string[] selectedSymbols = new string[] { "AAPL", "MSFT" };

        foreach (var symbol in selectedSymbols)
        {
            if (stockData.ContainsKey(symbol))
            {
                List<double> closingPrices = stockData[symbol];

                Console.WriteLine($"\nCalculations for stock: {symbol}");

                if (closingPrices.Count >= 5)
                {
                    MovingAverages movingAverages = new MovingAverages(closingPrices, period: 5, numPeriods: 3);
                    Console.WriteLine("Moving Averages Calculation:");
                    movingAverages.PerformCalculation();

                    NextPrice nextPrice = new NextPrice(closingPrices);
                    Console.WriteLine("Next Price Calculation:");
                    nextPrice.PerformCalculation();

                    Trend trend = new Trend(closingPrices);
                    Console.WriteLine("Trend Calculation:");
                    trend.PerformCalculation();
                    Console.WriteLine($"Trend Direction: {trend.GetTrendDirection()}");

                    double openingPrice = 100;
                    double closingPrice = 110;
                    double stocksNumber = 1000;

                    DailyReturns dailyReturns = new DailyReturns(closingPrices, openingPrice, closingPrice, stocksNumber);
                    Console.WriteLine("Daily Returns Calculation:");
                    dailyReturns.PerformCalculation();
                    Console.WriteLine("Daily Returns: " + string.Join(", ", dailyReturns.GetDailyReturns()));
                    Console.WriteLine("Daily Return Percentages: " + string.Join(", ", dailyReturns.GetDailyReturnPercentages()));

                    int totalPeriods = closingPrices.Count;
                    LinearRegressionPrediction linearRegression = new LinearRegressionPrediction(closingPrices, totalPeriods);
                    Console.WriteLine("Linear Regression Prediction Calculation:");
                    linearRegression.PerformCalculation();
                }
                else
                {
                    Console.WriteLine($"Not enough data points to perform calculations for {symbol}.");
                }
            }
            else
            {
                Console.WriteLine($"Stock data for {symbol} not found in database.");
            }
        }
    }
}