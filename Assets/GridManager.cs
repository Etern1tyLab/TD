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

	private Dictionary<string, GridObject> gridMap;

	private int levelGridX;
	private int levelGridY;
	private string levelGridData; 

	private string gridPrefab = "Prefabs/Objects/EmptyGridPrefab";
	private string spawnPrefab = "Prefabs/Objects/SpawnGridPrefab";
	private string waypointPrefab = "Prefabs/Objects/WayPointGridPrefab";

	// Called to create a grid on the map
	/// <summary>
	/// Creates the grid.
	/// </summary>
	/// <returns><c>true</c>, if grid was created, <c>false</c> otherwise.</returns>
	public bool CreateGrid (string levelName)
	{
		//Creating GridObject Dictionary
		gridMap = new Dictionary<string, GridObject>();

		//Loading level xml with all data for level
		Debug.Log("GridManager: Loading level xml");
		TextAsset levelAsset = (TextAsset) Resources.Load("Levels/" + levelName);  
		XmlDocument levelRoot = new XmlDocument ();
		levelRoot.LoadXml( levelAsset.text );


		if (loadLevelXml(levelRoot))
		{
			Debug.Log("GridManager: Xml is loaded. Now it will be parsed");
			string[] dataRows = levelGridData.Split(';');

			for(int y = 0; y < levelGridY; y++)
			{
				//levelGridY - 1 - x reverts map to normal
				string[] dataRowsElements = dataRows[levelGridY - 1 - y].Split(',');
				for(int x = 0; x < levelGridX; x++)
				{
					string id = x.ToString() + y.ToString();
					switch (dataRowsElements[x].Trim())
					{
					case "EM":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.EmptyGrid, gridPrefab));
						break;
					case "SP":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.SpawnPoint, spawnPrefab));
						break;
					case "DL":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.DownToLeft, waypointPrefab));
						break;
					case "DR":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.DownToRight, waypointPrefab));
						break;
					case "DT":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.DownToTop, waypointPrefab));
						break;
					case "LD":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.LeftToDown, waypointPrefab));
						break;
					case "LR":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.LeftToRight, waypointPrefab));
						break;
					case "LT":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.LeftToTop, waypointPrefab));
						break;
					case "RD":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.RightToDown, waypointPrefab));
						break;
					case "RL":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.RightToLeft, waypointPrefab));
						break;
					case "RT":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.RightToTop, waypointPrefab));
						break;
					case "TD":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.TopToDown, waypointPrefab));
						break;
					case "TL":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.TopToLeft, waypointPrefab));
						break;
					case "TR":
						gridMap.Add(id, new GridObject(x, y, GridObject.GridType.Waypoint, GridObject.WaypointDirection.TopToRight, waypointPrefab));
						break;
					default:
						Debug.LogError("GridManager: Unknown element in parsed file: " + dataRowsElements[y]);
						break;
					}
					// Load new grid object to scene
					Instantiate(gridMap[id].GridGameObject,new Vector3(x * 0.8f, y * 0.8f, 0),  Quaternion.identity);
				}
			}
			Debug.Log("GridManager: Grid is created.");
			return true;
		}
		else
		{
			Debug.LogError("GridManager: Grid is not created.");
			return false;
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

	private bool loadLevelXml(XmlDocument levelRoot) 
	{ 

		levelGridX = int.Parse(levelRoot.SelectSingleNode("levelData/level/gridValueX/text").InnerText);
		levelGridY= int.Parse(levelRoot.SelectSingleNode("levelData/level/gridValueY/text").InnerText);
		levelGridData = levelRoot.SelectSingleNode("levelData/level/grid/text").InnerText;

		if (levelGridX != null && levelGridY != null && levelGridData !=null)
			return true;
		else 
			return false;
	} 
}
