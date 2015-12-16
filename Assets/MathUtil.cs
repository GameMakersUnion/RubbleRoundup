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
			Vector3 start = BezierInterpolation(t, ref points, 1, (uint)(points.Length - 1));
			Vector3 end = BezierInterpolation(t, ref points, 0, (uint)(points.Length - 2));

			return Vector3.Lerp(start, end, t);
		}
	}

	private static Vector3 BezierInterpolation(float t, ref Vector3[] points, uint first, uint last)
	{
		if(first > last)
		{
			throw new UnityException("MathUtil.BezierInterpolation: first cannot be greater than last!");
		}
		else if(first == last)
		{
			return points[first];
		}

		Vector3 start = BezierInterpolation(t, ref points, first + 1, last);
		Vector3 end = BezierInterpolation(t, ref points, first, last - 1);

		return Vector3.Lerp(start, end, t);
	}
}
