using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingScript : MonoBehaviour
{
    private PlayerCombat m_playerController;
    private Rigidbody m_rb = null;
    
    public float m_Force = 500f;
    [SerializeField] private float m_damage = 10f;
    [SerializeField] private float m_lifeDuration = 10f;
    private float m_angle = 1f;
    void Start()
    {
        m_playerController = FindObjectOfType<PlayerCombat>();
        m_rb = GetComponent<Rigidbody>();
        m_angle = Vector3.Distance(m_playerController.transform.position, transform.position) *0.4f;

        transform.LookAt(m_playerController.transform.position + new Vector3(0,m_angle,0));
        m_rb.AddForce(((m_playerController.transform.position + new Vector3(0,m_angle,0)) - transform.position).normalized * m_Force);
        
        Invoke("DestroyYourself", m_lifeDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_playerController.TakeDamage(m_damage);
            //TODO Object pooling
            Destroy(this.gameObject);
        }
    }

    void DestroyYourself()
    {
        gameObject.SetActive(false);
    }
}
