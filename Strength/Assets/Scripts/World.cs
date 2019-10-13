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
    private Animator canvasAnimator;

    public WorldState wS = WorldState.Physical;
    public AbilityList abilityList;
    [SerializeField]
    protected Player player;
    [SerializeField]
    public Image health;
    [SerializeField]
    public Image topBar;
    [SerializeField]
    public Image bottomBar;
    [SerializeField]
    public Image maxTop;
    [SerializeField]
    public Image maxBottom;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        player.transformation += ChangeWorld;
        canvasAnimator = canvas.GetComponent<Animator>();
    }

    /// <summary>
    /// Changes the world state and handles all that logic
    /// </summary>
    public void ChangeWorld()
    {
        if(wS == WorldState.Physical)
        {
            wS = WorldState.Magical;
            canvas.GetComponentInChildren<Animator>().SetTrigger("switchedToMagic");
            canvasAnimator.SetTrigger("tintMagic");
            //TODO: Change screen color
        }
        else
        {
            wS = WorldState.Physical;
            canvas.GetComponentInChildren<Animator>().SetTrigger("switchedToStrength");
            canvasAnimator.SetTrigger("tintStrength");
            //TODO: Change screen color
        }
    }
}
