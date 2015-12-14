using UnityEngine;
using System.Collections;
using System;

public static class MathUtil
{
	public static Vector3 BezierInterpolation(float t, params Vector3[] points)
	{
		if(points.Length == 0)
		{
			return Vector3.zero;
		}
		else if(points.Length == 1)
		{
			return points[0];
		}
		else
		{
			int elementsToCopy = points.Length - 1;
			Vector3[] points1 = new Vector3[elementsToCopy];
			Vector3[] points2 = new Vector3[elementsToCopy];
			Array.Copy(points, 1, points1, 0, elementsToCopy);
			Array.Copy(points, 0, points2, 0, elementsToCopy);

			Vector3 start = BezierInterpolation(t, points1);
			Vector3 end = BezierInterpolation(t, points2);

			return Vector3.Lerp(start, end, t);
		}
	}
}
