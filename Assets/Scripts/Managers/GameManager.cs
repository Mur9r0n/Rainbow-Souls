using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerTransform { get; private set; }
    public List<AEnemyController> m_Enemies = new List<AEnemyController>();
    
    public List<int> m_FallenEnemies = new List<int>();

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
        PlayerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
}
