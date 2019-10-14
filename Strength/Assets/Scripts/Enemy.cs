using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Idle,
    Searching,
    MoveToAttack,
    Attacking
}

public class Enemy : Entity
{
    [Header("Enemy")]
    [SerializeField, Tooltip("The distance at which to start charging an attack")]
    private float attackRange;
    [SerializeField, Tooltip("The distance at which to start moving towards the player")]
    private float sightRange;
    [SerializeField]
    private float wanderDistance;
    [SerializeField, Tooltip("The attack that this enemy uses")]
    private Abilities attack;

    static World world;

    public Player player;

    public int projectilesLaunched = 0;
    private EnemyStates currentState;

    protected override void Awake()
    {
        base.Awake();

        if (world == null)
        {
            world = FindObjectOfType<World>();
        }

        player = FindObjectOfType<Player>();
        currentState = EnemyStates.Searching;
    }

    protected override void Update()
    {
        //Debug.Log(currentState);

        switch (currentState)
        {
            case EnemyStates.Attacking:
                StopMotion();
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    currentState = EnemyStates.Searching;
                break;
            case EnemyStates.Idle:
                StopMotion();
                break;
            case EnemyStates.Searching:
                Vector2 distanceToPlayer = transform.position - player.transform.position;
                
                if(distanceToPlayer.sqrMagnitude < sightRange * sightRange)
                {
                    currentState = EnemyStates.MoveToAttack;
                }
                else
                {
                    //Wander
                    //float vRandom = Random.value;
                    //Vector2 target = new Vector2(wanderDistance * vRandom, wanderDistance * (1 - vRandom));
                    //ApplyForce(target);
                }
                break;
            case EnemyStates.MoveToAttack:
                Vector2 targetDirection = player.transform.position - transform.position;
                float targetDistance = targetDirection.magnitude;
                
                if(targetDistance < attackRange)
                {
                    //TODO: Start the attack
                    currentState = EnemyStates.Attacking;
                    projectilesLaunched = 0;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    ApplyForce((targetDirection / targetDistance) * moveSpeed);
                    animator.SetFloat("Speed", moveSpeed);
                    animator.SetFloat("DirectionX", targetDirection.x);
                    animator.SetFloat("DirectionY", targetDirection.y);
                }
                break;
        }
        Attack(world.abilityList.list[(int)attack]);

        base.Update();
    }

    /// <summary>
    /// Applies a movement force to stop motion
    /// </summary>
    private void StopMotion()
    {
        if(Velocity != Vector2.zero)
        {
            ApplyForce(-Velocity.normalized * moveSpeed);
        }
    }

    protected override void Attack(Ability ability)
    {
        if (ability is Ranged)
        {
            projectilesLaunched++;
            if (projectilesLaunched <= 1)
            {
                ability.Activate(this, player.transform.position);
            }
        }
        else
        {
            if (attackBoxActive)
            {
                ability.Activate(this);
            }
        }
    }
}