﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Road : MonoBehaviour 
{
	public Lane lanePrefab;

	public static Road ActiveRoad { get; private set; }

	public Lane Contains(Vector3 position)
	{
		foreach(var lane in lanes)
		{
			SpriteRenderer sr = lane.gameObject.GetComponent<SpriteRenderer>();
			if (sr)
			{
				if(sr.bounds.Contains(position))
				{
					return lane;
				}
			}
		}
		return null;
	}

	public float RoadWidth
	{
		get
		{
			float retVal = 0;
			if (lanes.Count > 0)
			{
				Renderer renderer = lanes[0].gameObject.GetComponent<Renderer>();
				if (renderer)
				{
					retVal = renderer.bounds.size.x * lanes.Count;
				}
			}

			return retVal;
		}
	}

	public float RoadLength
	{
		get
		{
			float retVal = 0;
			if (lanes.Count > 0)
			{
				Renderer renderer = lanes[0].gameObject.GetComponent<Renderer>();
				if (renderer)
				{
					retVal = renderer.bounds.size.y;
				}
			}

			return retVal;
		}
	}

	public Bounds RoadBounds
	{
		get
		{
			Bounds retVal = new Bounds(Vector3.zero, Vector3.zero);
			if (lanes.Count > 0)
			{
				bool assigned = false;

				foreach (var lane in lanes)
				{
					SpriteRenderer sr = lane.gameObject.GetComponent<SpriteRenderer>();
					if (sr)
					{
						if (assigned)
						{
							retVal.Encapsulate(sr.bounds);
						}
						else
						{
							retVal = sr.bounds;
							assigned = true;
						}
					}
				}
			}
			return retVal;
		}
	}


	public Bounds DrivableRoadBounds
	{
		get
		{
			Bounds retVal = new Bounds(Vector3.zero, Vector3.zero);
			if (lanes.Count > 0)
			{
				bool assigned = false;

				foreach (var lane in lanes)
				{
					if (lane.type == Lane.LaneType.Street)
					{
						SpriteRenderer sr = lane.gameObject.GetComponent<SpriteRenderer>();
						if (sr)
						{
							if (assigned)
							{
								retVal.Encapsulate(sr.bounds);
							}
							else
							{
								retVal = sr.bounds;
								assigned = true;
							}
						}
					}
				}
			}
			return retVal;
		}
	}

	[HideInInspector]
    public List<Lane> lanes = new List<Lane>();

	// Use this for initialization
	public void Init () 
    {
		if(lanePrefab)
		{
			int numLanes = Engine.Main.numberOfLanes;
			float lastX = 0;
			for(uint i = 0; i < numLanes; i++)
			{
				Lane lane = Instantiate<Lane>(lanePrefab);
				SpriteRenderer sr = lane.gameObject.GetComponent<SpriteRenderer>();
				if (sr)
				{
					lane.transform.parent = transform;
					lane.laneIndex = i;
					Texture2D texture;
					Lane.LaneType type;
					if(i == 0)
					{
						texture = Engine.Main.curbs[0];
						type = Lane.LaneType.Curb;
					}
					else if(i == 1)
					{
						texture = Engine.Main.lanes[0];
						type = Lane.LaneType.Street;
					}
					else if(i == numLanes - 1)
					{
						texture = Engine.Main.curbs[1];
						type = Lane.LaneType.Curb;
					}
					else if(i == numLanes - 2)
					{
						texture = Engine.Main.lanes[2];
						type = Lane.LaneType.Street;
					}
					else
					{
						texture = Engine.Main.lanes[1];
						type = Lane.LaneType.Street;
					}
					sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					lane.type = type;
					lane.transform.position = new Vector3(lastX + sr.bounds.extents.x, transform.position.y, transform.position.z);
					lanes.Add(lane);
					lastX = sr.bounds.max.x;
				}
			}
			BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
			col.isTrigger = true;
			Bounds bounds = RoadBounds;
			col.size = bounds.size;
			col.offset = bounds.center;
		}
		else
		{
			throw new UnityException("Road needs a lane prefab!");
		}
		InitLoot();
	}

	void InitLoot()
	{
		foreach (Lane lane in lanes)
		{
			lane.numSpawns = 1;
			lane.numLoots = Random.Range(1, 5);
			lane.Init();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0, -Player.Main.driveSpeed * Time.deltaTime, 0);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject == Player.Main.gameObject)
		{
			ActiveRoad = this;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject == Player.Main.gameObject)
		{
			if(ActiveRoad == this)
			{
				ActiveRoad = null;
			}

			Engine.Main.SignalRoadDestroyed();
			Destroy(this.gameObject, 5f);
		}
	}
}
