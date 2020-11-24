using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTestScript : MonoBehaviour
{
    public int m_TargetSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GlobalGameData.Instance.SavePlayerDataGlobalFromGame(other.GetComponent<PlayerStats>());
            SceneManager.LoadScene(m_TargetSceneIndex);
        }
    }
}
