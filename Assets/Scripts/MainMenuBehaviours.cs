using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviours : MonoBehaviour
{
    [SerializeField] private Button m_continueButton;
    [SerializeField] private Button m_newGameButton;
    [SerializeField] private Button m_loadGameButton;
    [SerializeField] private Button m_settingsButton;
    [SerializeField] private Button m_creditsButton;
    [SerializeField] private Button m_quitGameButton;

    [SerializeField] private GameObject m_QuitGamePanel;


    private void Start()
    {
        m_QuitGamePanel.SetActive(false);
    }

    public void ContinueGame()
    {
        Debug.Log("No LoadFile Found");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        Debug.Log("Load last Save Game!");
    }

    public void OpenSettings()
    {
        Debug.Log("Open Settings Panel!");
    }

    public void ShowCredits()
    {
        Debug.Log("Show Credits!");
    }

    public void QuitGame()
    {
        m_QuitGamePanel.SetActive(true);
    }

    public void QuitGameYes()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void QuitGameNo()
    {
        m_QuitGamePanel.SetActive(false);
    }
}
