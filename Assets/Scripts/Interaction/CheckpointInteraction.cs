using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInteraction : AInteractables
{
    public bool m_IsActivated = false;
    
    public override void Start()
    {
        base.Start();

        m_interactableType = InteractableType.Checkpoint;
        
        if (!GameManager.Instance.m_CheckPoints.Contains(this))
            GameManager.Instance.m_CheckPoints.Add(this);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (m_IsActivated == false)
            m_IsActivated = true;
        
        Debug.Log("Interact with " + gameObject.name);

        UIManager.Instance.m_CheckPointMenuPanel.SetActive(true);
        UIManager.Instance.m_HUDPanel.SetActive(false);
        FindObjectOfType<PlayerMovement>().m_cinemachineFreeLook.enabled = false;
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<PlayerCombat>().enabled = false;
    }

}
