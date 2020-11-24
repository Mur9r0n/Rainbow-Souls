using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehaviours : MonoBehaviour
{
    private InventorySlot m_inventorySlot;
    private InventoryManager m_inventoryManager;


    private void Start()
    {
        m_inventorySlot = GetComponentInParent<InventorySlot>();
        m_inventoryManager = InventoryManager.Instance;
    }

    public void EquipItem()
    {
        Debug.Log("BLA");
        if (m_inventorySlot.m_item != null)
        {
            if (m_inventorySlot.m_item.Type == Item.ItemType.Helmet)
            {
                m_inventoryManager.Equip(m_inventorySlot.m_item);
            }

            if (m_inventorySlot.m_item.Type == Item.ItemType.Armor)
            {
                m_inventoryManager.Equip(m_inventorySlot.m_item);
            }

            if (m_inventorySlot.m_item.Type == Item.ItemType.Weapon)
            {
                m_inventoryManager.Equip(m_inventorySlot.m_item);
                m_inventoryManager.RemoveItem(m_inventorySlot.m_item);
                
                for (int i = 0; i < InventoryManager.Instance.Equipment.Count; i++)
                {
                    UIManager.Instance.m_equipmentSlots[i].m_Icon.sprite = InventoryManager.Instance.Equipment[i].m_icon;
                }

            }

            if (m_inventorySlot.m_item.Type == Item.ItemType.Cape)
            {
                m_inventoryManager.Equip(m_inventorySlot.m_item);
            }
        }
    }
}
