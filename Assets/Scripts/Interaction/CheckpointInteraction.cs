using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInteraction : Interactables
{
    public bool m_canInteractCheckpoint = false;
    void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_canInteractCheckpoint = true;
        }
        else
            m_canInteractCheckpoint = false;
    }
}
