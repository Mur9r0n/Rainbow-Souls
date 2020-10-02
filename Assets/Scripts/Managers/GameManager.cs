using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerTransform { get; private set; }

    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
    #endregion

    private void Start()
    {
        PlayerTransform = FindObjectOfType<PlayerController>().transform;
    }
}
