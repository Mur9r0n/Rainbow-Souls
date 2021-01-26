using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AInteractables : MonoBehaviour
{
    private LayerMask m_playerLayer;
    private float m_interactionRadius;
    protected InteractableType m_interactableType;
    
    public InteractManager m_interactmanager;
    public int ID = 10000000;
    public bool m_IsInteractable = false;

    public virtual void Start()
    {
        m_playerLayer = LayerMask.GetMask("Player");
        m_interactmanager = InteractManager.Instance;
    }

    public virtual void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 2.0f))
        {
            m_IsInteractable = true;
            if (!m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Add(this);
                UIManager.Instance.ShowInteractionTooltip(GetInteractionText());
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

    protected bool CheckForInteraction(Vector3 _position, float _interactionRadius)
    {
        m_interactionRadius = _interactionRadius;
        
        Collider[] hitColliders = Physics.OverlapSphere(_position, _interactionRadius, m_playerLayer);

        if (hitColliders.Length >= 1)
            return true;
        
        // foreach (var Collider in hitColliders)
        // {
        //     if (Collider.gameObject.CompareTag("Player"))
        //     {
        //         return true;
        //     }
        // }

        return false;
    }

    public virtual void Interact()
    {
        
    }
    
    private string GetInteractionText()
    {
        string _interactionText = "";
    
        switch (m_interactableType)
        {
            case InteractableType.Item:
            {
                _interactionText = $"Pick up {gameObject.name}";
                break;
            }
            case InteractableType.Chest:
            case InteractableType.Door:
            {
                _interactionText = $"Open {gameObject.name}";
                break;
            }
            case InteractableType.NPC:
            {
                _interactionText = $"Talk to {gameObject.name}";
                break;
            }
            case InteractableType.Checkpoint:
            {
                _interactionText = $"Interact with {gameObject.name}";
                break;
            }
        }
        return _interactionText;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_interactionRadius);
    }
}


public enum InteractableType
{
    Item,
    Chest,
    Door,
    NPC,
    Checkpoint
}
