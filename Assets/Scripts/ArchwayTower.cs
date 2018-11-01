using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArchwayTower : BaseTower {

	[SerializeField] int strength = 2;
	[SerializeField] int _range = 0;

	public override float range
	{
		get
		{
			return _range;
		}
	}

	public int health;

	// Use this for initialization
	void Start () {
		health = 10;
		Debug.Log("Arch Start");
		GetComponent<NavMeshObstacle>().enabled = false;
	}
	
	// Update is called once per frame
	override public void Update () {}

	override public void OnDamage(int strength, DamageType type = DamageType.NORMAL)
	{
		health-=strength;
	}

	override public  void DoDamage(BaseEnemy[] enemies){
	}
	override public void OnDeath()
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other){
		other.GetComponent<BaseEnemy>()?.OnDamage(strength);
		Debug.Log("Colliding");
	}
}
