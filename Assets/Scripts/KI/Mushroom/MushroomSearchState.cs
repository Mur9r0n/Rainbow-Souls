using UnityEngine;

public class MushroomSearchState : ABaseState
{
    public float m_Timer;
    public bool m_Playerfound;
    
    public override bool Enter()
    {
        m_Timer = m_mushroomController.m_ResetDelay;
        m_mushroomController.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
        // Debug.Log("Hier wäre die Update! MushroomSearchState");
        return base.Enter();
    }
    
    public override void Update()
    {
        m_Timer -= Time.deltaTime;
        // Debug.Log("Searching...");

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
        if (m_mushroomController.PlayerInFOV())
            m_Playerfound = true;
    }
}
