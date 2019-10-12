﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base ability class
/// </summary>
public abstract class Ability : ScriptableObject
{
    public Entity entity; // Entity using ability

    protected float attackModifier;

    public DamageType damageType; // Type of damage

    public enum DamageType
    {
        Physical,
        Magical
    }

    /// <summary>
    /// Activates abililty
    /// </summary>
    public abstract void Activate(Entity entity);

    public float CalcDamage()
    {
        return entity.BaseDamage * attackModifier;
    }
}
