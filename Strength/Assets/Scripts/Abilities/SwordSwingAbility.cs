﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword Swing", menuName = "Abilitie/Sword Swing")]
public class SwordSwingAbility : Melee
{
    public override void Activate()
    {
        Debug.Log("Sword Swung");
    }
}