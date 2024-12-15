
public class Trend : StockCalculation
{
    public Trend(List<double> dataPoints) : base(dataPoints) { }

    public override void PerformCalculation()
    {
        if (_dataPoints.Count < 2)
        {
            Console.WriteLine("Not enough data points to predict next price.");
            return;
        }

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
        double trendValue = CalculateTrendValue();
        if (trendValue > 0) return "Bullish";
        if (trendValue < 0) return "Bearish";
        return "Neutral";
    }

    private double CalculateTrendValue()
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