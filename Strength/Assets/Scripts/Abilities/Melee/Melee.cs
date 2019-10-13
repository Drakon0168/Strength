using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : Ability
{
    Collider2D entityHit;

    public override void Activate(Entity entity)
    {
        base.Activate(entity);
    }
}
