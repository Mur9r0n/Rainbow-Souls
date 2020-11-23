using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : Interactables
{
    public bool m_canInteractNPC = false;
    void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_canInteractNPC = true;
        }
        else
            m_canInteractNPC = false;
    }
}
