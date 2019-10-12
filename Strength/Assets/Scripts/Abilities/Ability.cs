using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base ability class
/// </summary>
public abstract class Ability : ScriptableObject
{
    public Entity entity; // Entity using ability

    [SerializeField]
    protected float attackModifier;

    [SerializeField]
    float castTime;

    [SerializeField]
    float coolDown;

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

    /// <summary>
    /// Calculates ya damage
    /// </summary>
    /// <returns>The damage that's calculated</returns>
    public float CalcDamage()
    {
        return entity.BaseDamage * attackModifier;
    }
}
