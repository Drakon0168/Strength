using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : ScriptableObject
{
    public enum WorldState
    {
        Physical,
        Magical
    }

    public WorldState wS = WorldState.Physical;

    /// <summary>
    /// Changes the world state and handles all that logic
    /// </summary>
    public void ChangeWorld()
    {
        if(wS == WorldState.Physical)
        {
            wS = WorldState.Magical;
            FindObjectOfType<Canvas>().GetComponent<Image>().color = new Color(108, 47, 31);
            //TODO: Change screen color
        }
        else
        {
            wS = WorldState.Physical;
            FindObjectOfType<Canvas>().GetComponent<Image>().color = new Color(0, 0, 0, 0);
            //TODO: Change screen color
        }
    }
}
