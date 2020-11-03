using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Key Item", menuName = "Item/Key Item")]
public class KeyItem : Item
{
    [Header("Specific Item Informations")]
    [SerializeField, Tooltip("Usage")]
    private GameObject m_unlock;
}
