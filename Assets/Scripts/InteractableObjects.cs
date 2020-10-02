using System;
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
    public bool m_Dissolving = false;
    public Material m_Material;

    void Start()
    {
        InteractManager.Instance.AddToList(this);
        if (m_Type == Type.Chest)
        {
            m_Material = GetComponent<Renderer>().material;
        }
    }

    private void Update()
    {
        //Still Testing
        #region Testing
        if (m_Dissolving)
        {
            float temp = m_Material.GetFloat("Vector1_8DEAC01A");
            if (temp <= 1)
            {
                m_Material.SetFloat("Vector1_8DEAC01A", temp += Time.deltaTime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion
            
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
                m_Dissolving = true;
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