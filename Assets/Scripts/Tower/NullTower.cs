using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTower : BaseTower
{
    public override float range => 0;

    public override void DoDamage(BaseEnemy[] enemies)
    {
        
    }
}
