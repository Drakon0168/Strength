using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Entity entity;
    public List<Entity> attackList = new List<Entity>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null)
        {
            Entity other = collision.GetComponent<Entity>();
            if (collision.gameObject != entity.gameObject && !attackList.Contains(other))
            {
                attackList.Add(other);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null)
        {
            Entity other = collision.GetComponent<Entity>();
            if (attackList.Contains(other))
            {
                attackList.Remove(other);
            }
        }
    }
}
