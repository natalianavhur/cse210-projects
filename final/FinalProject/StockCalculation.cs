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
