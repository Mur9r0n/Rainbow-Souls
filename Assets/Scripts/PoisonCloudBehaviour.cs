using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonCloudBehaviour : MonoBehaviour
{
    [SerializeField] private float m_Duration;
    private VisualEffect vfx = null;
    private bool m_isExpending = false;
    private void Awake()
    {
        vfx = GetComponentInChildren<VisualEffect>();
        m_isExpending = true;
        Invoke("DeactivateYourSelf", m_Duration * 0.5f);
    }

    private void Update()
    {
        if (m_isExpending)
        {
            transform.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
        }
    }

    void DeactivateYourSelf()
    {
        vfx.Stop();
        m_isExpending = false;
        Invoke("DestroyYourSelf", m_Duration * 0.5f);
    }

    void DestroyYourSelf()
    {
        Destroy(this.gameObject);
    }
}
