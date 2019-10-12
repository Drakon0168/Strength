using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilities
{

}

public abstract class Entity : Interactable
{
    [Header("Entity")]
    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private float magicResistance = 0;
    [SerializeField]
    private float physicalResistance = 0;
    [SerializeField]
    private float baseDamage = 0;
    [SerializeField]
    private List<Abilities> abilities;

    /// <summary>
    /// The current direction of motion
    /// </summary>
    private Vector2 Direction { get; set; }

    private float Velocity;
}
