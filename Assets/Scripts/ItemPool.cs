using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance { get; private set; }

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

    public List<Item> m_ItemPool = new List<Item>();

    private void Start()
    {
        // for (int i = 0; i < m_ItemPool.Count; i++)
        // {
        //     if (m_ItemPool[i].m_ID == 50000)
        //     {
        //         InventoryManager.Instance.Inventory[0] = m_ItemPool[i];
        //     }
        // }
        //
        // for (int i = 0; i < m_ItemPool.Count; i++)
        // {
        //     if (m_ItemPool[i].m_ID == 30000)
        //     {
        //         InventoryManager.Instance.Equipment[2] = m_ItemPool[i];
        //         break;
        //     }
        // }
        
    }

}
