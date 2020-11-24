using System;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuBehaviours : MonoBehaviour
{
    [SerializeField] private Button m_continueButton;
    
    [Header("Menu Panels")]
    [SerializeField] private GameObject m_mainMenuPanel;
    [SerializeField] private GameObject m_loadGamePanel;
    [SerializeField] private GameObject m_videoSettingsPanel;
    [SerializeField] private GameObject m_audioSettingsPanel;
    [SerializeField] private GameObject m_creditsPanel;
    [SerializeField] private GameObject m_quitGamePanel;

    [Header("First Selected Button per Panel")]
    [SerializeField] private GameObject m_mainMenuPanelFirstSelectedButton;
    [SerializeField] private GameObject m_loadGamePanelFirstSelectedButton;
    [SerializeField] private GameObject m_videoSettingsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_audioSettingsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_creditsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_quitGamePanelFirstSelectedButton;
    [SerializeField] private GameObject m_selectedButton;

    [Header("Load File Boxes")] 
    [SerializeField] private Button m_buttonSlot1;
    [SerializeField] private Button m_buttonSlot2;
    [SerializeField] private Button m_buttonSlot3;

    [SerializeField] private TextMeshProUGUI m_textSlot1;
    [SerializeField] private TextMeshProUGUI m_textSlot2;
    [SerializeField] private TextMeshProUGUI m_textSlot3;
    
    private bool m_saveFileExists = false;

    private string m_pathSlot1;
    private string m_pathSlot2;
    private string m_pathSlot3;


    public void Awake()
    {
        m_pathSlot1 = Application.persistentDataPath + "/PlayerData1.pyd";
        m_pathSlot2 = Application.persistentDataPath + "/PlayerData2.pyd";
        m_pathSlot3 = Application.persistentDataPath + "/PlayerData3.pyd";
        
        m_saveFileExists = DataManager.Instance.CheckForSaveFile(m_pathSlot1,m_pathSlot2,m_pathSlot3);
    }

    private void Start()
    {
        m_mainMenuPanel.SetActive(true);
        m_loadGamePanel.SetActive(false);
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
        DateTime tempTime = DateTime.MinValue;
        string tempString = "";

        if (File.Exists(m_pathSlot1))
        {
            if (File.GetLastWriteTime(m_pathSlot1) >= tempTime)
            {
                tempTime = File.GetLastWriteTime(m_pathSlot1);
                tempString = m_pathSlot1;
            }
        }
        if (File.Exists(m_pathSlot2))
        {
            if (File.GetLastWriteTime(m_pathSlot2) >= tempTime)
            {
                tempTime = File.GetLastWriteTime(m_pathSlot2);
                tempString = m_pathSlot2;
            }
        }
        if (File.Exists(m_pathSlot3))
        {
            if (File.GetLastWriteTime(m_pathSlot3) >= tempTime)
            {
                tempTime = File.GetLastWriteTime(m_pathSlot3);
                tempString = m_pathSlot3;
            }
        }
        
        Debug.Log("Load " + tempString);
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        m_selectedButton = EventSystem.current.currentSelectedGameObject;
        m_loadGamePanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(m_loadGamePanelFirstSelectedButton);

        DateTime tempString;
        
        if (File.Exists(m_pathSlot1))
        {
            tempString = File.GetLastWriteTime(m_pathSlot1);
            m_textSlot1.text = tempString.ToString();
            m_buttonSlot1.interactable = true;

        }
        if (File.Exists(m_pathSlot2))
        {
            tempString = File.GetLastWriteTime(m_pathSlot2);
            m_textSlot2.text = tempString.ToString();
            m_buttonSlot2.interactable = true;
        }
        if (File.Exists(m_pathSlot3))
        {
            tempString = File.GetLastWriteTime(m_pathSlot3);
            m_textSlot3.text = tempString.ToString();            
            m_buttonSlot3.interactable = true;
        }
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
        if (m_loadGamePanel.activeInHierarchy) {m_loadGamePanel.SetActive(false);}
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

    //TODO Loading the current Slot
    public void LoadSaveGameSlot(int _saveSlot)
    {
        Debug.Log("Load SaveSlot "+ _saveSlot);
        SceneManager.LoadScene(1);
    }
}
