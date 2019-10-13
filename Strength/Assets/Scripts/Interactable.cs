using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    [SerializeField]
    protected float maxHealth = 0;
    [SerializeField]
    protected float health = 0;

    /// <summary>
    /// The current health of the character
    /// </summary>
    public int Health
    {
        get { return health; }
        set
        {
            health = value;

            if(health > maxHealth)
            {
                health = maxHealth;
            }
            else if (health <= 0)
            {
                health = 0;
                Die();
            }
        }
    }

    protected Animator animator;

    /// <summary>
    /// Decrements health by the damage of the attack taking damage type into account
    /// </summary>
    /// <param name="attack">The attack that the entity was hit with</param>
    public abstract void TakeDamage(Ability attack);

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
