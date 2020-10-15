using System.IO.IsolatedStorage;
using UnityEngine;

public class SheepResetState : ABaseState
{
    private Quaternion? temp = null;
    private float timer = 0;
    public override bool Enter()
    {
        m_sheepController.m_Agent.isStopped = false;
        m_sheepController.m_Agent.SetDestination(m_sheepController.OriginalPosition);
        Debug.Log("Hier wäre die Update! SheepResetState");
        return base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(m_sheepController.transform.position, m_sheepController.OriginalPosition) <= .2f)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                m_sheepController.transform.rotation = m_sheepController.OriginalRotation;
                IsFinished = true;
            }
            if (temp == null)
            {
                temp = m_sheepController.transform.rotation;
            }
            m_sheepController.m_Agent.enabled = false;
            // m_controller.transform.rotation = m_controller.OriginalRotation;
            m_sheepController.transform.rotation = Quaternion.Lerp((Quaternion)temp,m_sheepController.OriginalRotation,timer);
            m_sheepController.m_Agent.enabled = true;
            // IsFinished = true;
        }
    }

    public override void Exit()
    {
        m_sheepController.m_FOVAngle = m_sheepController.OriginalFOVAngle;
        m_sheepController.m_FOVDistance = m_sheepController.OriginalFOVDistance;
        base.Exit();
    }
}