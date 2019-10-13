﻿using System.Collections;
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

    private Vector2 targetPosition;

    private EnemyStates currentState;

    protected override void Update()
    {
        switch (currentState)
        {
            case EnemyStates.Idle:
            case EnemyStates.Attacking:
                StopMotion();
                break;
            case EnemyStates.Searching:
                float vRandom = Random.value;
                Vector2 target = new Vector2(wanderDistance * vRandom, wanderDistance * (1 - vRandom));
                ApplyForce(target);
                break;
            case EnemyStates.MoveToAttack:
                Vector2 targetDirection = targetPosition - (Vector2)transform.position;
                float targetDistance = targetDirection.magnitude;
                
                if(targetDistance < attackRange)
                {
                    //TODO: Start the attack
                    currentState = EnemyStates.Attacking;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    ApplyForce((targetDirection / targetDistance) * moveSpeed);
                }
                break;
        }

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
        throw new System.NotImplementedException();
    }
}