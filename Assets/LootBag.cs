using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootBag : MonoBehaviour {

	public int numSpawns;
	public int numLoots; //textures 
	public Swag swagPrefab;

	[HideInInspector]
	public Road roadSpawn;

	private List<Swag> swags = new List<Swag>();


	//responsible for accessing the Engine.Main.loot array, 

	public void Init()
	{


	}

}
