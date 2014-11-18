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


public class GameStatusManager : MonoBehaviour {

	/// <summary>
	/// The managers.
	/// </summary>
	public GridManager gridManager;
	public UIManager uiManager;
	public SpawnManager spawnManager;

	/// <summary>
	/// Parsed XML values.
	/// </summary>
	private int levelGridX;
	public int LevelGridX { get {return levelGridX;}}

	private int levelGridY;
	public int LevelGridY {	get {return levelGridY;}}

	private string levelGridData; 
	private int levelWavesCount;

	/// <summary>
	/// Dictionary of grid objects.
	/// </summary>
	private Dictionary<string, GridObject> gridMap;

	/// <summary>
	/// The current level.
	/// </summary>
	private string currentLevel;

	/// <summary>
	/// Spawner values.
	/// </summary>
	private float waveDelayTimer = 30.0F;
	public float waveCooldown = 20.0F; //переменная (не константа уже!) для сброса таймера выше, мы её будем модифицировать
	public int waveNumber = 1; //переменная текущей волны
	private int MobCount;



	// Use this for initialization
	void Start () {

		gridManager.Initialize (this);
		spawnManager.Initialize (this);


		Debug.Log("GameStatus: Checking current scene");
		//If level.scene is loaded we need to get value to load XML file by its name.
		currentLevel = PlayerPrefs.GetString("CurrentLevel");

		//Loading level xml with all data for level
		Debug.Log("GridManager: Loading level xml");
		TextAsset levelAsset = (TextAsset) Resources.Load("Levels/" + currentLevel);  
		XmlDocument levelRoot = new XmlDocument ();
		levelRoot.LoadXml( levelAsset.text );
		Debug.Log("GameStatus: Xml is loaded. Now it will be parsed");


		if (loadLevelXml(levelRoot))
		{
			Debug.Log("GameStatus: XML is parsed. Loading grid manager to create map");
			// Creating grid
			gridMap = gridManager.CreateGrid (levelGridX, levelGridY, levelGridData);
			Debug.Log("GameStatus: Grid is created. Loading spawn data");
			//Loading waves
			if (loadSpawnDataXml(levelRoot))
			{
				Debug.Log("GameStatus: Spawn data is loaded. Creating waypoints");
				if (createWaypoints(gridMap))
				{
					Debug.Log("GameStatus: WaypointsCreated");
				}

			}

		}

	}



			

	
	// Update is called once per frame
	void Update () {

		if (waveDelayTimer > 0) //если таймеh спауна волны больше нуля
		{

				if (MobCount == 0) waveDelayTimer = 0; //если мобов на сцене нет - устанавливаем его в ноль
				else waveDelayTimer -= Time.deltaTime; //иначе отнимаем таймер

		}
		if (waveDelayTimer <= 0) //если таймер менее или равен нулю
		{
			if (waveNumber <= levelWavesCount) //если имеются точки спауна и ещё не достигнут предел количества волн
			{
				Debug.Log("GameStatus: Spawning wave " + waveNumber );

				spawnManager.spawnWave(waveNumber);


				if (waveCooldown > 5.0f) //если задержка длится более 5 секунд
				{
					waveCooldown -= 0.1f; //сокращаем на 0.1 секунды
					waveDelayTimer = waveCooldown; //задаём новый таймер
				}
				else //иначе
				{
					waveCooldown = 5.0f; //задержка никогда не будет менее 5 секунд
					waveDelayTimer = waveCooldown;
				}

				waveNumber++; //увеличиваем номер волны
			}
		}
	
	}

	/// <summary>
	/// Loads the level xml.
	/// </summary>
	/// <returns><c>true</c>, if level xml was loaded, <c>false</c> otherwise.</returns>
	/// <param name="levelRoot">Level root.</param>
	private bool loadLevelXml(XmlDocument levelRoot) 
	{ 
		//Grid Data
		levelGridX = int.Parse(levelRoot.SelectSingleNode("levelData/level/gridValueX").InnerText);
		levelGridY= int.Parse(levelRoot.SelectSingleNode("levelData/level/gridValueY").InnerText);
		levelGridData = levelRoot.SelectSingleNode("levelData/level/grid").InnerText;
	

		if (levelGridX != null && levelGridY != null && levelGridData !=null)
			return true;
		else 
			return false;
	} 

	/// <summary>
	/// Loads the spawn data xml.
	/// </summary>
	/// <param name="levelRoot">Level root.</param>
	private bool loadSpawnDataXml(XmlDocument levelRoot)
	{
		//WavesCount
		levelWavesCount = int.Parse(levelRoot.SelectSingleNode("levelData/level/wavesCount").InnerText);
		// Initializing waves
		spawnManager.initializeSpawnWaves(levelWavesCount);
		//Populating waves with creeps

		for (int i = 1; i <= levelWavesCount; i++) 
		{
			//Creep type in wave
			int creepTypeCount = int.Parse(levelRoot.SelectSingleNode("levelData/level/waves/wave" + i.ToString() + "/creepTypeCount").InnerText);
			for (int j = 1; j <= creepTypeCount; j++) {
				int creepCount = int.Parse(levelRoot.SelectSingleNode("levelData/level/waves/wave" + i.ToString() + "/creeps/creep" + j.ToString() + "/creepCount").InnerText);
				string creepName = levelRoot.SelectSingleNode("levelData/level/waves/wave" + i.ToString() + "/creeps/creep" + j.ToString() + "/creepName").InnerText;
				float creepHP = float.Parse(levelRoot.SelectSingleNode("levelData/level/waves/wave" + i.ToString() + "/creeps/creep" + j.ToString() + "/creepHP").InnerText);
				float creepSpeed = float.Parse(levelRoot.SelectSingleNode("levelData/level/waves/wave" + i.ToString() + "/creeps/creep" + j.ToString() + "/creepSpeed").InnerText);
				int creepCost = int.Parse(levelRoot.SelectSingleNode("levelData/level/waves/wave" + i.ToString() + "/creeps/creep" + j.ToString() + "/creepCost").InnerText);

				spawnManager.addCreepsToWave(i, creepCount, creepName, creepHP, creepSpeed,  creepCost);
			}
		}

		return true;
	}

	/// <summary>
	/// Creates the waypoints.
	/// </summary>
	private bool createWaypoints(Dictionary<string, GridObject> _gridmap)
	{
		try {
			List<string> spawnPoints = new List<string>();
			List<string> keys = new List<string>(_gridmap.Keys);
			foreach (string key in keys)
			{
				//Finding spawnpoints
				if(_gridmap[key].Type.Equals(GridObject.GridType.SpawnPoint))
				{
					spawnPoints.Add(_gridmap[key].GridID);
				}
			}
			
			foreach (string spawnPoint in spawnPoints)
			{
				spawnManager.buildWay(spawnPoint,_gridmap);
			}
			return true;
		} 
		catch (System.Exception ex) 
		{
			Debug.LogError("GameStatus: createWaypoints failed - " + ex);
			return false;
		}


	}

}
