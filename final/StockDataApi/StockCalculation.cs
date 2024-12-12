public abstract class StockCalculation
{
    public string _stockName;
    public List<double> _stockPrices;

    public StockCalculation(string stockName, List<double> stockPrices)
    {
        _stockName = stockName;
        _stockPrices = stockPrices;
    }

    public abstract void PerformCalculation();
}
