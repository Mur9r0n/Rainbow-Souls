using System.IO.IsolatedStorage;
using UnityEngine;

public class MushroomResetState : ABaseState
{
    private Quaternion? temp = null;
    private float timer = 0;
    public override bool Enter()
    {
        m_mushroomController.m_Agent.isStopped = false;
        m_mushroomController.m_Agent.SetDestination(m_mushroomController.OriginalPosition);
        Debug.Log("Hier wäre die Update! MushroomResetState");
        return base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(m_mushroomController.transform.position, m_mushroomController.OriginalPosition) <= .2f)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                m_mushroomController.transform.rotation = m_mushroomController.OriginalRotation;
                IsFinished = true;
            }
            if (temp == null)
            {
                temp = m_mushroomController.transform.rotation;
            }
            m_mushroomController.m_Agent.enabled = false;
            // m_controller.transform.rotation = m_controller.OriginalRotation;
            m_mushroomController.transform.rotation = Quaternion.Lerp((Quaternion)temp,m_mushroomController.OriginalRotation,timer);
            m_mushroomController.m_Agent.enabled = true;
            // IsFinished = true;
        }
    }

    public override void Exit()
    {
        timer = 0;
        m_mushroomController.m_FOVAngle = m_mushroomController.OriginalFOVAngle;
        m_mushroomController.m_FOVDistance = m_mushroomController.OriginalFOVDistance;
        base.Exit();
    }
}
