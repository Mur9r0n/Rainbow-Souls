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
    private int m_ID;

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
        Consumables,   //ID Prefix 100
        Undefined,     //ID Prefix 200
        KeyItem,       //ID Prefix 300
        Weapon,        //ID Prefix 400
        Armor,         //ID Prefix 500
        Helmet,        //ID Prefix 600
        Cape,          //ID Prefix 700
    }

    public virtual void Use()
    {
        Debug.Log("Used" + Name);
    }

    public string Name
    {
        get => m_name;
    }

    public int ID
    {
        get => m_ID;
    }

    public string Description
    {
        get => m_description;
    }

    public int PigmentValue
    {
        get => m_pigmentValue;
    }

    public Sprite Icon
    {
        get => m_icon;
    }

    public ItemType Type
    {
        get => m_ItemType;
    }
}