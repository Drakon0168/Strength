﻿using System.Collections;
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
    [SerializeField]
    private GameObject hud;

    private Animator canvasAnimator;
    private Animator hudAnimator;

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
        hudAnimator = hud.GetComponent<Animator>();
    }

    /// <summary>
    /// Changes the world state and handles all that logic
    /// </summary>
    public void ChangeWorld()
    {
        if(wS == WorldState.Physical)
        {
            wS = WorldState.Magical;
            hudAnimator.SetTrigger("switchedToMagic");
            canvasAnimator.SetTrigger("tintMagic");
        }
        else
        {
            wS = WorldState.Physical;
            hudAnimator.SetTrigger("switchedToStrength");
            canvasAnimator.SetTrigger("tintStrength");
        }
    }
}
