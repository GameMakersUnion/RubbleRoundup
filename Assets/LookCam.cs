using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LookCam : MonoBehaviour 
{

	public void SetCamera(Vector3 target)
	{
		Vector3 dtarget = Camera.main.transform.position;
		dtarget.x = transform.position.x;
		transform.LookAt(dtarget);

		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		if (sr) { sr.sortingOrder = 1; }
		if (transform.tag == "Player") sr.sortingOrder = sr.sortingOrder + 1;

	}
}
