using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interactables
{
    public bool m_canInteractDoor = false;
    void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_canInteractDoor = true;
        }
        else
            m_canInteractDoor = false;
    }
}
