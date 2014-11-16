//
//  Author:
//    Nikita Katsak dev.racoonlab@gmail.com
//
//  Copyright (c) 2014, Dev.RacoonLab
//
//  All rights reserved.
//
//

using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public GridManager gridManager;
	private int count = 10;



	private GameObject[] creepArray;
	private CreepObject[] creepStatsArray;

	public GameObject[] CreepArray {get {return creepArray;}}



	public bool SpawnWave ()//TODO here will be creep count and other stuff)
	{
		creepArray = new GameObject[count];
		creepStatsArray = new CreepObject[count];

		for(int i = 0; i < count; i++)
		{
			Debug.Log("SpawnManager: <Spawning object № " + i + " >");
			creepArray[i] = (GameObject)Instantiate(Resources.Load("Prefabs/Objects/UnitPrefab"));
						
			//Loading stats to the creep
			creepStatsArray[i] = creepArray[i].gameObject.GetComponent ("CreepObject") as CreepObject;
			creepStatsArray[i].MaxCreepSpeed = 1;
			creepStatsArray[i].MaxCreepHp = 1000;
			//Setting its initial position to spawn point
			//creepArray[i].gameObject.transform.position = gridManager.SpawnPoints[0].gameObject.transform.position;
			
			Debug.Log("SpawnManager: <Object № " + i + " is spawned>");

		}
		return true;


	}
}
