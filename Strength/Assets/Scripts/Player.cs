using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private int mana;
    private int stamina;
    private int maxMana;
    private int maxStamina;
    private State state = new State();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        Vector2 direction = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            state = State.Move;
            direction.y += 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            state = State.Move;
            direction.x -= 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            state = State.Move;
            direction.y -= 1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            state = State.Move;
            direction.x += 1;
        }

        direction.Normalize();
        ApplyForce((direction * moveSpeed) - Velocity);
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(World.wS == World.WorldState.Physical)
            {
                Attack(AbilityList.list[0]);
                stamina -= 10;
                mana += 10;
            }
            else
            {
                Attack(AbilityList.list[2]);
                mana -= 10;
                stamina += 10;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (World.wS == World.WorldState.Physical)
            {
                Attack(AbilityList.list[1]);
                stamina -= 60;
                mana += 60;
            }
            else
            {
                Attack(AbilityList.list[3]);
                mana -= 60;
                stamina += 60;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Defense();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift)){
            Transform();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    /// <summary>
    /// Dodge/Blocking
    /// </summary>
    private void Defense()
    {
        if(World.wS == World.WorldState.Physical)
        {
            //TODO: Add Block
        }
        else
        {
            //TODO: Add Dodge
        }
    }

    /// <summary>
    /// You use an ability to attack
    /// </summary>
    /// <param name="ability">The ability to be used</param>
    protected override void Attack(Ability ability)
    {
        ability.Activate(this);
    }

    /// <summary>
    /// Calls the world's transform method
    /// </summary>
    private void Transform()
    {
        World.ChangeWorld();
    }
}
