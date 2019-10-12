using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of abilities entities can use
/// </summary>
[CreateAssetMenu(menuName = "Abilitie/Ability List")]
public class AbilityList : ScriptableObject
{
    public List<Ability> abilities;
}
