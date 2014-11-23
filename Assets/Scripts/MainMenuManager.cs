using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Debug.Log("MainMenuManager: Checking current scene");
		if (Application.loadedLevelName.Equals("MainMenu"))
		{
			//On start of game we assign "MainMenu" value to CurrentLevel in player prefs.
			PlayerPrefs.SetString("CurrentLevel", "MainMenu");
			Debug.Log("MainMenuManager: Current scene is MainMenu");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	///  Event for start button click
	/// </summary>
	private void StartButtonClicked()
	{
		Debug.Log("MainMenuManager: Loading Level1");
		PlayerPrefs.SetString("CurrentLevel", "Level1");
		Debug.Log("MainMenuManager: "+ PlayerPrefs.GetString("CurrentLevel") + " is in prefs ");
		Application.LoadLevel("Test");
	}


}
