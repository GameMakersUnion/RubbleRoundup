using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Engine : MonoBehaviour {

    public static Engine Main;

    //drag and drop road textures in here, for rapid testing
    public Texture2D[] lanes;
    public Texture2D[] curbs;
    public Texture2D[] loots;

    public Road roadPrefab; //empty object responsible for making lanes

    public int numberOfLanes = 5;
    public int numberOfRoadsBuffered = 3;

    public Vector3 CameraTarget { get; private set; }

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

    void OnDestroy()
    {
        if(Main == this)
        {
            Main = null;
        }
    }
	
	void Start () 
    {
        for (int i = 0; i < numberOfRoadsBuffered; i++)
        {
            CreateNewRoadAhead();
        }

        LookCam lookCam = Player.Main.GetComponent<LookCam>();
        if(lookCam)
        {
            lookCam.SetCamera();
        }
    }

    [ExecuteInEditMode]
    void OnValidate()
    {
        if(numberOfLanes < 4)
        {
            numberOfLanes = 4;
        }

        if(numberOfRoadsBuffered < 1)
        {
            numberOfRoadsBuffered = 1;
        }
    }

    public void SignalRoadDestroyed()
    {
        CreateNewRoadAhead();
    }

    private void CreateNewRoadAhead()
    {
        Road[] roadObjects = GameObject.FindObjectsOfType<Road>();
        Vector3? farthestPosition = null;
        float length = 0;
        float width = 0;

        if (roadObjects.Length > 0)
        {
            foreach (var road in roadObjects)
            {
                if (farthestPosition == null || road.transform.position.y > farthestPosition.Value.y)
                {
                    farthestPosition = road.transform.position;
                    length = road.RoadLength;
                    width = road.RoadWidth;
                }
            }
        }

        Road newRoad = Instantiate<Road>(roadPrefab);
        newRoad.numLanes = 5;
        newRoad.Init();
        if(farthestPosition == null)
        {
            width = newRoad.RoadWidth;
            farthestPosition = Player.Main.transform.position + new Vector3(-width/2, 0, 0);
        }
        newRoad.transform.position = new Vector3(farthestPosition.Value.x, farthestPosition.Value.y + length, farthestPosition.Value.z);
    }
}
