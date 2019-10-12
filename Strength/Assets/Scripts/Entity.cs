using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of all abilities in the game
/// </summary>
public enum Abilities
{
    SwordSwipe,
    SwordSlam,
    MagicMissile,
    Lightning
}

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : Interactable
{
    [Header("Entity")]
    [SerializeField]
    protected float moveSpeed = 0;
    [SerializeField]
    protected float magicResistance = 0;
    [SerializeField]
    protected float physicalResistance = 0;
    [SerializeField]
    protected float baseDamage = 0;
    [SerializeField]
    protected List<Abilities> abilities;

    protected new Rigidbody2D rigidbody;
    protected Vector2 acceleration;

    /// <summary>
    /// The base damage of this entity used to affect ability damage
    /// </summary>
    public float BaseDamage { get; }

    /// <summary>
    /// The current direction of motion
    /// </summary>
    protected Vector2 Direction { get; set; }

    /// <summary>
    /// The current velocity of the entity
    /// </summary>
    public Vector2 Velocity
    {
        get { return rigidbody.velocity; }
        set { rigidbody.velocity = value; }
    }

    protected virtual void Update()
    {
        Velocity += acceleration * Time.deltaTime;
        acceleration = Vector2.zero;
    }

    /// <summary>
    /// Decrements health by the damage of the attack taking damage type into account
    /// </summary>
    /// <param name="attack">The attack that the entity was hit with</param>
    public override void TakeDamage(Ability attack)
    {
        //TODO decrement health based on the ability damage and damage type
    }

    /// <summary>
    /// Applies a force to the rigidbody
    /// </summary>
    /// <param name="force"></param>
    protected void ApplyForce(Vector2 force)
    {
        acceleration += force / rigidbody.mass;
    }

    /// <summary>
    /// Casts an ability using this entity's stats
    /// </summary>
    /// <param name="ability">The ability to cast</param>
    protected abstract void Attack(Abilities ability);
}
