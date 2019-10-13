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
    public virtual void Activate(Entity entity)
    {
        this.entity = entity;
        foreach(Collider2D c in entity.attackList)
        {
            Interactable i = c.GetComponent<Interactable>();
            if(i != null)
            {
                i.TakeDamage(this);
            }
            else
            {
                throw new System.Exception();
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
