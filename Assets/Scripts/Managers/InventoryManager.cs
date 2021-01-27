using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }


    public Item m_placeHolder = null;

    [Header("Inventory")] public int m_inventorySpace;
    public Item[] Inventory;

    [Header("Equipment")] public int m_equipmentSpace;
    public Item[] Equipment;

    #region Singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Inventory = new Item[m_inventorySpace];
        Equipment = new Item[m_equipmentSpace];
        
        for (int i = 0; i < m_inventorySpace; i++)
        {
            Inventory[i] = m_placeHolder;
        }

        for (int i = 0; i < m_equipmentSpace; i++)
        {
            Equipment[i] = m_placeHolder;
        }
    }

    #endregion


    private void Start()
    {
    }

    public void AddItem(Item _item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == m_placeHolder)
            {
                Inventory[i] = _item;
                UIManager.Instance.UpdateSlotsUI();
                break;
            }
        }
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == _item)
            {
                Inventory[i] = m_placeHolder;
                UIManager.Instance.UpdateSlotsUI();
                break;
            }
        }
    }

    public void Equip(Item _item, int _equipmentSlot)
    {
    }
}