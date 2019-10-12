using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged : Ability
{
    [SerializeField]
    private GameObject projectile;

    public override void Activate(Entity entity)
    {
        Instantiate(projectile, entity.gameObject.transform);
    }
}
