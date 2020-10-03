using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventorySystem : ScriptableObject
{
    public List<InventorySlot> InventoryContainer = new List<InventorySlot>();

    public void AddItem(Item _item)
    {
        // bool hasItem = false;
        // for (int i = 0; i < InventoryContainer.Count; i++)
        // {
        //     if (InventoryContainer[i].m_item == _item )
        //     {
        //         InventoryContainer[i].AddAmount(_amount);
        //         hasItem = true;
        //         break;
        //     }
        //
        //     if (!hasItem)
        //     {
        //     }
        // }
                InventoryContainer.Add(new InventorySlot(_item));
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item m_item;
    //public int m_amount;

    public InventorySlot(Item _item)
    {
        m_item = _item;
        //m_amount = _amount;
    }

    // public void AddAmount(int _value)
    // {
    //     m_amount += _value;
    // }
}
