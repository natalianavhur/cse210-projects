var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/stocks", () =>
{
    var historicalData = new HistoricalData("YOUR_API_KEY");
    return historicalData.ExtractDataFromDatabase();
}
)

.WithName("GetStocks")
.WithOpenApi();

app.MapGet("/stocks/{symbol}", (string symbol) =>
{
    var historicalData = new HistoricalData("YOUR_API_KEY");
    // return historicalData.ExtractDataFromDatabase();
    // return historicalData.ExtractDataFromDatabase(symbol);
}
)

.WithName("GetSingleStock")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
