using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    #region Singleton
    public static InteractManager Instance;

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
    
    private RenderInteractionUI m_renderInteractionUI;
    private ChestInteraction m_chestInteraction;
    private CheckpointInteraction m_checkpointInteraction;
    private DoorInteraction m_doorInteraction;
    private ItemInteraction m_itemInteraction;
    private NPCInteraction m_npcInteraction;
    
    private void Start()
    {
        m_renderInteractionUI = FindObjectOfType<RenderInteractionUI>();
        m_chestInteraction = FindObjectOfType<ChestInteraction>();
        m_checkpointInteraction = FindObjectOfType<CheckpointInteraction>();
        m_doorInteraction = FindObjectOfType<DoorInteraction>();
        m_itemInteraction = FindObjectOfType<ItemInteraction>();
        m_npcInteraction = FindObjectOfType<NPCInteraction>();
    }

    // private void Update()
    // {
    //     if (m_chestInteraction.m_canInteractChest)
    //     {
    //         m_renderInteractionUI.ActivateInteractionUI(true);
    //         m_renderInteractionUI.RenderInteractionChest();
    //     }
    //     else if (m_checkpointInteraction.m_canInteractCheckpoint)
    //     {
    //         m_renderInteractionUI.ActivateInteractionUI(true);
    //         m_renderInteractionUI.RenderInteractionCheckpoint();
    //     }
    //     else if (m_doorInteraction.m_canInteractDoor)
    //     {
    //         m_renderInteractionUI.ActivateInteractionUI(true);
    //         m_renderInteractionUI.RenderInteractionDoor();
    //     }
    //     else if (m_itemInteraction.m_canInteractItems)
    //     {
    //         m_renderInteractionUI.ActivateInteractionUI(true);
    //         m_renderInteractionUI.RenderInteractionItem();
    //     }
    //     else if (m_npcInteraction.m_canInteractNPC)
    //     {
    //         m_renderInteractionUI.ActivateInteractionUI(true);
    //         m_renderInteractionUI.RenderInteractionNPC();
    //     }
    //     else
    //     {
    //         m_renderInteractionUI.ActivateInteractionUI(false);
    //     }
    // }
}
