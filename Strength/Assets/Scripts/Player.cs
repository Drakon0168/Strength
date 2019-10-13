using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool transitioning = false;
    private bool manaMajor = true;
    private bool physicalMajor = true;
    private int abilityNum;
    private bool buttonPressed = false;
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

    protected override void Awake()
    {
        base.Awake();

        animator.SetFloat("MissileChargeMult", 1 / world.abilityList.list[(int)Abilities.MagicMissile].castTime);
        animator.SetFloat("MissileAttackMult", 1 / world.abilityList.list[(int)Abilities.MagicMissile].activeTime);
        animator.SetFloat("MissileCooldownMult", 1 / world.abilityList.list[(int)Abilities.MagicMissile].coolDown);

        animator.SetFloat("SwipeChargeMult", 1 / world.abilityList.list[(int)Abilities.SwordSwipe].castTime);
        animator.SetFloat("SwipeAttackMult", 1 / world.abilityList.list[(int)Abilities.SwordSwipe].activeTime);
        animator.SetFloat("SwipeCooldownMult", 1 / world.abilityList.list[(int)Abilities.SwordSwipe].coolDown);
    }

    // Update is called once per frame
    protected override void Update()
    {
        Vector2 direction = Vector2.zero;
        if (!transitioning) {
            if (Input.GetKey(KeyCode.W))
            {
                state = State.Move;
                direction.y += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                state = State.Move;
                direction.x -= 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                state = State.Move;
                direction.y -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                state = State.Move;
                direction.x += 1;
            }

            direction.Normalize();
            Direction = direction;

            if (direction == Vector2.zero)
            {
                Speed = 0;
            }
            else
            {
                Speed = moveSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (world.wS == World.WorldState.Physical)
                {
                    if (stamina >= minorCost)
                    {
                        //Attack(world.abilityList.list[(int)Abilities.SwordSwipe]);
                        abilityNum = (int)Abilities.SwordSwipe;
                        animator.SetTrigger("SwordSwipe");
                        stamina -= minorCost;
                        mana += minorCost;
                    }
                }
                else
                {
                    if (mana >= minorCost)
                    {
                        buttonPressed = true;
                        //Attack(world.abilityList.list[(int)Abilities.MagicMissile]);
                        abilityNum = (int)Abilities.MagicMissile;
                        animator.SetTrigger("ShootMissile");
                        mana -= minorCost;
                        stamina += minorCost;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (world.wS == World.WorldState.Physical)
                {
                    if (stamina >= majorCost && physicalMajor)
                    {
                        //Attack(world.abilityList.list[(int)Abilities.SwordSlam]);
                        abilityNum = (int)Abilities.SwordSlam;
                        stamina -= majorCost;
                        mana += majorCost;
                        physicalMajor = false;
                    }
                }
                else
                {
                    if (mana >= majorCost && manaMajor)
                    {
                        buttonPressed = true;
                        //Attack(world.abilityList.list[(int)Abilities.NightmareBlade]);
                        Attack(world.abilityList.list[(int)Abilities.NightmareBlade]);
                        mana -= majorCost;
                        stamina += majorCost;
                        manaMajor = false;
                    }
                }
            }
        else if (Input.GetKeyDown(KeyCode.Space))
            {
                Defense();
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl)) {
                Transform();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {

            }
        }

        // Activates ability after getting list of attack targets (for physical0
        if (world.abilityList.list[abilityNum].damageType == Ability.DamageType.Magical)
        {
            if (buttonPressed)
            {
                Attack(world.abilityList.list[abilityNum]);
                buttonPressed = false;
            }
        }
        else
        {
            if (attackBoxActive)
            {
                Attack(world.abilityList.list[abilityNum]);
            }
        }

        ApplyForce((direction * moveSpeed) - Velocity);
        base.Update();
        BoolCheck();
        UpdateUI();
        transitioning = animator.GetBool("Transitioning");
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
            GetComponent<Animator>().SetTrigger("Transform");
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

        if (physicalMajor && world.wS == World.WorldState.Physical)
        {
            world.maxTop.color = new Color32(255, 255, 255, 255);
        }
        else if (manaMajor && world.wS == World.WorldState.Magical)
        {
            world.maxTop.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            world.maxTop.color = new Color32(0, 0, 0, 255);
        }

        if (physicalMajor && world.wS == World.WorldState.Magical)
        {
            world.maxBottom.color = new Color32(255, 255, 255, 255);
        }
        else if (manaMajor && world.wS == World.WorldState.Physical)
        {
            world.maxBottom.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            world.maxBottom.color = new Color32(0, 0, 0, 255);
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
