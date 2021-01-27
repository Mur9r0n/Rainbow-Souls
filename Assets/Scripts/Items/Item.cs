using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public abstract class Item : ScriptableObject
{
    [Header("Item Informations")] 
    
    [SerializeField, Tooltip("Name")]
    private string m_name;

    [SerializeField, Tooltip("Identification")]
    public int m_ID;

    [SerializeField, Tooltip("Description")] [TextArea(5, 15)]
    private string m_description;

    [SerializeField, Tooltip("Pigment Value")]
    private int m_pigmentValue;

    [SerializeField, Tooltip("Icon")] 
    private Sprite m_icon;
    
    [SerializeField, Tooltip("Type")] 
    private ItemType m_ItemType;


    public enum ItemType
    {
        Weapon,        //ID Prefix 10XXX
        Helmet,        //ID Prefix 20XXX
        Armor,         //ID Prefix 30XXX
        Cape,          //ID Prefix 40XXX
        Consumables,   //ID Prefix 50XXX
        Undefined,     //ID Prefix 70XXX
        KeyItem,       //ID Prefix 90XXX
        Empty          //ID 1
    }

    public virtual void Use()
    {
        Debug.Log("Used " + GetName());
    }

    public string GetName()
    {
        return m_name;
    }

    public int GetID()
    {
        return m_ID;
    }

    public string GetDescription()
    {
        return m_description;
    }

    public int GetPigmentValue()
    {
        return m_pigmentValue;
    }

    public Sprite GetIcon()
    {
        return m_icon;
    }

    public ItemType GetType()
    {
        return m_ItemType;
    }
}