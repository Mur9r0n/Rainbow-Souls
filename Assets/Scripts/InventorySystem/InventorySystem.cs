using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    #region Singleton
    public static InventorySystem Instance;

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

    public int m_inventorySpace = 10;
    
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

[System.Serializable]
public class HelmetSlot
{
    public Helmet m_helmet;

    public HelmetSlot(Helmet _helmet)
    {
        m_helmet = _helmet;
    }
}

