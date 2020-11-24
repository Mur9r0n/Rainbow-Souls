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

    public delegate void ItemPickedup();

    public List<Item> Inventory = new List<Item>();
    // public List<EquipmentSlot> Equipment = new List<EquipmentSlot>();

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
}

// [System.Serializable]
// public class EquipmentSlot
// {
//     public Helmet m_helmet;
//
//     public EquipmentSlot(Helmet _helmet)
//     {
//         m_helmet = _helmet;
//     }
// }