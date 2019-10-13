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
            Die();
        }
        Debug.Log(collision.gameObject);

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
        velocity = diff * flightSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Die();
    }
}
