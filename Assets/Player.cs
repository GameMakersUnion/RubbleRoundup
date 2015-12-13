using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player Main;

	public float speedHorizontal = 3f;
	public float driveSpeed = 10f;
	private float minX = -6f;
	private float maxX = 6f;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () 
	{
		if(Main == null)
		{
			Main = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Start()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void OnDestroy()
	{
		if (Main == this)
		{
			Main = null;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		Vector3 position = transform.position;
		float x = Input.GetAxis("Horizontal");
		position += new Vector3(speedHorizontal * x, 0, 0) * Time.deltaTime;
		if(Road.ActiveRoad)
		{
			minX = Road.ActiveRoad.DrivableRoadBounds.min.x - spriteRenderer.bounds.extents.x;
			maxX = Road.ActiveRoad.DrivableRoadBounds.max.x + spriteRenderer.bounds.extents.x;
		}
		position.x = Mathf.Clamp(position.x, minX, maxX);

		transform.position = position;

	}
}
