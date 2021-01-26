using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInteraction : AInteractables
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
                UIManager.Instance.ShowInteractionTooltip("Interact with "+ gameObject.name);
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

        UIManager.Instance.m_CheckPointMenuPanel.SetActive(true);
        UIManager.Instance.m_HUDPanel.SetActive(false);
        FindObjectOfType<PlayerMovement>().m_cinemachineFreeLook.enabled = false;
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<PlayerCombat>().enabled = false;
    }

}
