using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stealth : BaseEnemy {

    public int health = 10;
    public float sightRange = 10f;
    public float attackRange = 2.5f;
    public int strength = 1;

    private BaseTower towerTarget; // Need this for DoDamage

    public override void Start()
    {
        base.Start();
        FindNearestArchwayTower();
    }

    public override void Update()
    {
        base.Update();

        // When stealth kills target, find a new one. When none are found do nothing.
        if(!FindNearestArchwayTower())
        {
            return;
        }

        DoDamage();
    }
    
    public void DoDamage()
    {
        var itwar = IsTargetWithinAttackRange();
        print(itwar);
        if(itwar)
        {
            print(towerTarget);
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

    private bool FindNearestArchwayTower()
    {
        var rayCastHits = Physics.SphereCastAll(transform.position, sightRange, Vector3.up);

        //Select the BaseTower component of all nearby colliders that are Towers
        var hits =
            (from tower in
                (from hit in rayCastHits
                 select hit.collider.GetComponent<BaseTower>())
             where tower != null
             select tower).ToArray();

        var toPrint = "Hits: ";
        foreach (var hit in hits){
            toPrint += hit;
            toPrint += "\n";
        }
        print(toPrint);

        // Make a list of all nearby ArchwayTowers
        var archwayList =
            (from at in (from maybeAt in hits
                        select maybeAt as ArchwayTower)
            where at != null
            select at).ToList();

        // Make a hitlist of ArchwayTowers if any exist otherwise have a hitlist of all towers nearby
        List<BaseTower> targets = new List<BaseTower>();

        if (archwayList.Count() != 0)
        {
            targets = (from at in archwayList select at as BaseTower).ToList();
        }
        else
        {
            targets = (from at in hits select at as BaseTower).ToList();
        }

        toPrint = "Targets: \n";
        foreach (var hit in targets){
            toPrint += hit;
            toPrint += "\n";
        }
        toPrint += "Count: ";
        toPrint += targets.Count;
        print(toPrint);

        // When no towers are found
        if (targets.Count <= 0 || targets == null)
        {
            print(
                string.Format(
                    "({0} = {1} || {2} = {3}) = {4}",
                     targets.Count,
                     targets.Count <= 0,
                     targets,
                     targets == null,
                     targets.Count <= 0 || targets == null));
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
