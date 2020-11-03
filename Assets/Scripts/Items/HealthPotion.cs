using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "Item/Health Potion")]
public class HealthPotion : Item
{
    [Header("Specific Item Informations")]

    [SerializeField, Tooltip("Heal Amount")]
    private int m_healAmount = 10;
}
