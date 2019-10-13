using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour
{
    public enum WorldState
    {
        Physical,
        Magical
    }
    private Canvas canvas;
    public WorldState wS = WorldState.Physical;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    /// <summary>
    /// Changes the world state and handles all that logic
    /// </summary>
    public void ChangeWorld()
    {
        if(wS == WorldState.Physical)
        {
            wS = WorldState.Magical;
            canvas.GetComponent<Image>().color = new Color(108, 47, 31);
            canvas.GetComponentInChildren<Animator>().SetTrigger("switchedToMagic");
            //TODO: Change screen color
        }
        else
        {
            wS = WorldState.Physical;
            canvas.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            canvas.GetComponentInChildren<Animator>().SetTrigger("switchedToStrength");
            //TODO: Change screen color
        }
    }
}
