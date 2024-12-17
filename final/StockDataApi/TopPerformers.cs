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

            // Ensure there are more than 1 stock entry to calculate the change
            if (closingPrices.Count > 1)
            {
                // Calculate the change between the first and last stock in the list
                double change = closingPrices.Last().Close - closingPrices.First().Close;

                // Update the stock change value in the historical data
                _historicalData.UpdateStockChange(symbol, change);
            }
        }

        // Get the list of stocks and order them by the calculated change
        var stocks = _historicalData.GetStocks();
        return stocks.OrderByDescending(stock => stock.Change).Take(10).ToList();
    }
}