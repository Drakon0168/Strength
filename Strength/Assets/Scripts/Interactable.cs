using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable")]
    [SerializeField]
    private int maxHealth = 0;
    private int health = 0;

    private Animator animator;

    /// <summary>
    /// Decrements health by the damage of the attack taking damage type into account
    /// </summary>
    /// <param name="attack"></param>
    public abstract void TakeDamage(Ability attack);

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
