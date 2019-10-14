using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{
    /// <summary>
    /// The ability that produced the 
    /// </summary>
    public Ranged ability;
    [SerializeField]
    private float flightSpeed;
    private Vector2 velocity;

    public override void TakeDamage(Ability attack)
    {
        Die();
    }

    /// <summary>
    /// The projectile dies when it collides with something and then damages it
    /// </summary>
    /// <param name="collision">The thing being damaged</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>() != null && collision.gameObject != ability.entity.gameObject)
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(ability);
            collision.gameObject.GetComponent<Entity>().ApplyForce(velocity.normalized * ability.knockback);
            Die();
        }
        Debug.Log(collision.gameObject);

    }

    public void Init(Ranged ability, Vector2 direction)
    {
        this.ability = ability;
        velocity = direction * flightSpeed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Die();
    }
}
