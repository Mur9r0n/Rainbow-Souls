using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public enum Type
    {
        Checkpoint,
        Chest,
        Door,
        Item,
        NPC
    };

    public Type m_Type;
    void Start()
    {
        InteractManager.Instance.AddToList(this);
    }

    public void Use()
    {
        //TODO:Logic
        switch (m_Type)
        {
            case Type.Checkpoint:
            
                Debug.Log("Activate Checkpoint");
                break;
            
            case Type.Chest:
            
                Debug.Log("Open Chest");
                InteractManager.Instance.RemoveFromList(this);
                break;
            
            case Type.Door:
            
                Debug.Log("Open Door");
                break;
            
            case Type.Item:
            
                Debug.Log("Pick up Item");
                InteractManager.Instance.RemoveFromList(this);
                break;
            
            case Type.NPC:
                Debug.Log("Talk To NPC");
                break;

        }
        
    }
}
