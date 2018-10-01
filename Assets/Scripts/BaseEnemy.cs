using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// <summary>
// Abstract base class of all Enemies in the game
// Takes care of basic things such as setting 
// the destination of the navMesh agent and
// exposes and interface for other objects
// to interact with enemies
// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseEnemy : MonoBehaviour {

	// used by Unity AI to move the enemy intelligently
	protected NavMeshAgent agent;
	public Transform target;

	// Used by a spawn manager to to choose how many of these enemies to spawn
	// and when to start spawing them
	public abstract int spawnRate { get; }
	public abstract int startWave { get; }

	// Use this for initialization
	public virtual void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		agent.SetDestination(target.position);
	}

	// Event Handlers for when the enemy dies and when it takes damage
	public abstract void OnDeath();
	public abstract void OnDamage(int strength, DamageType type = DamageType.NORMAL);
}
