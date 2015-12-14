using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class Lane : MonoBehaviour 
{
	public enum LaneType
	{
		Curb,
		Street
	}

	public uint laneIndex;
	public LaneType type;
	public int numSpawns;
	public int numLoots; //textures 
	public Swag swagPrefab;

	private List<Swag> swags = new List<Swag>();


	//loot
	public void Init () 
	{
		if (swagPrefab)
		{
			float lastX = 0;
			for (int i = 0; i < numSpawns; i++)
			{
				Swag swag = Instantiate<Swag>(swagPrefab);
				BoxCollider2D bc = swag.GetComponent<BoxCollider2D>();
				SpriteRenderer sr = swag.gameObject.GetComponent<SpriteRenderer>();
				if (sr)
				{
					swag.transform.parent = transform;
					swag.swagIndex = i;

					int randomLoot = Random.Range(0, numLoots);
					Texture2D texture = Engine.Main.loots[randomLoot];

					sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					Vector2 size = sr.sprite.bounds.size;
					bc.size = size;

					swag.transform.position = transform.position;
					sr.sortingOrder += 1;
					sr.bounds.Encapsulate(sr.bounds);

					//determines edges blah
					//= new Vector3(lastX + sr.bounds.extents.x, transform.position.y, transform.position.z);
					//swags.Add(swag);
					//lastX = sr.bounds.max.x;
					//offset
				}
			}
			//Bounds bounds = RoadBounds;
			//col.size = bounds.size;
			//col.offset = bounds.center;
		}
		else
		{
			throw new UnityException("Lane needs a swag prefab!");
		}
	}

	public void SpawnObject()
	{

	}
}

