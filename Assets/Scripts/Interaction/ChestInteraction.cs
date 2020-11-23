using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : Interactables
{
    public bool m_canInteractChest = false;
    
    void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_canInteractChest = true;
        }
        else
            m_canInteractChest = false;
    }
}
