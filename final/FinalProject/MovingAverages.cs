public class MovingAverages : StockCalculation
{
    private int _period;
    private int _numPeriods;

    public MovingAverages(List<double> dataPoints, int period, int numPeriods) : base(dataPoints)
    {
        _period = period;
        _numPeriods = numPeriods;
    }

    public override void PerformCalculation()
    {
        if (_dataPoints.Count >= _period)
        {
            List<double> sma = CalculateSMA();
            List<double> ema = CalculateEMA(sma);

            Console.WriteLine("Simple Moving Averages: " + string.Join(", ", sma));
            Console.WriteLine("Exponential Moving Averages: " + string.Join(", ", ema));
        }
        else
        {
            Console.WriteLine("Not enough data points to calculate Simple Moving Averages.");
        }
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