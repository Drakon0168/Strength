using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private float mana;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float maxMana;
    [SerializeField]
    private float maxStamina;
    [SerializeField]
    World world;
    [SerializeField]
    private float minorCost;
    [SerializeField]
    private float majorCost;
    private bool manaMajor = true;
    private bool physicalMajor = true;
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
            if (world.wS == World.WorldState.Physical)
            {
                if (stamina >= minorCost)
                {
                    Attack(AbilityList.list[0]);
                    stamina -= minorCost;
                    mana += minorCost;
                }
            }
            else
            {
                if (mana >= minorCost)
                {
                    Attack(AbilityList.list[2]);
                    mana -= minorCost;
                    stamina += minorCost;
                }
            }
            BoolCheck();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (world.wS == World.WorldState.Physical)
            {
                if (stamina >= majorCost && physicalMajor)
                {
                    Attack(AbilityList.list[1]);
                    stamina -= majorCost;
                    mana += majorCost;
                    physicalMajor = false;
                }
            }
            else
            {
                if (mana >= majorCost && manaMajor)
                {
                    Attack(AbilityList.list[3]);
                    mana -= majorCost;
                    stamina += majorCost;
                    manaMajor = false;
                }
            }
            BoolCheck();
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
        UpdateUI();
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

    private void UpdateUI()
    {
        world.health.fillAmount = health / maxHealth;
        if(world.wS == World.WorldState.Physical)
        {
            world.topBar.fillAmount = stamina / maxStamina;
            world.bottomBar.fillAmount = mana / maxMana;
        }
        else
        {
            world.topBar.fillAmount = mana / maxMana;
            world.bottomBar.fillAmount = stamina / maxStamina;
        }
    }

    private void BoolCheck()
    {
        if(stamina >= 100)
        {
            physicalMajor = true;
        }
        if(mana >= 100)
        {
            manaMajor = true;
        }
    }
}
public delegate void Transformation();
