using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Sword", menuName = "Item/Sword")]
public class Sword : Item
{
    [Header("Specific Item Informations")]
    
    [SerializeField, Tooltip("Damage Amount")]
    private int m_damage;
}
