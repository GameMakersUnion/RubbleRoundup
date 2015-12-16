using UnityEngine;
using System.Collections;

public class SphereMover : MonoBehaviour
{
	public float interpolationTime;
	public Vector3[] points;
	private float t;
	
	// Update is called once per frame
	void Update ()
	{
		if(points.Length > 1)
		{
			t += Time.deltaTime;
			if(t > interpolationTime)
			{
				t -= interpolationTime;
			}

			transform.position = MathUtil.BezierInterpolation(t / interpolationTime, points);
		}
	}
}
