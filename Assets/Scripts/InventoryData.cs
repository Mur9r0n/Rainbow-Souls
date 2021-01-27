using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<int> m_InventoryItems = new List<int>();
    public List<int> m_EquipmentItems = new List<int>();

    public InventoryData()
    {
        if (InventoryManager.Instance != null)
        {
            for (int i = 0; i < InventoryManager.Instance.Inventory.Length; i++)
            {
                m_InventoryItems.Add(InventoryManager.Instance.Inventory[i].GetID());
            }

            for (int i = 0; i < InventoryManager.Instance.Equipment.Length; i++)
            {
                m_EquipmentItems.Add(InventoryManager.Instance.Equipment[i].GetID());
            }
        }
    }
}