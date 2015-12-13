using UnityEngine;
using System.Collections;

public class CameraSetter : MonoBehaviour 
{
	public void CenterCamera(Vector3 target, Vector3 distanceVector)
	{
		this.transform.position = target + distanceVector;
		this.transform.LookAt(target);
	}
}
