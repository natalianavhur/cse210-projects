
public class NextPrice : StockCalculation
{
    public NextPrice(List<double> dataPoints) : base(dataPoints) { }

    public override void PerformCalculation()
    {
        if (_dataPoints.Count < 2)
        {
            Console.WriteLine("Not enough data points to predict next price.");
            return;
        }

        // Example using linear extrapolation: P(n+1) = 2*P(n) - P(n-1)
        double nextPrice = 2 * _dataPoints[^1] - _dataPoints[^2];
        Console.WriteLine($"Predicted Next Price: {nextPrice}");
    }
}
