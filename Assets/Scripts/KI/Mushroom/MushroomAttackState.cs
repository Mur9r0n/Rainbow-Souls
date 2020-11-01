using UnityEngine;

public class MushroomAttackState : ABaseState
{
    private Animator m_anim = null;
    private int m_attack = Animator.StringToHash("Melee Attack");
    private Vector3 m_transformPosition;
    private Vector3 m_playerTransformPosition;

    private float m_cloudDelay = 10f;
    private float m_attackDelay = 4f;

    private float m_cloudTimer = 0;
    private float m_attackTimer = 0;

    public override bool Enter()
    {
        m_anim = m_mushroomController.GetComponent<Animator>();
        Debug.Log("Hier wäre die Update! MushroomAttackState");
        return base.Enter();
    }

    public override void Update()
    {
        m_transformPosition = m_mushroomController.transform.position;
        m_playerTransformPosition = GameManager.Instance.PlayerTransform.position;

        if (m_cloudTimer > 0)
        {
            m_cloudTimer -= Time.deltaTime;
        }

        if (m_attackTimer > 0)
        {
            m_attackTimer -= Time.deltaTime;
        }

        m_mushroomController.transform.LookAt(m_playerTransformPosition);

        if (Vector3.Distance(m_playerTransformPosition, m_transformPosition) <= m_mushroomController.m_PoisonDistance &&
            Vector3.Distance(m_playerTransformPosition, m_transformPosition) > m_mushroomController.m_AttackDistance)
        {
            m_mushroomController.m_Agent.isStopped = false;
            m_mushroomController.m_Agent.SetDestination(m_playerTransformPosition + new Vector3(0, 1, 0));
            if (m_cloudTimer <= 0)
            {
                MonoBehaviour.Instantiate(m_mushroomController.m_PoisonCloudPrefab, m_transformPosition, Quaternion.identity);
                m_cloudTimer = m_cloudDelay;
            }
        }

        if (Vector3.Distance(m_playerTransformPosition, m_transformPosition) <= m_mushroomController.m_AttackDistance)
        {
            m_mushroomController.m_Agent.isStopped = true;

            if (m_attackTimer <= 0)
            {
                    m_anim.SetTrigger(m_attack);
                    m_attackTimer = m_attackDelay;
            }
        }
    }
}