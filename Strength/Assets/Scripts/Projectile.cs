using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{
    /// <summary>
    /// The ability that produced the 
    /// </summary>
    public Ranged ability;
    Vector2 velocity;

    public override void TakeDamage(Ability attack)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// The projectile dies when it collides with something and then damages it
    /// </summary>
    /// <param name="collision">The thing being damaged</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Entity>() != null)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(ability);
            Destroy(this.gameObject);
        }
        
    }

    protected override void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(Ranged ability)
    {
        this.ability = ability;
        transform.position = ability.entity.transform.position;

        Vector2 mP = Input.mousePosition;
        mP = Camera.main.ScreenToWorldPoint(mP);
        Vector2 diff = mP - ability.entity.Location;
        float angle = Mathf.Atan2(diff.y, diff.x);
        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

        diff.Normalize();
        velocity = diff * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y );
    }

    private void OnBecameInvisible()
    {
        Die();
    }
}
