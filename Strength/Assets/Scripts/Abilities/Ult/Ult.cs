using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ult", menuName = "Abilities/Ult/Ult")]
public class Ult : Ability
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Activate(Entity entity)
    {
        base.Activate(entity);

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemies)
        {
            e.TakeDamage(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
