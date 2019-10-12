using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public Entity entity;

    public float attackModifier;

    public DamageType damageType;

    public enum DamageType
    {
        Physical,
        Magical
    }

    public abstract void Activate();
}
