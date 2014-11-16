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

public class CreepObject : MonoBehaviour {

	//Current creep healthpoints value
	private int creepCurrentHp;
	//Max creep healthpoints value
	private int creepMaxHp;
	//Current creep speed value
	private int creepCurrentSpeed;
	//Max creep speed value
	private int creepMaxSpeed;
	//Creep name value
	private string creepName;
	// 1% from max creep hp
	private float creepHpPercent;
	// Creep statuses
	private string[] creepStatus;

	//Unit's health bar
	private tk2dUIProgressBar healthBar;
	//Unit's health bar text
	private tk2dTextMesh healthBarText;



	//Current creep HealthPoints getter and setter
	/// <summary>
	/// Gets or sets the current creep hp.
	/// </summary>
	/// <value>The current creep hp.</value>
	public int CurrentCreepHp
	{
		get {return creepCurrentHp;}
		set {creepCurrentHp = value;}
	}

	//Max creep HealthPoints getter and setter
	/// <summary>
	/// Gets or sets the max creep hp.
	/// </summary>
	/// <value>The max creep hp.</value>
	public int MaxCreepHp
	{
		get {return creepMaxHp;}
		set {creepMaxHp = value;}
	}

	//Current Creep speed getter and setter
	/// <summary>
	/// Gets or sets the current creep speed.
	/// </summary>
	/// <value>The current creep speed.</value>
	public int CurrentCreepSpeed
	{
		get {return creepCurrentSpeed;}
		set {creepCurrentSpeed = value;}
	}

	//Mac creep speed getter and setter
	/// <summary>
	/// Gets or sets the max creep speed.
	/// </summary>
	/// <value>The max creep speed.</value>
	public int MaxCreepSpeed
	{
		get {return creepMaxSpeed;}
		set {creepMaxSpeed = value;}
	}

	//Creep name getter and setter
	/// <summary>
	/// Gets or sets the name of the creep.
	/// </summary>
	/// <value>The name of the creep.</value>
	public string CreepName
	{
		get {return creepName;}
		set {creepName = value;}
	}


	// Use this for initialization
	void Start () {
	   // Setting starting health
		this.CurrentCreepHp = this.MaxCreepHp;
		// Calculating health percent
		creepHpPercent = this.CurrentCreepHp / 100;


		//Initializng creep healthbar text
		healthBarText = this.gameObject.GetComponentInChildren(typeof(tk2dTextMesh)) as tk2dTextMesh;
		healthBarText.text = this.CurrentCreepHp + "/" + this.MaxCreepHp;

		//Initializng creep healthbar
		healthBar = this.gameObject.GetComponentInChildren(typeof(tk2dUIProgressBar)) as tk2dUIProgressBar;
		healthBar.Value = 1;
	
	}
	
	// Update is called once per frame
	void Update () {


					
	}

	/// <summary>
	/// Call on this creep hit.
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void onHit (int damage)
	{
		Debug.Log("CreepObject: <This object is hited for " + damage + " >");
		if (this.CurrentCreepHp <= 0)
		{
			Destroy(this.gameObject);
			Debug.Log("CreepObject: <This object is dead>");
		}

		this.CurrentCreepHp = this.CurrentCreepHp - damage;
		if (this.CurrentCreepHp > 0)
		{
			healthBar.Value = healthBar.Value - damage / creepHpPercent;
			healthBarText.text = this.CurrentCreepHp + "/" + this.MaxCreepHp;
		}

	}




}
