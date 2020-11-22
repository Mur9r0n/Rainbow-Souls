using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuBehaviours : MonoBehaviour
{
    [SerializeField] private Button m_continueButton;
    
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

    [Header("Audio Sliders")] 
    [SerializeField] private Slider m_masterVolumeSlider;
    [SerializeField] private Slider m_musicVolumeSlider;
    [SerializeField] private Slider m_effectsVolumeSlider;

    private bool m_saveFileExists = false;

    public void Awake()
    {
        m_saveFileExists = DataManager.Instance.CheckForSaveFile();
    }

    private void Start()
    {
        m_mainMenuPanel.SetActive(true);
        m_videoSettingsPanel.SetActive(false);
        m_audioSettingsPanel.SetActive(false);
        m_creditsPanel.SetActive(false);
        m_quitGamePanel.SetActive(false);
        m_continueButton.interactable = m_saveFileExists;
        
        if (m_saveFileExists)
        {
            EventSystem.current.firstSelectedGameObject = null;
            EventSystem.current.firstSelectedGameObject = m_continueButton.gameObject;
        }
        else
        {
            EventSystem.current.firstSelectedGameObject = null;
            EventSystem.current.firstSelectedGameObject = m_mainMenuPanelFirstSelectedButton;
        }
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
        if (m_creditsPanel.activeInHierarchy) {m_creditsPanel.SetActive(false);}

        m_mainMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_selectedButton);
    }

    public void ShowCredits()
    {
        if (!m_creditsPanel.activeInHierarchy)
        {
            m_selectedButton = EventSystem.current.currentSelectedGameObject;
            m_creditsPanel.SetActive(true);
            m_mainMenuPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_creditsPanelFirstSelectedButton);
        }
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

    public void IncreaseVolume(Slider _slider)
    {
        if (_slider.value != _slider.maxValue)
        {
            _slider.value += 1f;
        }
    }

    public void DecreaseVolume(Slider _slider)
    {
        if (_slider.value != _slider.minValue)
        {
            _slider.value -= 1f;
        }
    }
}
