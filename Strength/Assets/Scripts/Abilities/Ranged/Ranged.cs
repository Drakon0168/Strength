using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged : Ability
{
    [SerializeField]
    private GameObject projectile;

    public override void Activate(Entity entity)
    {
        base.Activate(entity);
        Projectile p = Instantiate(projectile).GetComponent<Projectile>();
        p.Init(this);
    }
}
