using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{

	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var limitedQueue = new Queue<double>();
			var potentialMax = new LinkedList<double>();
			foreach (var point in data)
            {
				limitedQueue.Enqueue(point.OriginalY);
				if (limitedQueue.Count > windowWidth) 
					if (potentialMax.Last.Value == limitedQueue.Dequeue())
						potentialMax.RemoveLast();				
				if (potentialMax.Count > 0 && point.OriginalY >= potentialMax.First())
					while (potentialMax.Count > 0 && potentialMax.First() < point.OriginalY)
						potentialMax.RemoveFirst();
				potentialMax.AddFirst(point.OriginalY);
				yield return point.WithMaxY(potentialMax.Last.Value);
            }
		}
	}
}