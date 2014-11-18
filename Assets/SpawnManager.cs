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
using System.Xml; 
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

	/// <summary>
	/// The game status.
	/// </summary>
	private GameStatusManager gameStatus;

	private Dictionary<string, CreepWaveObject> creepWaves = new Dictionary<string, CreepWaveObject>();

	/// <summary>
	/// The waypoints.
	/// </summary>
	List<Transform[]> ways = new List<Transform[]> ();
	List<Transform> way;

	/// <summary>
	/// Initialize the specified _gameStatus.
	/// </summary>
	/// <param name="_gameStatus">_game status.</param>
	public void Initialize (GameStatusManager _gameStatus)
	{
		this.gameStatus = _gameStatus;
		Debug.Log("SpawnManager: Initialized");
	}


	public void spawnWave (int _wave)
	{
		for (int i = 0; i < creepWaves ["Wave" + _wave.ToString()].CreepsInWave.Count; i++) 
		{
			for (int j = 0; j < ways.Count; j++) 
			{
				creepWaves ["Wave"+_wave.ToString()].CreepsInWave[i].CreepGameObject = 
					(GameObject)Instantiate(creepWaves["Wave"+_wave.ToString()].CreepsInWave[i].CreepGameObject,ways[j][0].position, Quaternion.identity);


			}
		}


	}

	/// <summary>
	/// Adds the creeps to wave.
	/// </summary>
	/// <param name="">.</param>
	public void addCreepsToWave (int _waveNumber, int _creepCount, string _creepName, float _creepHP, float _creepSpeed, int _creepCost)
	{
		for (int i = 1; i <= _creepCount; i++)
		{
				creepWaves ["Wave" + _waveNumber.ToString()].addCreep (_creepName, _creepHP, _creepSpeed,  _creepCost);
		}
	}

	/// <summary>
	/// Initializes the spawn waves.
	/// </summary>
	/// <param name="wavesCount">Waves count.</param>
	public void initializeSpawnWaves (int _wavesCount)
	{
		for (int i = 1; i <= _wavesCount; i++)
		{
			creepWaves.Add("Wave" + i.ToString(), new CreepWaveObject());
		}

	}

	/// <summary>
	/// Builds the way.
	/// </summary>
	/// <param name="_spawnPointID">_spawn point ID.</param>
	/// <param name="_gridMap">_grid map.</param>
	public bool buildWay(string _spawnPointID, Dictionary<string, GridObject> _gridMap)
	{
		try 
		{
			Debug.Log("SpawnManager:Building way");
			
			way = new List<Transform> ();
			
			int lastX = _gridMap [_spawnPointID].CoordinateX;
			int lastY = _gridMap [_spawnPointID].CoordinateY;
			GridObject.SpawnpointDirection spawnPointType = _gridMap [_spawnPointID].SpawnPointDirection;
			
			//Add spawnpoint to way as first item
			way.Add (_gridMap [_spawnPointID].getTransform());

			switch (spawnPointType)
			{
			case GridObject.SpawnpointDirection.ToDown:
			{
				way.Add(_gridMap[lastX.ToString() + "x." + (lastY - 1).ToString() + "y"].getTransform());
				lastY--;
				break;
			}
			case GridObject.SpawnpointDirection.ToTop:
			{
				way.Add(_gridMap[lastX.ToString() + "x." + (lastY + 1).ToString() + "y"].getTransform());
				lastY++;
				break;
			}
			case GridObject.SpawnpointDirection.ToRight:
			{
				way.Add(_gridMap[(lastX + 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
				lastX++;
				break;
			}
			case GridObject.SpawnpointDirection.ToLeft:
			{
				way.Add(_gridMap[(lastX - 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
				lastX--;
				break;
			}
			default:
				Debug.LogError("SpawnManager: Unknown element");
				break;
			}

			//Iterating through availible waypoints
			while (lastX < gameStatus.LevelGridX - 1 && lastY < gameStatus.LevelGridY - 1) 
			{

				string wayPointID = lastX.ToString() + "x." + lastY.ToString() + "y";
				GridObject.WaypointDirection wayPointType = _gridMap [wayPointID].WayPointDirection;
				switch (wayPointType)
				{
				case GridObject.WaypointDirection.RightToLeft:
				{
					way.Add(_gridMap[(lastX - 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
					lastX--;
					break;
				}
				case GridObject.WaypointDirection.RightToDown:
				{
					way.Add(_gridMap[lastX.ToString() + "x." + (lastY - 1).ToString() + "y"].getTransform());
					lastY--;
					break;
				}
				case GridObject.WaypointDirection.RightToTop:
				{
					way.Add(_gridMap[lastX.ToString() + "x." + (lastY + 1).ToString() + "y"].getTransform());
					lastY++;
					break;
				}
				case GridObject.WaypointDirection.LeftToRight:
				{
					way.Add(_gridMap[(lastX + 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
					lastX++;
					break;
				}
				case GridObject.WaypointDirection.LeftToDown:
				{
					way.Add(_gridMap[lastX.ToString() + "x." + (lastY - 1).ToString() + "y"].getTransform());
					lastY--;
					break;
				}
				case GridObject.WaypointDirection.LeftToTop:
				{
					way.Add(_gridMap[lastX.ToString() + "x." + (lastY + 1).ToString() + "y"].getTransform());
					lastY++;
					break;
				}
				case GridObject.WaypointDirection.TopToLeft:
				{
					way.Add(_gridMap[(lastX - 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
					lastX--;
					break;
				}
				case GridObject.WaypointDirection.TopToRight:
				{
					way.Add(_gridMap[(lastX + 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
					lastX++;
					break;
				}
				case GridObject.WaypointDirection.TopToDown:
				{
					way.Add(_gridMap[lastX.ToString() + "x." + (lastY - 1).ToString() + "y"].getTransform());
					lastY--;
					break;
				}
				case GridObject.WaypointDirection.DownToLeft:
				{
					way.Add(_gridMap[(lastX - 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
					lastX--;
					break;
				}
				case GridObject.WaypointDirection.DownToRight:
				{
					way.Add(_gridMap[(lastX + 1).ToString() + "x." + lastY.ToString() + "y"].getTransform());
					lastX++;
					break;
				}
				case GridObject.WaypointDirection.DownToTop:
				{
					way.Add(_gridMap[lastX.ToString() + "x." + (lastY + 1).ToString() + "y"].getTransform());
					lastY++;
					break;
				}
				default:
					Debug.LogError("GridObject: Unknown direction:");
					break;
				}
			}
			
			//Add current way to available ways
			ways.Add (way.ToArray());
			Debug.Log("SpawnManager:Way added to available ways");
			return true;
		} 
		catch (System.Exception ex) 
		{
			Debug.LogError("SpawnManager:Way is not created - " + ex.StackTrace);
			return false;
		}

	}
}


