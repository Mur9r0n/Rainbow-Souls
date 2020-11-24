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
    
    public List<Interactables> m_interactables = new List<Interactables>();

    public Interactables LookForClosestInteraction()
    {
        Interactables tempClosestInteractable = null;
        
        foreach (Interactables interactable in m_interactables)
        {

            if (tempClosestInteractable != null)
            {
                if (Vector3.Distance(interactable.gameObject.transform.position, GameManager.Instance.PlayerTransform.position)
                    <= Vector3.Distance(tempClosestInteractable.transform.position, GameManager.Instance.PlayerTransform.position))
                {
                    tempClosestInteractable = interactable;
                }
            }
            if (tempClosestInteractable == null)
            {
                tempClosestInteractable = interactable;
            }
        }

        if (tempClosestInteractable != null)
        {
            Debug.Log("TEMP");

            return tempClosestInteractable;
        }
        
        Debug.Log("NULL");
        return null;
    }

    public void UpdateItems()
    {

    }
}
