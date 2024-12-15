// public class Confidence : StockCalculation
// {
//     public Confidence(List<double> dataPoints) : base(dataPoints) { }

//     public override double Calculate()
//     {
//         if (DataPoints.Count < 2)
//             throw new InvalidOperationException("Not enough data to calculate confidence.");

//         // Calculate the variance of actual data
//         double mean = DataPoints.Average();
//         double variance = DataPoints.Sum(dp => Math.Pow(dp - mean, 2)) / DataPoints.Count;

//         // Calculate linear regression error (example method)
//         double regressionError = 0; // Placeholder for error calculation logic

//         // Confidence = 1 - (Error / Variance)
//         double confidence = 1 - (regressionError / variance);
//         return Math.Clamp(confidence * 100, 0, 100); // Scale to percentage
//     }
// }
