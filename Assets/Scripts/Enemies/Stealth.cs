using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stealth : BaseEnemy {

    public int health = 10;
    public int sightRange = 10;
    public int range = 3;
    public int strength = 1;

    public override void Start()
    {
        base.Start();
        FindNearestArchwayOrBlessingScript();
    }

    public override void Update()
    {
        base.Update();

        // When stealth kills target, find a new one
        if(target == null)
        {
            FindNearestArchwayOrBlessingScript();
        }
    }

    // TODO Need to figure out how to deal damage to current target from range
    public void DoDamage()
    {

    }

    public override void OnDamage(int strength, DamageType type = DamageType.NORMAL)
    {
        health -= strength;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    private void FindNearestArchwayOrBlessingScript()
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

        // Go through all possible targets and choose the closest one as the target
        BaseTower p = null;
        foreach (BaseTower possibleTarget in targets)
        {
            if(p == null)
            {
                p = possibleTarget;
            }

            if (p != null && Vector3.Distance(transform.position, possibleTarget.transform.position) <= Vector3.Distance(transform.position, p.transform.position))
            {
                target = possibleTarget.transform;
            }
        }
    }
}
