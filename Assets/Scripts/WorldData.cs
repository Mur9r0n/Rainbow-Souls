using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WorldData
{
    public List<int> m_FallenEnemies = new List<int>();
    public List<int> m_OpenedChests = new List<int>();
    public List<int> m_OpenedDoors = new List<int>();
    public List<int> m_LootedItems = new List<int>();
    public List<int> m_ActivatedCheckPoints = new List<int>();

    public WorldData()
    {
        if (GameManager.Instance != null)
        {
            m_FallenEnemies = GameManager.Instance.m_FallenEnemies;
            m_OpenedChests = GameManager.Instance.m_OpenedChests;
            m_OpenedDoors = GameManager.Instance.m_OpenedDoors;
            m_LootedItems = GameManager.Instance.m_LootedItems;
            m_ActivatedCheckPoints = GameManager.Instance.m_ActivatedCheckPoints;
        }
    }
}