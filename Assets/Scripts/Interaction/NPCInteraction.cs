﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : Interactables
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_IsInteractable = true;
            if (!m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Add(this);
                UIManager.Instance.ShowInteractionTooltip("Speak to "+ gameObject.name);
            }
        }
        else
        {
            m_IsInteractable = false;
            if (m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Remove(this);
                UIManager.Instance.HideInteractionTooltip();
            }
        }
    }
    
    public override void Interact()
    {
        Debug.Log("Interact with " + gameObject.name);
    }
}
