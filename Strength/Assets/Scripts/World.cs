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
    public AbilityList abilityList;
    [SerializeField]
    protected Player player;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        player.transformation += ChangeWorld;
    }

    /// <summary>
    /// Changes the world state and handles all that logic
    /// </summary>
    public void ChangeWorld()
    {
        if(wS == WorldState.Physical)
        {
            wS = WorldState.Magical;
            canvas.GetComponent<Image>().color = new Color32(108, 47, 31, 119);
            canvas.GetComponentInChildren<Animator>().SetTrigger("switchedToMagic");
            //TODO: Change screen color
        }
        else
        {
            wS = WorldState.Physical;
            canvas.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            canvas.GetComponentInChildren<Animator>().SetTrigger("switchedToStrength");
            //TODO: Change screen color
        }
    }
}
