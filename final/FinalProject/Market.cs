using System;
using System.Collections.Generic;
using System.Linq;

public class Market
{
    private List<Stock> _marketStocks;
    private HistoricalData _historicalData;

    public List<Stock> MarketStocks => _marketStocks;

    public Market(HistoricalData historicalData)
    {
        _marketStocks = new List<Stock>();
        _historicalData = historicalData;
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
            Console.WriteLine($"{stock.Symbol}\t{stock.CompanyName}\t{stock.Price:C}\t{stock.Volume}");
        }
    }

    public List<double> CalculateDailyReturns(string symbol, List<double> stockPrices, double openingPrice, double closingPrice, double stocksNumber)
    {
        var stock = _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            throw new ArgumentException("Stock not found.");
        }

        var dailyReturns = new DailyReturns(stockPrices, openingPrice, closingPrice, stocksNumber);
        dailyReturns.PerformCalculation();

        return dailyReturns.GetDailyReturns();
    }

    public double PredictStockPrice(string symbol, List<double> stockPrices, int totalPeriods)
    {
        var stock = _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            throw new ArgumentException("Stock not found.");
        }

        var prediction = new LinearRegressionPrediction(stockPrices, totalPeriods);
        prediction.PerformCalculation();

        return prediction.PredictedStockPrice;
    }
    public (List<double> SMA, List<double> EMA) CalculateMovingAverages(string symbol, List<double> stockPrices, int period, int numPeriods)
    {
        var stock = _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock == null)
        {
            throw new ArgumentException("Stock not found.");
        }

        var movingAverages = new MovingAverages(stockPrices, period, numPeriods);
        movingAverages.PerformCalculation();

        var sma = movingAverages.CalculateSMA();
        var ema = movingAverages.CalculateEMA(sma);

        return (sma, ema);
    }

    public Stock GetStockBySymbol(string symbol)
    {
        return _marketStocks.FirstOrDefault(s => s.Symbol == symbol);
    }

    public List<Stock> GetTopPerformers()
    {
        var topPerformers = new TopPerformers(_historicalData);
        return topPerformers.GetTopPerformers();
    }

    public double CalculateConfidence(double[] returns)
    {
        double average = returns.Average();
        double sumOfSquaresOfDifferences = returns.Select(val => (val - average) * (val - average)).Sum();
        double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / returns.Length);
        double confidence = standardDeviation / average;

        return confidence;
    }

    public double CalculateChange(double openingPrice, double closingPrice)
    {
        return closingPrice - openingPrice;
    }
}