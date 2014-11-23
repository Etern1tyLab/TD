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
using System.Xml; 
using System.Collections.Generic;

public class CreepWaveObject 
{
	/// <summary>
	/// The creeps in wave.
	/// </summary>
	List<CreepObject> creepsInWave; 
	public List<CreepObject> CreepsInWave {get {return creepsInWave;}}



	/// <summary>
	/// Initializes a new instance of the <see cref="CreepWaveObject"/> class.
	/// </summary>
	public CreepWaveObject ()
	{
		creepsInWave = new List<CreepObject>();

	}

	/// <summary>
	/// Adds the creep.
	/// </summary>
	/// <param name="_creepName">_creep name.</param>
	/// <param name="_creepHP">_creep HP</param>
	/// <param name="_creepSpeed">_creep speed.</param>
	/// <param name="_creepCost">_creep cost.</param>
	public void addCreep(string _creepName, float _creepHP, float _creepSpeed, int _creepCost)
	{
		creepsInWave.Add (new CreepObject (_creepName, _creepHP, _creepSpeed, _creepCost ));
	}




}
