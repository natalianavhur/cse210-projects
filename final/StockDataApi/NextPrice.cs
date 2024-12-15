// public class NextPrice : StockCalculation
// {
//     public CalculateNextPrice(List<double> dataPoints) : base(dataPoints) { }

//     public override double Calculate()
//     {
//         if (DataPoints.Count < 2)
//             throw new InvalidOperationException("Not enough data to predict next price.");

//         // Example using linear extrapolation: P(n+1) = 2*P(n) - P(n-1)
//         double nextPrice = 2 * DataPoints[^1] - DataPoints[^2];
//         return nextPrice;
//     }
// }
