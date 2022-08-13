using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var sum = 0.0;
			var limitedQueue = new Queue<double>();
			foreach (var point in data)
            {
				sum += point.OriginalY;
				limitedQueue.Enqueue(point.OriginalY);
				if (limitedQueue.Count > windowWidth) sum -= limitedQueue.Dequeue();
				yield return point.WithAvgSmoothedY(sum / limitedQueue.Count);
            }
		}
	}
}