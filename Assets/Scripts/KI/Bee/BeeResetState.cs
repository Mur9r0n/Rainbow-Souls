using System.IO.IsolatedStorage;
using UnityEngine;

public class BeeResetState : ABaseState
{
    private Quaternion? temp = null;
    private float timer = 0;
    public override bool Enter()
    {
        m_beeController.m_Agent.isStopped = false;
        m_beeController.m_Agent.SetDestination(m_beeController.OriginalPosition);
        Debug.Log("Hier wäre die Update! SheepResetState");
        return base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(m_beeController.transform.position, m_beeController.OriginalPosition) <= .2f)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                m_beeController.transform.rotation = m_beeController.OriginalRotation;
                IsFinished = true;
            }
            if (temp == null)
            {
                temp = m_beeController.transform.rotation;
            }
            m_beeController.m_Agent.enabled = false;
            // m_controller.transform.rotation = m_controller.OriginalRotation;
            m_beeController.transform.rotation = Quaternion.Lerp((Quaternion)temp,m_beeController.OriginalRotation,timer);
            m_beeController.m_Agent.enabled = true;
            // IsFinished = true;
        }
    }

    public override void Exit()
    {
        m_beeController.m_FOVAngle = m_beeController.OriginalFOVAngle;
        m_beeController.m_FOVDistance = m_beeController.OriginalFOVDistance;
        base.Exit();
    }
}
