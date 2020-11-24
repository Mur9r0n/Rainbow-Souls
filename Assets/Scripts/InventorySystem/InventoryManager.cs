using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    #region Singleton

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    private int m_inventorySpace = 50;
    private int m_equipmentSpace = 4;

    public delegate void ItemPickedup();

    public List<Item> Inventory = new List<Item>();
    public List<Item> Equipment = new List<Item>();

    public void AddItem(Item _item)
    {
        if (Inventory.Count < m_inventorySpace)
        {
            Inventory.Add(_item);
        }
    }

    public void RemoveItem(Item _item)
    {
        Inventory.Remove(_item);
    }

    /// <summary>
    /// Equipment[0] Helmet Slot
    /// Equipment[1] Armor Slot
    /// Equipment[2] Weapon Slot
    /// Equipment[3] Cape Slot
    /// </summary>
    /// <param name="_equipmentSlot"></param>
    public void Equip(Item _item)
    {
        if (Equipment.Count < m_equipmentSpace)
        {
            Equipment.Add(_item);
        }
    }
    
    
}