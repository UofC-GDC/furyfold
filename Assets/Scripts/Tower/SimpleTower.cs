﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// The simplest possible tower that can be made
// only implementing the bare minimum to compile
// and function. Used as a demonstration of how to
// make towers
// </summary>
public class SimpleTower : BaseTower
{
	// Configurable in Inspector
	[SerializeField] int strength = 0;
	[SerializeField] int _range = 0;


	// Just return the property set in the inspector
	public override float range
	{
		get
		{
			return _range;
		}
	}

	// Just do some damage to every enemy equally
	public override void DoDamage(BaseEnemy[] enemies)
	{

		foreach (var enemy in enemies)
		{
			enemy.OnDamage(strength);
		}
	}
}
