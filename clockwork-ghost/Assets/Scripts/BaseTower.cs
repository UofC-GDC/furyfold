using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

// <summary>
// Abstract base class of all towers. Simplifies adding
// new towers by taking care of the base functionality
// of finding enemies to attack and dealing damage
// to them. 
// </summary>

[RequireComponent(typeof(NavMeshObstacle))]
public abstract class BaseTower : MonoBehaviour {

	// The max distance at which the Tower will find objects to attack
	public abstract float range { get; }
	
	// Update is called once per frame
	public virtual void Update () {
		// find all nearby objects
		var rayCastHits = Physics.SphereCastAll(transform.position, range, Vector3.up);
		
		// For each nearby object, if it's an enemy, do damage to it
		foreach (var hit in rayCastHits){
			var enemy = hit.collider.GetComponent<BaseEnemy>();
			if (enemy != null){
				DoDamage(enemy);
			}
		}
	}

	// How to deal with each enemy
	public abstract void DoDamage(BaseEnemy enemy);
}

public enum DamageType{
	NORMAL, GROUND, AIR
}
