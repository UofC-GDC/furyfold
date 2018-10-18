using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
	// Event Handlers for when the enemy dies and when it takes damage
	void OnDeath();
	void OnDamage(int strength, DamageType type = DamageType.NORMAL);
}
