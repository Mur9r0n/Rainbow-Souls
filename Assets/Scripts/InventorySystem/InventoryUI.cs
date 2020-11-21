using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private InventoryManager m_inventoryManager;

    [SerializeField] private GameObject m_itemGridParent;

    private InventorySlot[] m_inventorySlots;
    
    // Start is called before the first frame update
    void Start()
    {
        m_inventoryManager = InventoryManager.Instance;
        m_inventoryManager.itemPickedupcallback += UpdateSlotsUI;

        m_inventorySlots = m_itemGridParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlotsUI()
    {
        for (int i = 0; i < m_inventorySlots.Length; i++)
        {
            //Are there more items to add?
            if (i < m_inventoryManager.Inventory.Count)
            {
                m_inventorySlots[i].AddItem(m_inventoryManager.Inventory[i]);   
            }
            //if there are no more items to add, then clear the slots
            else
                m_inventorySlots[i].RemoveItem();
        }
    }
}
