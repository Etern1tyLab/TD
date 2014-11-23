//
//  Author:
//    Nikita Katsak dev.racoonlab@gmail.com
//
//  Copyright (c) 2014, Dev.RacoonLab
//
//  All rights reserved.
//



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GridObject
{
	public enum SpawnpointDirection
	{
		ToRight,
		ToLeft,
		ToDown,
		ToTop
	};
	public enum WaypointDirection
	{
		//Enums for Right
		RightToLeft,
		RightToDown,
		RightToTop,
		//Enums for Left
		LeftToRight,
		LeftToDown,
		LeftToTop,
		//Enums for Top
		TopToLeft,
		TopToRight,
		TopToDown,
		//Enums for Down
		DownToLeft,
		DownToRight,
		DownToTop
	};
	public enum GridType
	{
		SpawnPoint,
		EmptyGrid,
		Waypoint,
		HomeObject
	};

	/// <summary>
	/// The coordinate x.
	/// </summary>
	private int coordinateX;
	public int CoordinateX {get {return coordinateX;}}

	/// <summary>
	/// The coordinate y.
	/// </summary>
	private int coordinateY;
	public int CoordinateY {get {return coordinateY;}}

	/// <summary>
	/// The Grid type.
	/// </summary>
	private GridType type;
	public GridType Type {get {return type;}}

	/// <summary>
	/// The direction for waypoint.
	/// </summary>
	private WaypointDirection wayPointDirection;
	public WaypointDirection WayPointDirection {get {return wayPointDirection;}}

	/// <summary>
	/// The direction for waypoint.
	/// </summary>
	private SpawnpointDirection spawnPointDirection;
	public SpawnpointDirection SpawnPointDirection {get {return spawnPointDirection;}}

	/// <summary>
	/// The grid game object.
	/// </summary>
	private GameObject gridGameObject;
	public GameObject GridGameObject {
		get {
			return gridGameObject;
		}
		set {
			gridGameObject = value;
		}
	}

	/// <summary>
	/// The grid ID.
	/// </summary>
	private string gridID;
	public string GridID {get {return gridID;}}


	/// <summary>
	/// Initializes a new instance of the <see cref="GridObject"/> class with EMPTYGRID.
	/// </summary>
	/// <param name="_coordinateX">_coordinate x.</param>
	/// <param name="_coordinateY">_coordinate y.</param>
	/// <param name="_type">_type.</param>
	/// <param name="_prefabLocation">_prefab location.</param>
	public GridObject(string _id, int _coordinateX, int _coordinateY, GridType _type, string _prefabLocation)
	{
		gridID = _id;
		coordinateX = _coordinateX;
		coordinateY = _coordinateY;
		type = _type;

		gridGameObject = (GameObject) Resources.Load(_prefabLocation);
		gridGameObject.name = _type.ToString();

	}

	/// <summary>
	/// Initializes a new instance of the <see cref="GridObject"/> class with WAYPOINT.
	/// </summary>
	/// <param name="_coordinateX">_coordinate x.</param>
	/// <param name="_coordinateY">_coordinate y.</param>
	/// <param name="_type">_type.</param>
	/// <param name="_direction">_direction.</param>
	/// <param name="_gridGameObject">_grid game object.</param>
	public GridObject(string _id, int _coordinateX, int _coordinateY, GridType _type, WaypointDirection _direction, string _prefabLocation)
	{
		gridID = _id;
		coordinateX = _coordinateX;
		coordinateY = _coordinateY;
		type = _type;

		wayPointDirection = _direction;

		gridGameObject = (GameObject) Resources.Load(_prefabLocation);
		//Gives the name to the prefab in inspector
		gridGameObject.name = _type.ToString();

		setWaypointImage(_direction);

	}

	/// <summary>
	/// Initializes a new instance of the <see cref="GridObject"/> class with SPAWNPOINT.
	/// </summary>
	/// <param name="_coordinateX">_coordinate x.</param>
	/// <param name="_coordinateY">_coordinate y.</param>
	/// <param name="_type">_type.</param>
	/// <param name="_direction">_direction.</param>
	/// <param name="_gridGameObject">_grid game object.</param>
	public GridObject(string _id, int _coordinateX, int _coordinateY, GridType _type, SpawnpointDirection _direction, string _prefabLocation)
	{
		gridID = _id;
		coordinateX = _coordinateX;
		coordinateY = _coordinateY;
		type = _type;

		spawnPointDirection = _direction;
		
		gridGameObject = (GameObject) Resources.Load(_prefabLocation);
		//Gives the name to the prefab in inspector
		gridGameObject.name = _type.ToString();
		
		setSpawnpointImage(_direction);
		
	}

	/// <summary>
	/// Sets the waypoint image.
	/// </summary>
	/// <param name="_direction">_direction.</param>
	private void setWaypointImage(WaypointDirection _direction)
	{
		switch (_direction)
		{
		case WaypointDirection.RightToLeft:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(4);
			break;
		case WaypointDirection.RightToDown:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(7);
			break;
		case WaypointDirection.RightToTop:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(9);
			break;
		case WaypointDirection.LeftToRight:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(4);
			break;
		case WaypointDirection.LeftToDown:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(5);
			break;
		case WaypointDirection.LeftToTop:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(3);
			break;
		case WaypointDirection.TopToLeft:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(3);
			break;
		case WaypointDirection.TopToRight:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(9);
			break;
		case WaypointDirection.TopToDown:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(8);
			break;
		case WaypointDirection.DownToLeft:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(5);
			break;
		case WaypointDirection.DownToRight:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(7);
			break;
		case WaypointDirection.DownToTop:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(8);
			break;
		default:
			Debug.LogError("GridObject: Unknown direction:");
			break;
		}
	}

	/// <summary>
	/// Sets the spawnpoint image.
	/// </summary>
	/// <param name="_direction">_direction.</param>
	private void setSpawnpointImage(SpawnpointDirection _direction)
	{
		switch (_direction)
		{
		case SpawnpointDirection.ToLeft:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(10);
			break;
		case SpawnpointDirection.ToDown:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(10);
			break;
		case SpawnpointDirection.ToTop:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(10);
			break;
		case SpawnpointDirection.ToRight:
			this.GridGameObject.GetComponent<tk2dSprite>().SetSprite(10);
			break;
		default:
			Debug.LogError("GridObject: Unknown direction:");
			break;
		}
		
		
	}

	/// <summary>
	/// Gets the tk2ui item.
	/// </summary>
	/// <returns>The tk2ui item.</returns>
	public tk2dUIItem getTk2uiItem()
	{
		return this.GridGameObject.GetComponent<tk2dUIItem>();
	}

	/// <summary>
	/// Gets the transform.
	/// </summary>
	/// <returns>The transform.</returns>
	public Transform getTransform()
	{
		return this.GridGameObject.transform;
	}
	
}
