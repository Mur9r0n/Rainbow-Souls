using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : AInteractables
{
    [SerializeField] private GameObject m_lootprefab;
    public bool m_isDissolving = false;
    public Material m_material;

    public override void Start()
    {
        base.Start();
        m_material = GetComponent<Renderer>().material;
        m_interactableType = InteractableType.Chest;
    }

    public override void Update()
    {
        base.Update();
        
        if (m_isDissolving)
        {
            float temp = m_material.GetFloat("Vector1_8DEAC01A");
            if (temp <= 1)
            {
                m_material.SetFloat("Vector1_8DEAC01A", temp += Time.deltaTime);
            }
            else
            {
                m_interactmanager.m_interactables.Remove(this);
                Destroy(gameObject);
            }
        }
    }

    public override void Interact()
    {
        Debug.Log("Interact with " + gameObject.name);

        Instantiate(m_lootprefab, transform.position, Quaternion.identity);
        m_isDissolving = true;
    }
}
