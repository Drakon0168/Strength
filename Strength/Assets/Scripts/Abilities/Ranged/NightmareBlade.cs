using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nightmare Blade", menuName = "Abilities/Ranged/Nightmare Blade")]
public class NightmareBlade : Ranged
{
    public override void Activate(Entity entity)
    {
        Debug.Log("Nightmare Blade");
    }
}
