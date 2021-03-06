﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArchwayTower : BaseTower
{

	[SerializeField] int strength = 2;
	[SerializeField] int _range = 0;

	public override float range
	{
		get
		{
			return _range;
		}
	}



	// Use this for initialization
	void Start()
	{
		GetComponent<NavMeshObstacle>().enabled = false;
	}

	// Update is called once per frame
	override public void Update() { }


	override public void DoDamage(BaseEnemy[] enemies)
	{
	}

	private void OnTriggerStay(Collider other)
	{
		other.GetComponent<BaseEnemy>()?.OnDamage(strength);
	}
}
