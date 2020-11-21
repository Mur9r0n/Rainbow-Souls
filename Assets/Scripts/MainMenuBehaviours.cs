using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuBehaviours : MonoBehaviour
{
    [SerializeField] private GameObject m_mainMenuPanel;
    [SerializeField] private GameObject m_videoSettingsPanel;
    [SerializeField] private GameObject m_audioSettingsPanel;
    [SerializeField] private GameObject m_creditsPanel;
    [SerializeField] private GameObject m_quitGamePanel;

    [SerializeField] private GameObject m_mainMenuPanelFirstSelectedButton;
    [SerializeField] private GameObject m_videoSettingsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_audioSettingsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_creditsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_quitGamePanelFirstSelectedButton;
    [SerializeField] private GameObject m_selectedButton;


    private void Start()
    {
        m_mainMenuPanel.SetActive(true);
        m_videoSettingsPanel.SetActive(false);
        m_audioSettingsPanel.SetActive(false);
        m_creditsPanel.SetActive(false);
        m_quitGamePanel.SetActive(false);
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.firstSelectedGameObject = m_mainMenuPanelFirstSelectedButton;

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
        m_selectedButton = EventSystem.current.currentSelectedGameObject;
        m_videoSettingsPanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_videoSettingsPanelFirstSelectedButton);
    }

    public void VideoSettings()
    {
        if (!m_videoSettingsPanel.activeInHierarchy)
        {
            m_audioSettingsPanel.SetActive(false);
            m_videoSettingsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_videoSettingsPanelFirstSelectedButton);
        }
    }

    public void AudioSettings()
    {
        if (!m_audioSettingsPanel.activeInHierarchy)
        {
            m_videoSettingsPanel.SetActive(false);
            m_audioSettingsPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_audioSettingsPanelFirstSelectedButton);
        }
    }

    public void GoBack()
    {
        if (m_audioSettingsPanel.activeInHierarchy) {m_audioSettingsPanel.SetActive(false);}
        if (m_videoSettingsPanel.activeInHierarchy) {m_videoSettingsPanel.SetActive(false);}

        m_mainMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_selectedButton);
    }

    public void ShowCredits()
    {
        Debug.Log("Show Credits!");
    }

    public void QuitGame()
    {
        m_selectedButton = EventSystem.current.currentSelectedGameObject;
        m_quitGamePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(m_quitGamePanelFirstSelectedButton);
    }

    public void QuitGameYes()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void QuitGameNo()
    {
        m_quitGamePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(m_selectedButton);
    }
}
