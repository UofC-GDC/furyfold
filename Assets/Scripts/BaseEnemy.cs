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
public abstract class BaseEnemy : MonoBehaviour, IDamagable
{

	// used by Unity AI to move the enemy intelligently
	protected NavMeshAgent agent;
	public Transform target;
    public static long counter = 0;
    public long ID;

	// Use this for initialization
	public virtual void Start()
	{
        ID = counter;
        agent = GetComponent<NavMeshAgent>();
        counter++;
	}

	// Update is called once per frame
	public virtual void Update()
	{
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
	}

	public abstract void OnDeath();
	public abstract void OnDamage(int strength, DamageType type = DamageType.NORMAL);
}
