// public class Trend : StockCalculation
// {
//     public Trend(List<double> dataPoints) : base(dataPoints) { }

//     public override double Calculate()
//     {
//         if (DataPoints.Count < 2)
//             throw new InvalidOperationException("Not enough data to calculate trend.");

//         double sumSlopes = 0;

//         // Calculate slopes between consecutive data points
//         for (int i = 1; i < DataPoints.Count; i++)
//         {
//             sumSlopes += DataPoints[i] - DataPoints[i - 1];
//         }

//         // Return an aggregated trend value (positive = bullish, negative = bearish)
//         return sumSlopes / (DataPoints.Count - 1);
//     }

//     // Optional helper method for interpreting trend
//     public string GetTrendDirection()
//     {
//         double trendValue = Calculate();
//         if (trendValue > 0) return "Bullish";
//         if (trendValue < 0) return "Bearish";
//         return "Neutral";
//     }
// }
