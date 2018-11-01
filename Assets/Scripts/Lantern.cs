using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Lantern : BaseTower {


	[SerializeField] int _range = 5;
	[SerializeField] int strength = 10;
	public int health =10;
	public override float range
	{
		get
		{
			return _range;
		}
	}



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	override public void Update () {
		if(health<=0) OnDeath();
	}

	override public void OnDamage(int strength, DamageType type = DamageType.NORMAL)
	{
		health-=strength;
	}

	override public  void DoDamage(BaseEnemy[] enemies){
		foreach (var enemy in enemies)
		{
			enemy.OnDamage(strength);
		}
	}

	override public void OnDeath()
	{
			var rayCastHits = Physics.SphereCastAll(transform.position, range, Vector3.up);

			//Select the BaseEnemy component of all nearby colliders that are enemies
			var hits =
				(from enemy in
					(from hit in rayCastHits
					 select hit.collider.GetComponent<BaseEnemy>())
				 where enemy != null
				 select enemy).ToArray();

			// Let the implementing class deal with the enemies
			DoDamage(hits);
		Destroy(gameObject);
	}

}


