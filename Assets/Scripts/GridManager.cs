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

public class GridManager : MonoBehaviour {

	/// <summary>
	/// The game status.
	/// </summary>
	private GameStatusManager gameStatus;
	/// <summary>
	/// Dictionary of grid objects.
	/// </summary>
	private Dictionary<string, GridObject> gridMap;
	/// <summary>
	/// The spawn points IDs.
	/// </summary>
	private List<string> spawnPointsIDs = new List<string>();


	/// <summary>
	/// Prefabs values.
	/// </summary>
	private string gridPrefab = "Prefabs/GridObjects/EmptyGridPrefab";
	private string spawnPrefab = "Prefabs/GridObjects/SpawnGridPrefab";
	private string waypointPrefab = "Prefabs/GridObjects/WayPointGridPrefab";

	/// <summary>
	/// Initialize the specified _gameStatus.
	/// </summary>
	/// <param name="_gameStatus">_game status.</param>
	public void Initialize (GameStatusManager _gameStatus)
	{
		this.gameStatus = _gameStatus;
		Debug.Log("GridManager: Initialized");
	}


	// Called to create a grid on the map
	/// <summary>
	/// Creates the grid.
	/// </summary>
	/// <returns><c>true</c>, if grid was created, <c>false</c> otherwise.</returns>
	public Dictionary<string, GridObject> CreateGrid (int levelGridX, int levelGridY, string levelGridData)
	{
		if (levelGridData != null && !levelGridData.Equals(""))
		{
			//Creating GridObject Dictionary
			gridMap = new Dictionary<string, GridObject>();


			string[] dataRows = levelGridData.Split(';');

			for(int y = 0; y < levelGridY; y++)
			{
				//levelGridY - 1 - x reverts map to normal
				string[] dataRowsElements = dataRows[levelGridY - 1 - y].Split(',');
				for(int x = 0; x < levelGridX; x++)
				{
					string id = x.ToString() + "x." + y.ToString() + "y";
					switch (dataRowsElements[x].Trim())
					{
					case "EM":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.EmptyGrid, gridPrefab));
						break;
					case "SR":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.SpawnPoint, GridObject.SpawnpointDirection.ToRight, spawnPrefab));
						break;
					case "SL":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.SpawnPoint, GridObject.SpawnpointDirection.ToLeft, spawnPrefab));
						break;
					case "SD":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.SpawnPoint, GridObject.SpawnpointDirection.ToDown, spawnPrefab));
						break;
					case "ST":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.SpawnPoint, GridObject.SpawnpointDirection.ToTop, spawnPrefab));
						break;
					case "DL":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.DownToLeft, waypointPrefab));
						break;
					case "DR":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.DownToRight, waypointPrefab));
						break;
					case "DT":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.DownToTop, waypointPrefab));
						break;
					case "LD":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.LeftToDown, waypointPrefab));
						break;
					case "LR":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.LeftToRight, waypointPrefab));
						break;
					case "LT":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.LeftToTop, waypointPrefab));
						break;
					case "RD":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.RightToDown, waypointPrefab));
						break;
					case "RL":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.RightToLeft, waypointPrefab));
						break;
					case "RT":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.RightToTop, waypointPrefab));
						break;
					case "TD":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.TopToDown, waypointPrefab));
						break;
					case "TL":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.TopToLeft, waypointPrefab));
						break;
					case "TR":
						gridMap.Add(id, new GridObject(id, x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.TopToRight, waypointPrefab));
						break;
					default:
						Debug.LogError("GridManager: Unknown element in parsed file: " + dataRowsElements[y]);
						break;
					}
					// Add spawn point location to list
					if (gridMap[id].GetType().Equals(GridObject.GridType.SpawnPoint))
						spawnPointsIDs.Add(id);
					// Load new grid object to scene

					gridMap[id].GridGameObject = (GameObject) Instantiate(gridMap[id].GridGameObject,new Vector3(x * 0.8f, y * 0.8f, 0),  Quaternion.identity);
				}
			}
			Debug.Log("GridManager: Grid is created.");
			return gridMap;
		}
		else
		{
			Debug.LogError("GridManager: Grid is not created.");
			return null;
		}


	}

	/// <summary>
	/// Enables the grid click.
	/// </summary>
	/// <param name="gridMap">Grid map.</param>
	/*public void enableGridClick(GameObject[,] gridMap)
	{
		for(int x = 0; x < mapSizeX; x++)
		{
			for(int y = 0; y < mapSizeY; y++)
			{
				tk2dUIItem gridElement = gridMap[x, y].GetComponent<tk2dUIItem>();
				gridElement.OnClickUIItem += Clicked;
			}
		}

	}
	
	/// <summary>
	/// Clicked the specified gridUIItem.
	/// </summary>
	/// <param name="clickedUIItem">Clicked user interface item.</param>
	void Clicked(tk2dUIItem clickedUIItem)
	{
		Debug.Log("Clicked:" + clickedUIItem);
	}*/
	
	//TODO OnDisable()
}
