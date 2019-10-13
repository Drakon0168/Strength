using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)), RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    [SerializeField]
    protected float maxHealth = 10;
    [SerializeField]
    protected float health = 10;

    protected Collider2D collider2;

    /// <summary>
    /// The current health of the character
    /// </summary>
    public float Health
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
        collider2 = GetComponent<Collider2D>();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
