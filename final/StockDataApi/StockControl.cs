[ApiController]
[Route("api/[controller]")]
public class StockControl : ControllerBase
{
    private readonly List<Stock> _stocks;
    private readonly List<HistoricalData> _historicalData;

    public StockController()
    {
        // Loading sample data, in reality, this would come from your database or API
        _stocks = LoadStocks();
        _historicalData = LoadHistoricalData();
    }

    // Sample methods for loading data (replace with actual data loading logic)
    private List<Stock> LoadStocks()
    {
        return new List<Stock>
        {
            new Stock("AAPL", "Apple Inc.", 175.43, 100000, 1.25, DateTime.Now),
            new Stock("GOOGL", "Alphabet Inc.", 2750.56, 150000, 0.75, DateTime.Now),
            new Stock("MSFT", "Microsoft Corp.", 299.25, 120000, 0.85, DateTime.Now)
        };
    }

    private List<HistoricalData> LoadHistoricalData()
    {
        return new List<HistoricalData>
        {
            new HistoricalData("AAPL", DateTime.Now.AddDays(-1), 170.50, 175.00, 178.00, 169.00),
            new HistoricalData("AAPL", DateTime.Now, 175.00, 175.43, 176.00, 173.50)
        };
    }

    // Endpoint to get top performers
    [HttpGet("topperformers")]
    public IActionResult GetTopPerformers()
    {
        var topPerformers = _stocks.OrderByDescending(s => s.Change).Take(10).ToList();
        return Ok(topPerformers.Select(s => new
        {
            s.Symbol,
            s.CompanyName,
            s.Price,
            s.Volume,
            s.Change
        }));
    }

    // Endpoint to get historical data for a specific stock symbol
    [HttpGet("historicaldata/{symbol}")]
    public IActionResult GetHistoricalData(string symbol)
    {
        var historicalData = _historicalData.Where(hd => hd.Symbol == symbol).ToList();
        return Ok(historicalData.Select(hd => new
        {
            hd.Date,
            hd.Open,
            hd.Close,
            hd.High,
            hd.Low
        }));
    }

    // Endpoint to get company info (price, change, prediction, etc.)
    [HttpGet("companyinfo/{symbol}")]
    public IActionResult GetCompanyInfo(string symbol)
    {
        var stock = _stocks.FirstOrDefault(s => s.Symbol == symbol);
        if (stock != null)
        {
            var info = new
            {
                CurrentPrice = stock.Price,
                Change = stock.Change,
                Prediction = "Bullish", // You can implement prediction logic here
                PredictedNextPrice = stock.Price * 1.02, // Example prediction logic
                Confidence = 85 // Example confidence value
            };
            return Ok(info);
        }
        return NotFound();
    }

    // Endpoint to get linear regression data for a specific stock symbol
    [HttpGet("linearregression/{symbol}")]
    public IActionResult GetLinearRegression(string symbol)
    {
        var stockData = _stocks.Where(s => s.Symbol == symbol).ToList();
        // Example of linear regression (replace with actual regression logic)
        var slope = 0.5; // Example value
        var intercept = 150; // Example value
        return Ok(new { Slope = slope, Intercept = intercept });
    }
}
