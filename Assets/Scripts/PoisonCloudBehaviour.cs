using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonCloudBehaviour : MonoBehaviour
{
    [SerializeField] private float m_duration;
    private VisualEffect m_vfx = null;
    private bool m_expending = false;
    private void Awake()
    {
        m_vfx = GetComponentInChildren<VisualEffect>();
        m_expending = true;
        Invoke("DeactivateYourSelf", m_duration * 0.5f);
    }

    private void Update()
    {
        if (m_expending)
        {
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
        }
    }

    void DeactivateYourSelf()
    {
        m_vfx.Stop();
        m_expending = false;
        Invoke("DestroyYourSelf", m_duration * 0.5f);
    }

    void DestroyYourSelf()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Raucherhusten!");
            other.gameObject.SetActive(false);
        }
    }
}
