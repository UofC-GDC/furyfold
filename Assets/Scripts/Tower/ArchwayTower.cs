using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArchwayTower : BaseTower
{

	[SerializeField] int strength = 2;
	[SerializeField] int _range = 0;
	public int health = 10;

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

	override public void OnDamage(int strength, DamageType type = DamageType.NORMAL)
	{
		health -= strength;
	}

	override public void DoDamage(BaseEnemy[] enemies)
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<BaseEnemy>()?.OnDamage(strength);
	}
}
