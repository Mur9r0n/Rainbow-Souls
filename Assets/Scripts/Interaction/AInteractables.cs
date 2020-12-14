using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class AInteractables : MonoBehaviour
{
    private LayerMask m_playerLayer;
    private float m_interactionRadius;
    
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
        
    }

    protected bool CheckForInteraction(Vector3 _position, float _interactionRadius)
    {
        m_interactionRadius = _interactionRadius;
        
        Collider[] hitColliders = Physics.OverlapSphere(_position, _interactionRadius, m_playerLayer);
        
        foreach (var Collider in hitColliders)
        {
            if (Collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void Interact()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_interactionRadius);
    }
}
