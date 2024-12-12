using System;
using System.Collections.Generic;
using System.Linq;

public class Market
{
    private List<Stock> _marketStocks;

    public Market()
    {
        _marketStocks = new List<Stock>();
    }

    public void AddStock(Stock stock)
    {
        _marketStocks.Add(stock);
    }

    public void DisplayStocks()
    {
        Console.WriteLine("Symbol\tName\tClose Price\tVolume");
        foreach (var stock in _marketStocks)
        {
            Console.WriteLine($"{stock.Symbol}\t{stock.Name}\t{stock.Close:C}\t{stock.Volume}");
        }
    }

    // Calculate Daily Returns for a stock
    public List<double> CalculateDailyReturns(string symbol, List<double> stockPrices, double openingPrice, double closingPrice, double stocksNumber)
    {
        var stock = _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            throw new ArgumentException("Stock not found.");
        }

        var dailyReturns = new DailyReturns(stock.Name, stockPrices, stockPrices.Count, openingPrice, closingPrice, stocksNumber);
        dailyReturns.PerformCalculation();

        return dailyReturns.GetDailyReturns();
    }

    // Predict stock price using Linear Regression
    public double PredictStockPrice(string symbol, List<double> stockPrices, int totalPeriods)
    {
        var stock = _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            throw new ArgumentException("Stock not found.");
        }

        var prediction = new LinearRegressionPrediction(stock.Name, stockPrices, totalPeriods);
        prediction.PerformCalculation();

        return prediction.PredictedStockPrice();
    }

    // Calculate Moving Averages for a stock
    public (List<double> SMA, List<double> EMA) CalculateMovingAverages(string symbol, List<double> stockPrices, int period, int numPeriods)
    {
        var stock = _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            throw new ArgumentException("Stock not found.");
        }

        var movingAverages = new MovingAverages(stock.Name, stockPrices, period, numPeriods);
        movingAverages.PerformCalculation();

        var sma = movingAverages.CalculateSMA();
        var ema = movingAverages.CalculateEMA(sma);

        return (sma, ema);
    }

    public Stock GetStockBySymbol(string symbol)
    {
        return _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
    }
}
