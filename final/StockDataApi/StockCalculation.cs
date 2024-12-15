using System;
using System.Collections.Generic;
using System.Linq;

public abstract class StockCalculation
{
    protected List<double> _dataPoints;
    public StockCalculation(List<double> dataPoints)
    {
        _dataPoints = dataPoints;
    }

    public abstract void PerformCalculation();
}

public class MovingAverages : StockCalculation
{
    protected int _period;
    protected int _numPeriods;

    public MovingAverages(List<double> dataPoints, int period, int numPeriods) : base(dataPoints)
    {
        _period = period;
        _numPeriods = numPeriods;
    }

    public override void PerformCalculation()
    {
        List<double> sma = CalculateSMA();
        List<double> ema = CalculateEMA(sma);

        Console.WriteLine("Simple Moving Averages: " + string.Join(", ", sma));
        Console.WriteLine("Exponential Moving Averages: " + string.Join(", ", ema));
    }

    public List<double> CalculateSMA()
    {
        List<double> simpleMovingAverage = new List<double>();
        double sma;

        if (_dataPoints.Count < _period)
        {
            throw new ArgumentException("Not enough data points to calculate Simple Moving Average.");
        }

        for (int i = 0; i <= _dataPoints.Count - _numPeriods; i++)
        {
            sma = _dataPoints.Skip(i).Take(_numPeriods).Average();
            simpleMovingAverage.Add(sma);
        }

        return simpleMovingAverage;
    }

    public List<double> CalculateEMA(List<double> simpleMovingAverage)
    {
        double smoothingFactor = 2.0 / (5 + 1);
        double ema = simpleMovingAverage[0];

        List<double> exponentialMovingAverage = new List<double>() { ema };
        for (int i = 5; i < _dataPoints.Count; i++)
        {
            ema = (_dataPoints[i] * smoothingFactor) + (ema * (1 - smoothingFactor));
            exponentialMovingAverage.Add(ema);
        }
        foreach (var value in exponentialMovingAverage)
        {
            Console.WriteLine(value);
        }

        return exponentialMovingAverage;
    }
}


public class NextPrice : StockCalculation
{
    public NextPrice(List<double> dataPoints) : base(dataPoints) { }

    public override void PerformCalculation()
    {
        if (_dataPoints.Count < 2)
            throw new InvalidOperationException("Not enough data to predict next price.");

        // Example using linear extrapolation: P(n+1) = 2*P(n) - P(n-1)
        double nextPrice = 2 * _dataPoints[^1] - _dataPoints[^2];
        Console.WriteLine($"Predicted Next Price: {nextPrice}");
    }
}

public class Trend : StockCalculation
{
    public Trend(List<double> dataPoints) : base(dataPoints) { }

    public override void PerformCalculation()
    {
        if (_dataPoints.Count < 2)
            throw new InvalidOperationException("Not enough data to calculate trend.");

        double sumSlopes = 0;

        // Calculate slopes between consecutive data points
        for (int i = 1; i < _dataPoints.Count; i++)
        {
            sumSlopes += _dataPoints[i] - _dataPoints[i - 1];
        }

        // Return an aggregated trend value (positive = bullish, negative = bearish)
        double trendValue = sumSlopes / (_dataPoints.Count - 1);
        Console.WriteLine($"Trend Value: {trendValue}");
    }

    // Optional helper method for interpreting trend
    public string GetTrendDirection()
    {
        double trendValue = Calculate();
        if (trendValue > 0) return "Bullish";
        if (trendValue < 0) return "Bearish";
        return "Neutral";
    }

    private double Calculate()
    {
        // Internal method to calculate trend value
        double sumSlopes = 0;

        for (int i = 1; i < _dataPoints.Count; i++)
        {
            sumSlopes += _dataPoints[i] - _dataPoints[i - 1];
        }

        return sumSlopes / (_dataPoints.Count - 1);
    }
}


public class DailyReturns : StockCalculation
{
    protected double _closingPrice;
    protected double _openingPrice;
    protected double _stocksNumber;
    protected List<double> _dailyReturns = new List<double>();
    protected List<double> _dailyReturnPercentages = new List<double>();

    public DailyReturns(List<double> dataPoints, double openingPrice, double closingPrice, double stocksNumber)
        : base(dataPoints)
    {
        _openingPrice = openingPrice;
        _closingPrice = closingPrice;
        _stocksNumber = stocksNumber;
    }

    public override void PerformCalculation()
    {
        double difference;
        double dailyReturn;
        double dailyReturnPercentage;

        for (int i = 0; i < _dataPoints.Count; ++i)
        {
            difference = _closingPrice - _openingPrice;
            dailyReturn = difference * _stocksNumber;

            _dailyReturns.Add(dailyReturn);
            dailyReturnPercentage = dailyReturn / _dataPoints[i] * 100;

            _dailyReturnPercentages.Add(dailyReturnPercentage);
        }
    }

    public List<double> GetDailyReturns()
    {
        return _dailyReturns;
    }
}


public class LinearRegressionPrediction : StockCalculation
{
    protected double _predictedStockPrice;
    protected List<int> _timePeriods = new List<int>();

    protected int _totalPeriods;
    protected double _regressionSlope;
    protected double _regressionIntercept;

    public double PredictedStockPrice
    {
        get { return _predictedStockPrice; }
    }

    public LinearRegressionPrediction(List<double> dataPoints, int totalPeriods) : base(dataPoints)
    {
        for (int i = 1; i <= totalPeriods; i++)
        {
            _timePeriods.Add(i);
        }
        _totalPeriods = totalPeriods;
    }

    public override void PerformCalculation()
    {
        double sumProductDeviations = 0;
        double sumSquaredDeviations = 0;

        double averageTimePeriod = _timePeriods.Average();
        double averageStockPrice = _dataPoints.Average();
   
        for (int i = 0; i < _dataPoints.Count; ++i)
        {
            sumProductDeviations += (_timePeriods[i] - averageTimePeriod) * (_dataPoints[i] - averageStockPrice);
            sumSquaredDeviations += (_timePeriods[i] - averageTimePeriod) * (_timePeriods[i] - averageTimePeriod);
        }
        _regressionSlope = sumProductDeviations / sumSquaredDeviations;
        _regressionIntercept = averageStockPrice - _regressionSlope * averageTimePeriod;

        _predictedStockPrice = _regressionSlope * _totalPeriods + _regressionIntercept;
        Console.WriteLine($"Predicted Stock Price: {_predictedStockPrice}");
    }
}