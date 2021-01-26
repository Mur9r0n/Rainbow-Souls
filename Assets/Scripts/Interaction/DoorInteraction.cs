using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : AInteractables
{
    public bool m_IsLocked = false;

    public override void Start()
    {
        base.Start();
        m_interactableType = InteractableType.Door;
        
        if (!GameManager.Instance.m_Doors.Contains(this))
            GameManager.Instance.m_Doors.Add(this);
    }

    public override void Update()
    {
        base.Update();
    }
    
    public override void Interact()
    {
        if (m_IsLocked == false)
            m_IsLocked = true;
        
        Debug.Log("Interact with " + gameObject.name);
        gameObject.SetActive(false);
    }
}
