using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WorldData
{
    public List<AEnemyController> m_Enemies = new List<AEnemyController>();
    public List<ChestInteraction> m_Chests = new List<ChestInteraction>();
    public List<CheckpointInteraction> m_CheckPoints = new List<CheckpointInteraction>();
    public List<DoorInteraction> m_Doors = new List<DoorInteraction>();
    public List<ItemInteraction> m_Items = new List<ItemInteraction>();

    public WorldData()
    {
        m_Enemies = GameManager.Instance.m_Enemies;
        m_Chests = GameManager.Instance.m_Chests;
        m_CheckPoints = GameManager.Instance.m_CheckPoints;
        m_Doors = GameManager.Instance.m_Doors;
        m_Items = GameManager.Instance.m_Items;
    }
}
