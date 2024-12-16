var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseCors("AllowAll");

// Function to perform calculations for a given stock
object PerformCalculations(string symbol, List<double> closingPrices)
{
    var results = new Dictionary<string, object>();

    // Perform Moving Averages calculation
    var movingAverages = new MovingAverages(closingPrices, period: 5, numPeriods: 3);
    movingAverages.PerformCalculation();
    results["MovingAverages"] = new
    {
        SMA = movingAverages.CalculateSMA(),
        EMA = movingAverages.CalculateEMA(movingAverages.CalculateSMA())
    };

    // Perform Next Price calculation
    var nextPrice = new NextPrice(closingPrices);
    nextPrice.PerformCalculation();
    results["NextPrice"] = nextPrice;

    // Perform Trend calculation
    var trend = new Trend(closingPrices);
    trend.PerformCalculation();
    results["Trend"] = new
    {
        Value = trend.CalculateTrendValue(),
        Direction = trend.GetTrendDirection()
    };

    // Assuming additional data points for opening and closing prices for DailyReturns
    double openingPrice = 100;  // Example value
    double closingPrice = 110;  // Example value
    double stocksNumber = 1000; // Example value

    // Perform Daily Returns calculation
    var dailyReturns = new DailyReturns(closingPrices, openingPrice, closingPrice, stocksNumber);
    dailyReturns.PerformCalculation();
    results["DailyReturns"] = new
    {
        Returns = dailyReturns.GetDailyReturns(),
        ReturnPercentages = dailyReturns.GetDailyReturnPercentages()
    };

    // Perform Linear Regression Prediction
    int totalPeriods = closingPrices.Count; // Example total periods
    var linearRegression = new LinearRegressionPrediction(closingPrices, totalPeriods);
    linearRegression.PerformCalculation();
    results["LinearRegressionPrediction"] = new
    {
        PredictedPrice = linearRegression.PredictedStockPrice
    };

    return results;
}

// API endpoint to get all stocks data
app.MapGet("/stocks", () =>
{
    var historicalData = new HistoricalData("YOUR_API_KEY");
    return historicalData.ExtractDataFromDatabase();
})
.WithName("GetStocks")
.WithOpenApi();

// API endpoint to get calculations for a specific stock
app.MapGet("/stocks/{symbol}", (string symbol) =>
{
    var historicalData = new HistoricalData("YOUR_API_KEY");
    var stockData = historicalData.ExtractDataFromDatabase();

    if (stockData.ContainsKey(symbol))
    {
        List<double> closingPrices = stockData[symbol];
        if (closingPrices.Count >= 5)
        {
            return PerformCalculations(symbol, closingPrices);
        }
        else
        {
            return Results.BadRequest("Not enough data points to perform calculations.");
        }
    }
    else
    {
        return Results.NotFound("Stock data not found.");
    }
})
.WithName("GetSingleStock")
.WithOpenApi();

app.Run();