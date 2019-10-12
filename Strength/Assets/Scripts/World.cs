using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World
{
    public enum WorldState
    {
        Physical,
        Magical
    }

    public static WorldState wS = WorldState.Physical;

    /// <summary>
    /// Changes the world state and handles all that logic
    /// </summary>
    public static void ChangeWorld()
    {
        if(wS == WorldState.Physical)
        {
            wS = WorldState.Magical;
            //TODO: Change screen color
        }
        else
        {
            wS = WorldState.Physical;
            //TODO: Change screen color
        }
    }
}
