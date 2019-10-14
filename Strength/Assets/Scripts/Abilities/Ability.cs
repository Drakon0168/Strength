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

    public int knockback = 55;

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
               // entity.AttackList[i].ApplyForce(entity.Velocity.normalized * knockback);
            }
        }
        else
        {
            Enemy e = entity as Enemy;
            if (entity.AttackList.Contains(e.player))
            {
                e.player.TakeDamage(this);
            }
        }
        entity.AttackList.Clear();
    }

    public virtual void Activate(Entity entity, Vector2 pos)
    {
        Activate(entity);
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
