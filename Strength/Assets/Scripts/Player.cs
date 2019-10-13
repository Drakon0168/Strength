using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private int mana;
    [SerializeField]
    private int stamina;
    [SerializeField]
    private int maxMana;
    [SerializeField]
    private int maxStamina;
    [SerializeField]
    World world;
    private State state = new State();
    public Transformation transformation;

    protected override Vector2 Direction
    {
        get
        {
            return new Vector2(animator.GetFloat("DirectionX"), animator.GetFloat("DirectionY"));
        }
        set
        {
            animator.SetFloat("DirectionX", value.x);
            animator.SetFloat("DirectionY", value.y);
        }
    }

    protected float Speed
    {
        get { return animator.GetFloat("Speed"); }
        set { animator.SetFloat("Speed", value); }
    }

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
        Direction = direction;

        if(direction == Vector2.zero)
        {
            Speed = 0;
        }
        else
        {
            Speed = moveSpeed;
        }

        ApplyForce((direction * moveSpeed) - Velocity);
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(world.wS == World.WorldState.Physical)
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
            if (world.wS == World.WorldState.Physical)
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
        else if (Input.GetKeyDown(KeyCode.LeftControl)){
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
        if(world.wS == World.WorldState.Physical)
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
    public void Transform()
    {
        if(transformation != null)
        {
            transformation();
        }
    }
}
public delegate void Transformation();
