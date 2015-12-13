using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Player Main;

    public float speedHorizontal = 3f;
    public float driveSpeed = 10f;
    private float minX = -6f;
    private float maxX = 6f;

	// Use this for initialization
	void Start () 
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
            minX = Road.ActiveRoad.RoadBounds.min.x;
            maxX = Road.ActiveRoad.RoadBounds.max.x;
        }
        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;

	}
}
