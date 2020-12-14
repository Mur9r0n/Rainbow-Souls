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
    }

    public override void Update()
    {
        if (CheckForInteraction(gameObject.transform.position, 3.0f))
        {
            m_IsInteractable = true;
            if (!m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Add(this);
                UIManager.Instance.ShowInteractionTooltip("Open "+ gameObject.name);
            }
        }
        else
        {
            m_IsInteractable = false;
            if (m_interactmanager.m_interactables.Contains(this))
            {
                m_interactmanager.m_interactables.Remove(this);
                UIManager.Instance.HideInteractionTooltip();
            }
        }

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
