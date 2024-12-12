using System;
using System.Collections.Generic;
using System.Linq;

public class TopPerformers : StockCalculation
{
    protected double _predictedStockPrice;
    protected List<int> _timePeriods = new List<int>();

    public double PredictedStockPrice()
    {
        return _predictedStockPrice;
    }
    public TopPerformers(string stockName, List<double> stockPrices, int totalPeriods) : base(stockName, stockPrices)
    {
        
    }

    public override void PerformCalculation()
    {


    }
}