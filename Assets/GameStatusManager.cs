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




public class GameStatusManager : MonoBehaviour {


	public GridManager gridManager;
	public UIManager uiManager;
	public SpawnManager spawnManager;


	private string currentLevel;
	private int currentStatus; //TODO

	//Creep speed getter and setter
	/// <summary>
	/// Gets or sets the current status.
	/// </summary>
	/// <value>The current status.</value>
	public int CurrentStatus
	{
		get {return currentStatus;}
		set {currentStatus = value;}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("GameStatus: Checking current scene");
		//If level.scene is loaded we need to get value to load XML file by its name.
		currentLevel = PlayerPrefs.GetString("CurrentLevel");

		if (uiManager.showLoadingScreen())
		{
			Debug.Log("GameStatus: Loading screen loaded");
			// Creating grid
			if (gridManager.CreateGrid (currentLevel))
			{
				Debug.Log("GameStatus: <Grid is created>");
				this.CurrentStatus = 1;
				uiManager.hideLoadingScreen();
			}
			else
			{
				Debug.LogError("GameStatus: <Grid is not created>");
			}
			
			if (spawnManager.SpawnWave())
			{
				Debug.Log("GameStatus: <Creeps spawned>");
			}
		}



			
	}
	
	// Update is called once per frame
	void Update () {
	
	}




}
