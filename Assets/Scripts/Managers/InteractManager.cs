using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager Instance;
    
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
    
    public List<AInteractables> m_interactables = new List<AInteractables>();

    public AInteractables LookForClosestInteraction()
    {
        AInteractables tempClosestAInteractable = null;
        
        foreach (AInteractables interactable in m_interactables)
        {

            if (tempClosestAInteractable != null)
            {
                if (Vector3.Distance(interactable.gameObject.transform.position, GameManager.Instance.PlayerTransform.position)
                    <= Vector3.Distance(tempClosestAInteractable.transform.position, GameManager.Instance.PlayerTransform.position))
                {
                    tempClosestAInteractable = interactable;
                }
            }
            if (tempClosestAInteractable == null)
            {
                tempClosestAInteractable = interactable;
            }
        }

        if (tempClosestAInteractable != null)
        {
            return tempClosestAInteractable;
        }
        
        Debug.Log("NULL");
        return null;
    }

    public void UpdateItems()
    {

    }
}
