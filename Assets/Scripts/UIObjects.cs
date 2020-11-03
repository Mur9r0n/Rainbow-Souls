using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UIObjects : MonoBehaviour
{
    public CanvasGroup m_InventoryPanel;

    private void Start()
    {
        m_InventoryPanel = GetComponentInChildren<CanvasGroup>();
    }

    public void ShowInventory()
    {
        if (m_InventoryPanel.alpha ==0)
        {
            m_InventoryPanel.alpha = 1;
            m_InventoryPanel.interactable = true;
        }
        else
        {
            m_InventoryPanel.alpha = 0;
            m_InventoryPanel.interactable = false;
        }
    }
}
