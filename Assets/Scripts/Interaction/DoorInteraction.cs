using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : AInteractables
{

    public override void Start()
    {
        base.Start();
        m_interactableType = InteractableType.Door;
        if(GlobalGameData.Instance.m_WorldData.m_OpenedDoors.Contains(ID))
        {
            gameObject.SetActive(false);
        }
    }

    public override void Update()
    {
        base.Update();
    }
    
    public override void Interact()
    {
        Debug.Log("Interact with " + gameObject.name);
        GameManager.Instance.m_OpenedDoors.Add(ID);
        
        gameObject.SetActive(false);
        
    }
}
