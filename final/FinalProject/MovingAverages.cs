using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;

public class MovingAverages : StockCalculation
{
    protected int _period;
    protected int _numPeriods;

    public MovingAverages(string stockName, List<double> stockPrices, int period, int numPeriods) : base(stockName, stockPrices)
    {
        _period = period;
        _numPeriods = numPeriods;
    }

    public override void PerformCalculation()
    {
        //Simple Moving Average (SMA)


        // Exponential Moving Average (EXA)

    }
    public List<double> CalculateSMA()
    {
        List<double> simpleMovingAverage = new List<double>();
        double sma;

        if (_stockPrices.Count < _period)
        {
            throw new ArgumentException("Not enough data points to calculate Simple Moving Average.");
        }

        for (int i = 0; i <= _stockPrices.Count - _numPeriods; i++)
        {
            sma = _stockPrices.Skip(i).Take(_numPeriods).Average();
            simpleMovingAverage.Add(sma);
        }

        return simpleMovingAverage;
    }

    public List<double> CalculateEMA(List<double> simpleMovingAverage)
    {
        double smoothingFactor = 2.0 / (5 + 1);
        double ema = simpleMovingAverage[0];

        List<double> exponentialMovingAverage = new List<double>() { ema };
        for (int i = 5; i < _stockPrices.Count; i++)
        {
            ema = (_stockPrices[i] * smoothingFactor) + (ema * (1 - smoothingFactor));
            exponentialMovingAverage.Add(ema);
        }
        foreach (var value in exponentialMovingAverage)
        {
            Console.WriteLine(value);
        }

        return exponentialMovingAverage;

    }

}