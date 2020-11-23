using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interactables
{
    public bool m_canInteractItems = false;
    void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_canInteractItems = true;
        }
        else
            m_canInteractItems = false;
    }
}
