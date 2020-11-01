using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public InventorySystem m_inventorySystem;
    public InteractableItem m_interactableItem;
    
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

    [SerializeField] private GameObject m_lootPrefab;

    void Start()
    {
        m_inventorySystem = FindObjectOfType<InventorySystem>();
        m_interactableItem = GetComponent<InteractableItem>();
        
        InteractManager.Instance.AddToList(this);
        if (m_Type == Type.Chest)
        {
            m_Material = GetComponent<Renderer>().material;
        }
        
        //TODO: Löschen beim Builden
        if (m_Type == Type.Item)
        {
            if (!gameObject.TryGetComponent(out InteractableItem interactableItem))
            {
                throw new MissingComponentException("You forgot the InteractableItems Component in " + gameObject.name);
            }
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
                Instantiate(m_lootPrefab, transform.position, Quaternion.identity);
                InteractManager.Instance.RemoveFromList(this);
                m_Dissolving = true;
                break;

            case Type.Door:

                Debug.Log("Open Door");
                break;

            case Type.Item:

                InteractManager.Instance.RemoveFromList(this);
                m_inventorySystem.AddItem(m_interactableItem.m_Item);
                Debug.Log("Picked up " + m_interactableItem.m_Item.Name);
                Destroy(gameObject);
                break;

            case Type.NPC:
                Debug.Log("Talk To NPC");
                break;
        }
    }
}