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
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            state = State.Move;
            direction.y += 1;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            state = State.Move;
            direction.x -= 1;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            state = State.Move;
            direction.y -= 1;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            state = State.Move;
            direction.x += 1;
        }

        direction.Normalize();
        ApplyForce((direction * moveSpeed) - Velocity);
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack(AbilityList.list[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack(AbilityList.list[1]);
        }
    }

    protected override void Attack(Ability ability)
    {
        ability.Activate(this);
    }
}
