using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			double previousY = 0;
			bool isFirst = true;
			foreach (var point in data)
            {
				if (isFirst)
                {
					yield return point.WithExpSmoothedY(point.OriginalY);
					isFirst = !isFirst;
					previousY = point.OriginalY;
					continue;
                }
				var newY = SmoothY(point.OriginalY, previousY, alpha);
				yield return point.WithExpSmoothedY(newY);
				previousY = newY;
            }
		}

		public static double SmoothY(double y, double previousY, double alpha)
        {
			return previousY + alpha * (y - previousY);
        }
	}
}