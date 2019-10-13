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
    protected float attackModifier = 1f;

    public float castTime;

    public float activeTime;

    public float coolDown;

    public DamageType damageType; // Type of damage

    public enum DamageType
    {
        Physical,
        Magical
    }

    /// <summary>
    /// Activates abililty
    /// </summary>
    /// <param name="entity">Entity using the ability</param>
    public virtual void Activate(Entity entity)
    {
        this.entity = entity;
        if (entity is Player)
        {
            for (int i = 0; i < entity.AttackList.Count; i++)
            {
                entity.AttackList[i].TakeDamage(this);
            }
            entity.AttackList.Clear();
        }
        else
        {
            Enemy e = entity as Enemy;
            for (int i = 0; i < entity.AttackList.Count; i++)
            {
                if (entity.AttackList[i] is Player)
                {
                    
                }
            }
        }
    }

    /// <summary>
    /// Calculates ya damage
    /// </summary>
    /// <returns>The damage that's calculated</returns>
    public float CalcDamage()
    {
        return entity.BaseDamage * attackModifier;
    }
}
