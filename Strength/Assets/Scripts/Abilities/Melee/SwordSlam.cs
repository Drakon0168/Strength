using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword Slam", menuName = "Abilities/Melee/Sword Slam")]
public class SwordSlam : Melee
{
    public override void Activate(Entity entity)
    {
        Debug.Log("Sword Slam");
    }
}
