using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemInteraction : AInteractables
{
    private InventoryManager m_inventoryManager;

    [SerializeField] private InteractableItem m_interactableItem;

    public override void Start()
    {
        base.Start();
        m_inventoryManager = InventoryManager.Instance;
        m_interactableItem = GetComponent<InteractableItem>();

        if (!gameObject.TryGetComponent(out InteractableItem interactableItem))
        {
            Debug.Log("You forgot the InteractableItem Component in " + gameObject.name);
        }
    }
    
    //Weapons begin with 10XXX
    //ID 10000 - Son of a Bitch
    
    //Helmet begin with 20XXX
    //ID 20000 - Helmet of Doom
    
    //Chestplatt begin with 30XXX
    //ID 30000 - Armor of the Velvet Prince
    
    //Capes begin with 40XXX
    //ID 40000 - The Cape of Higher Destination
    
    //Consumables begin with 50XXX
    //ID 50000 - Health Potion
    
    //Undefined begin with 70XXX
    //ID 70000 - Bran
    
    //Key Objects begin with 90XXX
    //ID 90000 - Skeleton Key
    
    public override void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_IsInteractable = true;
            if (!m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Add(this);
                UIManager.Instance.ShowInteractionTooltip("Pick up "+ gameObject.name);
            }
        }
        else
        {
            m_IsInteractable = false;
            if (m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Remove(this);
                UIManager.Instance.HideInteractionTooltip();
            }
        }
    }
    
    public override void Interact()
    {
        m_inventoryManager.AddItem(m_interactableItem.m_Item);

        Debug.Log("Picked up " + m_interactableItem.m_Item.GetName());
        m_interactmanager.m_interactables.Remove(this);
        
        switch (ID)
        {
            case 10000:
            {
                Debug.Log("Interact with Son of a Bitch");
                break;
            }
            case 20000:
            {
                Debug.Log("Interact with Helmet of Doom");
                break;
            }
            case 30000:
            {
                Debug.Log("Interact with Armor of the Velvet Prince");
                break;
            }         
            case 40000:
            {
                Debug.Log("Interact with Cape of Higher Destination");
                break;
            }
            case 90000:
            {
                Debug.Log("Interact with Skeleton Key");
                break;
            }

            default:
            {
                Debug.Log("ID Not Implemented yet!");
                Debug.Log("FIX DAAD!");
                break;
            }
        }
        Destroy(gameObject);
        
        Destroy(gameObject);
        
        UIManager.Instance.UpdateSlotsUI();
    }
}
