using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WorldData
{
    public List<AEnemyController> p_Enemies = new List<AEnemyController>();
    public List<ChestInteraction> p_Chests = new List<ChestInteraction>();
    public List<CheckpointInteraction> p_CheckPoints = new List<CheckpointInteraction>();
    public List<DoorInteraction> p_Doors = new List<DoorInteraction>();
    public List<ItemInteraction> p_Items = new List<ItemInteraction>();

    public WorldData(WorldStats _worldStats)
    {
        p_Enemies = _worldStats.p_Enemies;
        p_Chests = _worldStats.p_Chests;
        p_CheckPoints = _worldStats.p_CheckPoints;
        p_Doors = _worldStats.p_Doors;
        p_Items = _worldStats.p_Items;
    }
}
