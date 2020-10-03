using UnityEngine;

public class SheepSearchState : ABaseState
{
    public float m_Timer;
    public bool m_Playerfound;
    
    public override bool Enter()
    {
        m_Timer = 10;
        m_controller.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
        Debug.Log("Hier wäre die Update! SheepSearchState");
        return base.Enter();
    }
    
    public override void Update()
    {
        m_Timer -= Time.deltaTime;
        Debug.Log("Searching...");

        if (m_Timer >= 0f)
        {
            SearchForPlayer();
        }
        else
        {
            m_Playerfound = false;
        }
        
    }

    public override void Exit()
    {
        m_Playerfound = false;
        base.Exit();
    }

    public void SearchForPlayer()
    {
        if (m_controller.PlayerInFOV())
            m_Playerfound = true;
    }
}
