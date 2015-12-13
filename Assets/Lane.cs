using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Lane : MonoBehaviour 
{
	public enum LaneType
	{
		Curb,
		Street
	}

	public float laneIndex;
	public LaneType type;
}
