using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// List of all abilities in the game
/// </summary>
public enum Abilities
{
    SwordSwipe,
    SwordSlam,
    MagicMissile,
    NightmareBlade
}

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : Interactable
{
    [Header("Entity")]
    [SerializeField]
    protected float moveSpeed = 20f;
    [SerializeField]
    protected float magicResistance = .5f;
    [SerializeField]
    protected float physicalResistance = .5f;
    [SerializeField]
    protected float baseDamage = 1;
    [SerializeField]
    protected AbilityList abilities; // Use the ability list scriptableObject from the Abilities folder
    [SerializeField]
    protected float friction = .12f;
    [SerializeField]
    protected float maxFriction = .35f;
    [SerializeField]
    protected AttackCollider attackCollider;
    [SerializeField]
    protected bool hasAttackBox = false; // Needed fpr melee attacks
    [SerializeField]
    protected CinemachineVirtualCamera vcam;



    [HideInInspector]
    public bool attackBoxActive;

    //public List<Collider2D> attackList = new List<Collider2D>();
    protected new Rigidbody2D rigidbody;
    protected Vector2 acceleration;
    protected enum State
    {
        Move,
        Idle,
        Transform,
        Block,
        AttackCharge,
        Attacking,
        Cooldown
    }

    /// <summary>
    /// The base damage of this entity used to affect ability damage
    /// </summary>
    public float BaseDamage { get { return baseDamage; } }

    /// <summary>
    /// The current direction of motion
    /// </summary>
    protected virtual Vector2 Direction { get; set; }

    /// <summary>
    /// The current velocity of the entity
    /// </summary>
    public Vector2 Velocity
    {
        get { return rigidbody.velocity; }
        set { rigidbody.velocity = value; }
    }

    /// <summary>
    /// The players current location in 2D
    /// </summary>
    public Vector2 Location
    {
        get { return transform.position; }
    }

    public List<Entity> AttackList
    {
        get
        {
            return attackCollider.attackList;
        }
    }

    protected virtual void Update()
    {
        Velocity += Vector2.ClampMagnitude(-Velocity * friction, maxFriction) ;
        Velocity += acceleration * Time.deltaTime;
        acceleration = Vector2.zero;
    }

    /// <summary>
    /// Decrements health by the damage of the attack taking damage type into account
    /// </summary>
    /// <param name="attack">The attack that the entity was hit with</param>
    public override void TakeDamage(Ability attack)
    {
        if(attack.damageType == Ability.DamageType.Magical)
        {
            Health -= magicResistance * attack.CalcDamage();
        }
        else
        {
            Health -= physicalResistance * attack.CalcDamage();
        }

        if(vcam == null)
        {
            vcam = GameObject.FindGameObjectWithTag("cinemachine").GetComponent<CinemachineVirtualCamera>();
        }

        //shake the screen
        float shakeTime = 2f;

        Debug.Log("screenshake");
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 3;

        shakeTime -= Time.deltaTime;

        if(shakeTime <= 0)
        {
            Debug.Log("Screen Reset");
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        }
    }

    /// <summary>
    /// Applies a force to the rigidbody
    /// </summary>
    /// <param name="force"></param>
    public void ApplyForce(Vector2 force)
    {
        acceleration += force / rigidbody.mass;
    }

    /// <summary>
    /// Casts an ability using this entity's stats
    /// </summary>
    /// <param name="ability">The ability to cast</param>
    protected abstract void Attack(Ability ability);

    protected override void Awake()
    {
        base.Awake();

        if (hasAttackBox)
        {
            if (attackCollider != null)
                attackCollider.entity = this;
            else
                Debug.LogWarning(name + " does not have an AttackCollider and probably needs one.");
        }

        rigidbody = GetComponent<Rigidbody2D>();
    }
}
