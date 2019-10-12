using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Interactable
{
    /// <summary>
    /// The ability that produced the 
    /// </summary>
    public Ranged ability;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
