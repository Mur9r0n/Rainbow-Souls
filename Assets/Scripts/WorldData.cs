using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WorldData
{
    public List<AEnemyController> m_Enemies = new List<AEnemyController>();

    public WorldData()
    {
        m_Enemies = GameManager.Instance.m_Enemies;
    }
}
