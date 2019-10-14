using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged : Ability
{
    [SerializeField]
    private GameObject projectile;

    /// <summary>
    /// Activates Projectile for player
    /// </summary>
    /// <param name="entity"></param>
    public override void Activate(Entity entity)
    {
        base.Activate(entity);

        Vector2 mP = Input.mousePosition;
        mP = Camera.main.ScreenToWorldPoint(mP);

        SpawnProjectile(entity, mP);
    }

    /// <summary>
    /// Activates projectile for ranged unit
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="pos">Target location</param>
    public override void Activate(Entity entity, Vector2 pos)
    {
        base.Activate(entity);
        SpawnProjectile(entity, pos);
    }

    void SpawnProjectile(Entity entity, Vector3 pos)
    {
        Projectile p = Instantiate(projectile).GetComponent<Projectile>();

        p.transform.position = entity.transform.position;

        Vector2 diff = (Vector2)pos - entity.Location;
        float angle = Mathf.Atan2(diff.y, diff.x);
        p.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

        diff.Normalize();
        p.Init(this, diff);
    }
}
