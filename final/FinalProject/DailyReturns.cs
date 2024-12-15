
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
        double previousPrice = _dataPoints[0];
        double dailyReturn;
        double dailyReturnPercentage;

        for (int i = 1; i < _dataPoints.Count; ++i)
        {
            dailyReturn = (_dataPoints[i] - previousPrice) * _stocksNumber;
            _dailyReturns.Add(dailyReturn);
            dailyReturnPercentage = (dailyReturn / previousPrice) * 100;
            _dailyReturnPercentages.Add(dailyReturnPercentage);
            previousPrice = _dataPoints[i];
        }

        Console.WriteLine("Daily Returns: " + string.Join(", ", _dailyReturns));
        Console.WriteLine("Daily Return Percentages: " + string.Join(", ", _dailyReturnPercentages));
    }

    public List<double> GetDailyReturns()
    {
        return _dailyReturns;
    }

    public List<double> GetDailyReturnPercentages()
    {
        return _dailyReturnPercentages;
    }
}

