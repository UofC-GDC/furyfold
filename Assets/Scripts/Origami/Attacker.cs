using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Attacker : BaseEnemy
{

    public int health = 10;
    public float sightRange = 10f;
    public float attackRange = 1.5f;
    public int strength = 1;

    private BaseTower towerTarget; // Need this for DoDamage

    public override void Start()
    {
        base.Start();
        FindNearestTower();
    }

    public override void Update()
    {
        base.Update();

        // When stealth kills target, find a new one. When none are found do nothing.
        if (!FindNearestTower())
        {
            return;
        }

        DoDamage();
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    public override void OnDamage(int damage, DamageType type = DamageType.NORMAL)
    {
        health -= damage;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public void DoDamage()
    {
        if (IsTargetWithinAttackRange())
        {
            towerTarget.OnDamage(strength);
            target = gameObject.transform;
        }
    }

    private bool IsTargetWithinAttackRange()
    {
        float distance = Vector3.Distance(transform.position, towerTarget.transform.position);

        if (distance <= attackRange)
        {
            return true;
        }

        return false;
    }

    private bool FindNearestTower()
    {
        var rayCastHits = Physics.SphereCastAll(transform.position, sightRange, Vector3.up);

        //Select the BaseTower component of all nearby colliders that are Towers
        var hits =
            (from tower in
                (from hit in rayCastHits
                 select hit.collider.GetComponent<BaseTower>())
             where tower != null
             select tower).ToArray();

        // Find all BaseTowers in list that have a subclass of ArchwayTower and make a new list
        List<BaseTower> targets = new List<BaseTower>();

        targets = (from at in hits select at as BaseTower).ToList();

        // When no towers are found
        if (targets.Count <= 0 || targets == null)
        {
            return false;
        }

        // Go through all possible targets and choose the closest one as the target
        BaseTower p = null;
        foreach (BaseTower possibleTarget in targets)
        {
            if (p == null)
            {
                p = possibleTarget;
                target = possibleTarget.transform;
                towerTarget = possibleTarget;
            }

            if (p != null && Vector3.Distance(transform.position, possibleTarget.transform.position) <= Vector3.Distance(transform.position, p.transform.position))
            {
                target = possibleTarget.transform;
                towerTarget = possibleTarget;
            }
        }

        return true;
    }
}
