using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    [SerializeField]
    protected int maxHealth = 0;
    protected int health = 0;

    protected Animator animator;

    /// <summary>
    /// Decrements health by the damage of the attack taking damage type into account
    /// </summary>
    /// <param name="attack"></param>
    public abstract void TakeDamage(Ability attack);

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
