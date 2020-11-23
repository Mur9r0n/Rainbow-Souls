using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RenderInteractionUI : MonoBehaviour
{
    public GameObject m_panelParent;
    public TextMeshProUGUI m_Text = null;

    private void Start()
    {
        ActivateInteractionUI(false);
    }

    public void RenderInteractionChest()
    {
        m_Text.SetText("You can interact with a chest");
    }
    
    public void RenderInteractionCheckpoint()
    {
        m_Text.SetText("You can interact with a checkpoint");
    }
    
    public void RenderInteractionDoor()
    {
        m_Text.SetText("You can interact with a Door");
    }
    
    public void RenderInteractionItem()
    {
        m_Text.SetText("You can interact with an Item");
    }    
    
    public void RenderInteractionNPC()
    {
        m_Text.SetText("You can interact with an NPC");
    }

    public void ActivateInteractionUI(bool _activated)
    {
        m_panelParent.SetActive(_activated);
    }
}
