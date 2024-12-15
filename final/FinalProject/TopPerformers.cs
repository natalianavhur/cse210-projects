public class TopPerformers
{
    private HistoricalData _historicalData;

    public TopPerformers(HistoricalData historicalData)
    {
        _historicalData = historicalData;
    }

    public List<Stock> GetTopPerformers()
    {
        var stockData = _historicalData.ExtractDataFromDatabase();

        foreach (var symbol in stockData.Keys)
        {
            var closingPrices = stockData[symbol];
            if (closingPrices.Count > 1)
            {
                double change = closingPrices.Last() - closingPrices.First();
                _historicalData.UpdateStockChange(symbol, change);
            }
        }

        var stocks = _historicalData.GetStocks();
        return stocks.OrderByDescending(stock => stock.Change).Take(10).ToList();
    }
}
