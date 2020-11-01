using UnityEngine;

public class BeeAttackState : ABaseState
{
    private Vector3 m_playerTransformPosition;

    private float m_shotDelay = 4f;
    private float m_shotTimer = 0f;
    
    public override bool Enter()
    {
        m_beeController.m_Agent.isStopped = true;
        // Debug.Log("Hier wäre die Update! BeeAttackState");
        return base.Enter();
    }
    
    public override void Update()
    {
        m_playerTransformPosition = GameManager.Instance.PlayerTransform.position;

        if (m_shotTimer > 0)
        {
            m_shotTimer -= Time.deltaTime;
        }
        
        m_beeController.transform.LookAt(GameManager.Instance.PlayerTransform.position);

        if (m_shotTimer <= 0)
        {
            m_shotTimer = m_shotDelay;
            MonoBehaviour.Instantiate(m_beeController.m_StingPrefab, m_beeController.m_StingTransform.position,Quaternion.identity);
        }
    }
}
