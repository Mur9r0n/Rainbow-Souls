using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Helmet", menuName = "Item/Helmet")]
public class Helmet : Item
{
    [Header("Specific Item Informations")] [SerializeField, Tooltip("Armor Value")]
    private int m_armorValue;

    [SerializeField, Tooltip("Bleeding Resistance")]
    private int m_bleedingResistance;

    [SerializeField, Tooltip("Poison Resistance")]
    private int m_poisonResistance;
}
