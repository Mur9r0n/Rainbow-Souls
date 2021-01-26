using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStats : MonoBehaviour
{
    public List<AEnemyController> p_Enemies = new List<AEnemyController>();
    public List<ChestInteraction> p_Chests = new List<ChestInteraction>();
    public List<CheckpointInteraction> p_CheckPoints = new List<CheckpointInteraction>();
    public List<DoorInteraction> p_Doors = new List<DoorInteraction>();
    public List<ItemInteraction> p_Items = new List<ItemInteraction>();
}
