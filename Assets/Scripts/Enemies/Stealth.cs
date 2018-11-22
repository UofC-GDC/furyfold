using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stealth : BaseEnemy {

    public int health = 10;
    public int sightRange = 10;
    public int attackRange = 2;
    public int strength = 1;

    private BaseTower towerTarget; // Need this for DoDamage

    public override void Start()
    {
        base.Start();
        FindNearestArchwayOrBlessingScript();
    }

    public override void Update()
    {
        base.Update();

        // When stealth kills target, find a new one. When none are found do nothing.
        if(!FindNearestArchwayOrBlessingScript())
        {
            return;
        }

        DoDamage();
    }
    
    public void DoDamage()
    {
        if(IsTargetWithinAttackRange())
        {
            towerTarget.OnDamage(strength);
            target = gameObject.transform;
        }
    }

    public override void OnDamage(int damage, DamageType type = DamageType.NORMAL)
    {
        health -= damage;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    private bool IsTargetWithinAttackRange()
    {
        float distance = Vector3.Distance(transform.position, towerTarget.transform.position);
        
        if(distance <= attackRange)
        {
            return true;
        }

        return false;
    }

    private bool FindNearestArchwayOrBlessingScript()
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
        var targets = hits.OfType<ArchwayTower>().ToList();

        // When no archway towers are found
        if (targets.Count <= 0 || targets == null)
        {
            return false;
        }

        // Go through all possible targets and choose the closest one as the target
        BaseTower p = null;
        foreach (BaseTower possibleTarget in targets)
        {
            if(p == null)
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
