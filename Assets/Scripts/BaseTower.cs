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

	private const float timeOut = 0.1f;
	private float lastLoopTime;

	
	// Update is called once per frame
	public virtual void Update () {
		if (Time.time - lastLoopTime > timeOut){
			// find all nearby objects
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
			
			lastLoopTime = Time.time;
		}
	}

	// How to deal with each enemy
	public abstract void DoDamage(BaseEnemy[] enemies);
}

public enum DamageType{
	NORMAL, GROUND, AIR
}
