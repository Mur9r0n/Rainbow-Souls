using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int m_CurrentHealth;
    public int m_MaxHealth;
    public float[] m_Position;

    public PlayerData(PlayerController _controller)
    {
        m_CurrentHealth = _controller.m_CurrentHealth;
        m_MaxHealth = _controller.m_MaxHealth;

        m_Position = new float[3];
        m_Position[0] = _controller.transform.position.x;
        m_Position[1] = _controller.transform.position.y;
        m_Position[2] = _controller.transform.position.z;
    }
}
