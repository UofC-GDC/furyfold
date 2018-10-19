using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// The simplest possible enemy that can be made
// only implementing the bare minimum to compile 
// and function. Used as a demonstration of how to 
// make enemies. A step up from making towers
// </summary>
public class SimpleEnemy : BaseEnemy
{
	// No health. Just die on damage
    public override void OnDamage(int strength, DamageType type)
    {
        OnDeath();
    }

	// Destroy the enemy on death. No clean up needed
    public override void OnDeath()
    {
        Destroy(gameObject);
    }
}
