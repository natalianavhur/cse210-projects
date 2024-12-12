using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;

public abstract class ChartManager
{
    public virtual PlotModel CreateStockPriceChart(Market market, string symbol, string title = "Stock Prices")
    {
        var plotModel = new PlotModel { Title = title };

        var lineSeries = new LineSeries
        {
            Title = "Close Prices",
            MarkerType = MarkerType.Circle
        };

        var stock = market.GetStockBySymbol(symbol);
        if (stock != null)
        {
            lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(stock.Timestamp), stock.Close));
        }

        plotModel.Series.Add(lineSeries);
        return plotModel;
    }

    public virtual PlotModel CreateDailyReturnsChart(List<double> dailyReturns, string title = "Daily Returns")
    {
        var plotModel = new PlotModel { Title = title };

        var barSeries = new BarSeries
        {
            Title = "Daily Returns",
            StrokeColor = OxyColors.Black,
            StrokeThickness = 1
        };

        foreach (var dailyReturn in dailyReturns)
        {
            barSeries.Items.Add(new BarItem { Value = dailyReturn });
        }

        plotModel.Series.Add(barSeries);
        return plotModel;
    }

    public virtual void RenderTable(List<string> headers, List<List<string>> rows)
    {
        Console.WriteLine(string.Join(" | ", headers));
        Console.WriteLine(new string('-', headers.Sum(h => h.Length + 3)));

        foreach (var row in rows)
        {
            Console.WriteLine(string.Join(" | ", row));
        }
    }
}
