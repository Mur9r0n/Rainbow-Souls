using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Item m_item;
    [SerializeField]
    private Image m_icon;

    public void AddItem(Item _item)
    {
        m_item = _item;

        m_icon.sprite = m_item.Icon;
        m_icon.enabled = true;
    }

    public void RemoveItem()
    {
        m_item = null;

        m_icon.sprite = null;
        m_icon.enabled = false;
    }

    public void UseItem()
    {
        if (m_item !=null)
        {
            m_item.Use();
        }
    }
}
