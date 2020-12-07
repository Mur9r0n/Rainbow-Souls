using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehaviours : MonoBehaviour
{
    private int m_inventorySlot = -1;
    private int m_equipmentSlot = -1;
    
    //TODO ITEM SHOWCASE
    public void ShowChoicePanelInventory(int _inventorySlot)
    {
        m_equipmentSlot = -1;
        m_inventorySlot = _inventorySlot;
        UIManager.Instance.m_EquipmentChoicePanel.SetActive(false);
        UIManager.Instance.m_InventoryChoicePanel.SetActive(true);
        UIManager.Instance.m_InventoryChoicePanel.transform.position = UIManager.Instance.m_InventorySlots[_inventorySlot].transform.position;
    }
    
    public void ShowChoicePanelEquipment(int _equipmentSlot)
    {
        m_inventorySlot = -1;
        m_equipmentSlot = _equipmentSlot;
        UIManager.Instance.m_InventoryChoicePanel.SetActive(false);
        UIManager.Instance.m_EquipmentChoicePanel.SetActive(true);
        UIManager.Instance.m_EquipmentChoicePanel.transform.position = UIManager.Instance.m_EquipmentSlots[_equipmentSlot].transform.position;
    }

    public void UseSlot()
    {
        //TODO PLayer stat changes
        switch (InventoryManager.Instance.Inventory[m_inventorySlot].GetType())
        {
            case Item.ItemType.Helmet:
            {
                InventoryManager.Instance.Equipment[0] = InventoryManager.Instance.Inventory[m_inventorySlot];
                break;
            }
            case Item.ItemType.Weapon:
            {
                InventoryManager.Instance.Equipment[1] = InventoryManager.Instance.Inventory[m_inventorySlot];
                break;
            }
            case Item.ItemType.Armor:
            {
                InventoryManager.Instance.Equipment[2] = InventoryManager.Instance.Inventory[m_inventorySlot];
                break;
            }
            case Item.ItemType.Cape:
            {
                InventoryManager.Instance.Equipment[3] = InventoryManager.Instance.Inventory[m_inventorySlot];
                break;
            }

            default:
            {
                InventoryManager.Instance.Inventory[m_inventorySlot].Use();
                break;
            }
        }

        InventoryManager.Instance.Inventory[m_inventorySlot] = InventoryManager.Instance.m_placeHolder;
        UIManager.Instance.UpdateSlotsUI();
        UIManager.Instance.m_InventoryChoicePanel.SetActive(false);
    }

    public void DropSlot()
    {
        //TODO: Instantiate the dropped item
        if (InventoryManager.Instance.Inventory[m_inventorySlot] != InventoryManager.Instance.m_placeHolder)
        {
            InventoryManager.Instance.Inventory[m_inventorySlot] = InventoryManager.Instance.m_placeHolder;
            UIManager.Instance.UpdateSlotsUI();
            UIManager.Instance.m_InventoryChoicePanel.SetActive(false);
        }
    }

    public void Cancel()
    {
        if (UIManager.Instance.m_EquipmentChoicePanel.activeSelf)
        {
            UIManager.Instance.m_EquipmentChoicePanel.SetActive(false);
        }

        if (UIManager.Instance.m_InventoryChoicePanel.activeSelf)
        {
            UIManager.Instance.m_InventoryChoicePanel.SetActive(false);
        }
    }

    public void Unequip()
    {
        //TODO Inventory full message ------- Stats changes
        for (int i = 0; i < InventoryManager.Instance.Inventory.Length; i++)
        {
            if (InventoryManager.Instance.Inventory[i].GetType() == Item.ItemType.Empty)
            {
                InventoryManager.Instance.Inventory[i] = InventoryManager.Instance.Equipment[m_equipmentSlot];
                InventoryManager.Instance.Equipment[m_equipmentSlot] = InventoryManager.Instance.m_placeHolder;
                break;
            }
        }
        
        UIManager.Instance.UpdateSlotsUI();
        UIManager.Instance.m_EquipmentChoicePanel.SetActive(false);
    }
}
