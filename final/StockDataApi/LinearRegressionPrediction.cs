public class LinearRegressionPrediction : StockCalculation
{
    private double _predictedStockPrice;
    private List<int> _timePeriods = new List<int>();

    private int _totalPeriods;
    private double _regressionSlope;
    private double _regressionIntercept;

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
        if (_dataPoints.Count < _totalPeriods)
        {
            Console.WriteLine("Not enough data points to perform linear regression prediction.");
            return;
        }
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