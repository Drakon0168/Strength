using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword Swipe", menuName = "Abilities/Melee/Sword Swipe")]
public class SwordSwipe : Melee
{
    public override void Activate(Entity entity)
    {
        Debug.Log("Sword Swing");
    }
}
