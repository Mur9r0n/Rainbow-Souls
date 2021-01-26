using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMenuBehaviours : MonoBehaviour
{
    public void Rest()
    {
        //TODO Reset the world by respawning enemies and healing player etc
        Debug.Log("Rest means the world will be resetted.");
        Debug.Log("Does nothing atm, except close the menu.");
        Leave();
    }

    public void LevelUp()
    {
        UIManager.Instance.m_CheckPointMenuPanel.SetActive(false);
        UIManager.Instance.m_LevelingPanel.SetActive(true);
    }

    public void Leave()
    {
        UIManager.Instance.m_CheckPointMenuPanel.SetActive(false);
        FindObjectOfType<PlayerMovement>().m_cinemachineFreeLook.enabled = true;
        FindObjectOfType<PlayerMovement>().enabled = true;
        FindObjectOfType<PlayerCombat>().enabled = true;
        UIManager.Instance.m_HUDPanel.SetActive(true);
    }
}
