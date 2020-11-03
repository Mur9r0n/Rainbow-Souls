using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Cape", menuName = "Item/Cape")]
public class Cape : Item
{
    [Header("Specific Item Informations")][SerializeField, Tooltip("Charisma")]
    private int m_amountOfNonPeasantry;
}
