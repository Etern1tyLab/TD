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
using System.Collections.Generic;
using System;


public class CreepObject : MonoBehaviour{

	/// <summary>
	/// The creep current hp.
	/// </summary>
	private float creepCurrentHp;
	public float CreepCurrentHp {	get {return creepCurrentHp;}}

	/// <summary>
	/// The creep max hp.
	/// </summary>
	private float creepMaxHp;
	public float CreepMaxHp { get {return creepMaxHp;}}

	/// <summary>
	/// The creep current speed.
	/// </summary>
	private float creepCurrentSpeed;
	public float CreepCurrentSpeed { get {return creepCurrentSpeed;}}

	/// <summary>
	/// The grid game object.
	/// </summary>
	private GameObject creepGameObject;
	public GameObject CreepGameObject {
		get {
			return creepGameObject;
		}
		set {
			creepGameObject = value;
		}
	}

	/// <summary>
	/// The creep kill cost.
	/// </summary>
	private int creepKillCost;
	public int CreepKillCost { get {return creepKillCost;}}

	// 1% from max creep hp
	private float creepHpPercent;
	// Creep statuses
	private string[] creepStatus;
	//Unit's health bar
	private tk2dUIProgressBar healthBar;
	//Unit's health bar text
	private tk2dTextMesh healthBarText;


	/// <summary>
	/// Initializes a new instance of the <see cref="CreepObject"/> class.
	/// </summary>
	/// <param name="_creepCurrentHp">_creep current hp.</param>
	/// <param name="_creepMaxHp">_creep max hp.</param>
	/// <param name="_creepCurrentSpeed">_creep current speed.</param>
	/// <param name="_prefabLocation">_prefab location.</param>
	public CreepObject(string _creepName, float _creepHP, float _creepSpeed, int _creepCost)
	{
		creepCurrentHp = _creepHP;
		creepMaxHp = _creepHP;
		creepCurrentSpeed = _creepSpeed;
		creepGameObject = (GameObject) Resources.Load("Prefabs/UnitObjects/" + _creepName);
		creepKillCost = _creepCost;

	}

		
}
