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
        if(itwar)
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

    private bool FindNearestArchwayTower()
    {
        //REFACTOR NOTE Physics call too expensive to do with many objects every frame.

        #region Refactored
        //var rayCastHits = Physics.SphereCastAll(transform.position, sightRange, Vector3.up);

        ////Select the BaseTower component of all nearby colliders that are Towers
        //var hits =
        //    (from tower in
        //        (from hit in rayCastHits
        //         select hit.collider.GetComponent<BaseTower>())
        //     where tower != null
        //     select tower).ToArray();

        //// Make a list of all nearby ArchwayTowers
        //var archwayList =
        //    (from at in (from maybeAt in hits
        //                select maybeAt as ArchwayTower)
        //    where at != null
        //    select at).ToList();
        #endregion

        var archwayList = FindObjectsOfType<ArchwayTower>().OrderBy(st => (st.gameObject.transform.position - transform.position).sqrMagnitude).ToList();

        // Make a hitlist of ArchwayTowers if any exist otherwise have a hitlist of all towers nearby
        List<BaseTower> targets = new List<BaseTower>();

        if (archwayList.Count() != 0)
        {
            targets = (from at in archwayList select at as BaseTower).ToList();
        }
        else
        {
            var hits = FindObjectsOfType<BaseTower>().OrderBy(st => (st.gameObject.transform.position - transform.position).sqrMagnitude);
            targets = (from at in hits select at as BaseTower).ToList();
            // When no towers are found
            if (targets.Count <= 0 || targets == null)
            {
                return false;
            }
        }

        towerTarget = targets[0];
        target = towerTarget.transform;

        #region Refactored
        //// Go through all possible targets and choose the closest one as the target
        //BaseTower p = null;
        //foreach (BaseTower possibleTarget in targets)
        //{
        //    if(p == null)
        //    {
        //        p = possibleTarget;
        //        target = possibleTarget.transform;
        //        towerTarget = possibleTarget;
        //    }

        //    if (p != null && Vector3.Distance(transform.position, possibleTarget.transform.position) <= Vector3.Distance(transform.position, p.transform.position))
        //    {
        //        target = possibleTarget.transform;
        //        towerTarget = possibleTarget;
        //    }
        //}
        #endregion

        return true;
    }
}
