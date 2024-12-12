using System;
using System.Collections.Generic;
using System.Linq;

public class DailyReturns : StockCalculation
{
    protected double _closingPrice;
    protected double _openingPrice;
    protected double _stocksNumber;
    protected List<double> _dailyReturns = new List<double>();
    protected List<double> _dailyReturnPercentages = new List<double>();

    public DailyReturns(string stockName, List<double> stockPrices, int totalPeriods) : base(stockName, stockPrices)
    {


    }

    public override void PerformCalculation()
    {
        double difference;
        double dailyReturn;
        double dailyReturnPercentage;

        for (int i = 0; i < _stockPrices.Count; ++i)
        {
            difference = _closingPrice - _openingPrice;
            dailyReturn = difference * _stocksNumber;

            _dailyReturns.Add(dailyReturn);
            dailyReturnPercentage = dailyReturn / _stockPrices[i] * 100;

            _dailyReturnPercentages.Add(dailyReturnPercentage);
        }

    }


}