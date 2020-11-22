using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{

    private float m_interactionRadius;
    private LayerMask m_playerLayer;

    public virtual void Start()
    {
        m_playerLayer = LayerMask.GetMask("Player");
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
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_interactionRadius);
    }
}
