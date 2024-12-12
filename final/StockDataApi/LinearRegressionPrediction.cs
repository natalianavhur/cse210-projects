using System;
using System.Collections.Generic;
using System.Linq;

public class LinearRegressionPrediction : StockCalculation
{
    protected double _predictedStockPrice;
    protected List<int> _timePeriods = new List<int>();

    protected int _totalPeriods;
    protected double _regressionSlope;
    protected double _regressionIntercept;

    public double PredictedStockPrice()
    {
        return _predictedStockPrice;
    }
    public LinearRegressionPrediction(string stockName, List<double> stockPrices, int totalPeriods) : base(stockName, stockPrices)
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
        double averageStockPrice = _stockPrices.Average();

        for (int i = 0; i < _stockPrices.Count; ++i)
        {
            sumProductDeviations += (_timePeriods[i] - averageTimePeriod) * (_stockPrices[i] - averageStockPrice);
            sumSquaredDeviations += (_timePeriods[i] - averageTimePeriod) * (_timePeriods[i] - averageTimePeriod);
        }
        _regressionSlope = sumProductDeviations / sumSquaredDeviations;
        _regressionIntercept = averageStockPrice - _regressionSlope * averageTimePeriod;

        _predictedStockPrice = _regressionSlope * _totalPeriods + _regressionIntercept;

    }
}


// using System;
// using System.Collections.Generic;
// using System.Linq;

// public class LinearRegressionPrediction : StockCalculation
// {
//     protected double _stockPrice;
//     protected List<int> _periods = new List<int>();

//     protected int _period;
//     protected double _slope;
//     protected double _yIntercept;
//     public LinearRegressionPrediction(string stockName, List<double> stockPrices, int period) : base(stockName, stockPrices)
//     {
//         for (int i = 1; i <= period; i++)
//         {
//             _periods.Add(i);
//         }
//         _period = period;
//     }

//     public override void PerformCalculation()
//     {
//         double pricesSum = 0;
//         double periodsSum = 0;

//         double averageTimePeriod = _periods.Average();
//         double averageStockPrice = _stockPrices.Average();

//         for (int i = 0; i < _stockPrices.Count; ++i)
//         {
//             pricesSum += (_periods[i] - averageTimePeriod) * (_stockPrices[i] - averageStockPrice);
//             periodsSum += (_periods[i] - averageTimePeriod) * (_periods[i] - averageTimePeriod);
//         }
//         _slope = pricesSum / periodsSum;
//         _yIntercept = averageStockPrice - _slope * averageTimePeriod;

//         _stockPrice = _slope * _period + _yIntercept;
//     }
// }