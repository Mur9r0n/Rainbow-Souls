using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public List<InteractableObjects> interactables = new List<InteractableObjects>();

    public void AddToList(InteractableObjects _object)
    {
        interactables.Add(_object);
    }

    public void RemoveFromList(InteractableObjects _object)
    {
        interactables.Remove(_object);
    }
    
}
