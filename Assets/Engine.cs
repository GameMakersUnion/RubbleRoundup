using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Engine : MonoBehaviour {

	public static Engine Main;

	private static uint NumRoads = 0;

	//drag and drop road textures in here, for rapid testing
	public Texture2D[] lanes;
	public Texture2D[] curbs;
	public Texture2D[] loots;

	public Road roadPrefab; //empty object responsible for making lanes

	public int numberOfLanes = 5;
	public int numberOfRoadsBuffered = 3;

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
        Road road = null;
		for (int i = 0; i < numberOfRoadsBuffered; i++)
		{
			Road temp = CreateNewRoadAhead();
			if (road == null)
			{
				road = temp;
			}
		}

		CameraSetter cs = Camera.main.gameObject.GetComponent<CameraSetter>();
		if(cs && road)
		{
			Bounds roadBounds = road.RoadBounds;
			Vector3 target = roadBounds.center;
			target.y = Player.Main.transform.position.y;

			float distance = roadBounds.extents.x;
			cs.CenterCamera(target, Player.Main.transform.forward * distance);
			Player.Main.transform.position = target;

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

    private Road CreateNewRoadAhead()
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
		newRoad.name += "_" + NumRoads.ToString();
		NumRoads++;
		newRoad.Init();
		if(farthestPosition == null)
		{
			width = newRoad.RoadWidth;
			farthestPosition = Player.Main.transform.position + new Vector3(-width/2, 0, 0);
		}
		newRoad.transform.position = new Vector3(farthestPosition.Value.x, farthestPosition.Value.y + length, farthestPosition.Value.z);

		return newRoad;
	}
}
