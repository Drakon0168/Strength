using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic Missile", menuName = "Abilities/Ranged/Magic Missile")]
public class MagicMissile : Ranged
{
    public override void Activate(Entity entity)
    {
        Debug.Log("Magic Missile");
    }
}
