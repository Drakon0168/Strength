using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement : NonInteractable
{
    /// <summary>
    /// The thing the UI element is monitoring for changes
    /// </summary>
    protected GameObject monitored;

    /// <summary>
    /// Call this on an element-specific trigger
    /// </summary>
    protected abstract void OnChange();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
