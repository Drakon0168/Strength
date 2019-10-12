using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of abilities entities can use
/// </summary>
[CreateAssetMenu(menuName = "Abilitie/Ability List")]
public class AbilityList : ScriptableObject, ISerializationCallbackReceiver
{
    public static List<Ability> list;

    [SerializeField]
    private List<Ability> _list;

    public void OnAfterDeserialize()
    {
        list = _list;
    }

    public void OnBeforeSerialize()
    {
        _list = list;
    }
}
