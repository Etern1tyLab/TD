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

public class UIManager : tk2dUIBaseDemoController{

	public GameObject loadingScreen;

	void Start () 
	{

		loadingScreen = (GameObject)Instantiate(Resources.Load("Prefabs/UI/LoadingScreen"));
		loadingScreen.transform.position = new Vector3 (5, 5, 0);
		HideWindow(loadingScreen.transform);
		Debug.Log("UIManager: Level UI loaded");
	}


	public bool showLoadingScreen ()
	{
		ShowWindow(loadingScreen.transform);
		return true;
	}

	public void hideLoadingScreen ()
	{
		ShowWindow(loadingScreen.transform);
	}


}
