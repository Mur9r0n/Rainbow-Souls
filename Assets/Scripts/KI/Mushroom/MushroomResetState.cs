﻿using System.IO.IsolatedStorage;
using UnityEngine;

public class MushroomResetState : ABaseState
{
    private Quaternion? temp = null;
    private float timer = 0;
    public override bool Enter()
    {
        m_controller.m_Agent.isStopped = false;
        m_controller.m_Agent.SetDestination(m_controller.OriginalPosition);
        // Debug.Log("Hier wäre die Update! MushroomResetState");
        return base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(m_controller.transform.position, m_controller.OriginalPosition) <= .2f)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                m_controller.transform.rotation = m_controller.OriginalRotation;
                IsFinished = true;
            }
            if (temp == null)
            {
                temp = m_controller.transform.rotation;
            }
            m_controller.m_Agent.enabled = false;
            m_controller.transform.rotation = Quaternion.Lerp((Quaternion)temp,m_controller.OriginalRotation,timer);
            m_controller.m_Agent.enabled = true;
        }
    }

    public override void Exit()
    {
        timer = 0;
        m_controller.m_FOVAngle = m_controller.OriginalFOVAngle;
        m_controller.m_FOVDistance = m_controller.OriginalFOVDistance;
        base.Exit();
    }
}
