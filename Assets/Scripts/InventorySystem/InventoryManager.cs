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

    public List<Item> Inventory = new List<Item>();
    public List<Item> Equipment = new List<Item>();
    
    [Header("Default Equipment")]
    [SerializeField] private Item m_helmetdefault; 
    [SerializeField] private Item m_armordefault; 
    [SerializeField] private Item m_weapondefault; 
    [SerializeField] private Item m_capedefault; 

    // private void Start()
    // {
    //     Equipment.Add(m_helmetdefault);
    //     Equipment.Add(m_armordefault);
    //     Equipment.Add(m_weapondefault);
    //     Equipment.Add(m_capedefault);
    // }

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
    // public void Equip(Item _item, Item.ItemType _itemType)
    // {
    //     if (Equipment.Count < m_equipmentSpace)
    //     {
    //         if (_itemType == Item.ItemType.Helmet)
    //         {
    //             Equipment.Insert(0, _item);
    //         }
    //         else if (_itemType == Item.ItemType.Armor)
    //         {
    //             Equipment.Insert(1, _item);
    //         }
    //         else if (_itemType == Item.ItemType.Weapon)
    //         {
    //             Equipment.Insert(2, _item);
    //         }
    //         else if (_itemType == Item.ItemType.Cape)
    //         {
    //             Equipment.Insert(3, _item);
    //         }
    //     }
    // }

    public void Equip(Item _item)
    {
        Equipment.Add(_item);
    }
}