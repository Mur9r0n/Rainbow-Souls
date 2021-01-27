using System;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class MainMenuBehaviours : MonoBehaviour
{
    [SerializeField] private Button m_continueButton;
    [SerializeField] private Button m_loadGameBackButton;

    [Header("Menu Panels")] [SerializeField]
    private GameObject m_mainMenuPanel;

    [SerializeField] private GameObject m_loadGamePanel;
    [SerializeField] private GameObject m_videoSettingsPanel;
    [SerializeField] private GameObject m_audioSettingsPanel;
    [SerializeField] private GameObject m_creditsPanel;
    [SerializeField] private GameObject m_quitGamePanel;

    [Header("First Selected Button per Panel")] [SerializeField]
    private GameObject m_mainMenuPanelFirstSelectedButton;

    [SerializeField] private GameObject m_loadGamePanelFirstSelectedButton;
    [SerializeField] private GameObject m_videoSettingsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_audioSettingsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_creditsPanelFirstSelectedButton;
    [SerializeField] private GameObject m_quitGamePanelFirstSelectedButton;
    [SerializeField] private GameObject m_selectedButton;

    [Header("Load File Boxes")] [SerializeField]
    private Button m_buttonSlot1;

    [SerializeField] private Button m_buttonSlot2;
    [SerializeField] private Button m_buttonSlot3;

    [SerializeField] private TextMeshProUGUI m_textSlot1;
    [SerializeField] private TextMeshProUGUI m_textSlot2;
    [SerializeField] private TextMeshProUGUI m_textSlot3;

    [SerializeField] private Button m_deleteButtonSlot1;
    [SerializeField] private Button m_deleteButtonSlot2;
    [SerializeField] private Button m_deleteButtonSlot3;

    private bool m_saveFileExists = false;

    private string m_playerPathSlot1;
    private string m_playerPathSlot2;
    private string m_playerPathSlot3;
    
    private string m_worldPathSlot1;
    private string m_worldPathSlot2;
    private string m_worldPathSlot3;

    public void Awake()
    {
        m_playerPathSlot1 = Application.persistentDataPath + "/PlayerData1.pyd";
        m_playerPathSlot2 = Application.persistentDataPath + "/PlayerData2.pyd";
        m_playerPathSlot3 = Application.persistentDataPath + "/PlayerData3.pyd";
        m_worldPathSlot1 = Application.persistentDataPath + "/WorldData1.wrd";
        m_worldPathSlot2 = Application.persistentDataPath + "/WorldData2.wrd";
        m_worldPathSlot3 = Application.persistentDataPath + "/WorldData3.wrd";

        CheckIfSaveFileExists();
    }

    private void Start()
    {
        m_mainMenuPanel.SetActive(true);
        m_loadGamePanel.SetActive(false);
        m_videoSettingsPanel.SetActive(false);
        m_audioSettingsPanel.SetActive(false);
        m_creditsPanel.SetActive(false);
        m_quitGamePanel.SetActive(false);

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
        string playerDataPath = "";
        int mostRecentSlot = 1;

        if (File.Exists(m_playerPathSlot1))
        {
            if (File.GetLastWriteTime(m_playerPathSlot1) >= tempTime)
            {
                tempTime = File.GetLastWriteTime(m_playerPathSlot1);
                playerDataPath = m_playerPathSlot1 + " " + m_worldPathSlot1;
                mostRecentSlot = 1;
            }
        }

        if (File.Exists(m_playerPathSlot2))
        {
            if (File.GetLastWriteTime(m_playerPathSlot2) >= tempTime)
            {
                tempTime = File.GetLastWriteTime(m_playerPathSlot2);
                playerDataPath = m_playerPathSlot2 + " " + m_worldPathSlot2;
                mostRecentSlot = 2;
            }
        }

        if (File.Exists(m_playerPathSlot3))
        {
            if (File.GetLastWriteTime(m_playerPathSlot3) >= tempTime)
            {
                tempTime = File.GetLastWriteTime(m_playerPathSlot3);
                playerDataPath = m_playerPathSlot3 + " " + m_worldPathSlot3;
                mostRecentSlot = 3;
            }
        }

        Debug.Log("Load " + playerDataPath);
        GlobalGameData.Instance.SavePlayerDataGlobalFromFile(mostRecentSlot);
        GlobalGameData.Instance.SaveWorldDataGlobalFromFile(mostRecentSlot);
        
        //TODO load last used Scene
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    //TODO First selected
    public void LoadGame()
    {
        m_selectedButton = EventSystem.current.currentSelectedGameObject;
        m_loadGamePanel.SetActive(true);
        m_mainMenuPanel.SetActive(false);

        DateTime tempString;

        if (m_saveFileExists)
        {
            if (File.Exists(m_playerPathSlot1))
            {
                tempString = File.GetLastWriteTime(m_playerPathSlot1);
                m_textSlot1.text = tempString.ToString();
                m_buttonSlot1.interactable = true;
                m_deleteButtonSlot1.interactable = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(m_buttonSlot1.gameObject);
            }
            else
            {
                m_textSlot1.text = "No Save Game";
                m_buttonSlot1.interactable = false;
                m_deleteButtonSlot1.interactable = false;
            }

            if (File.Exists(m_playerPathSlot2))
            {
                tempString = File.GetLastWriteTime(m_playerPathSlot2);
                m_textSlot2.text = tempString.ToString();
                m_buttonSlot2.interactable = true;
                m_deleteButtonSlot2.interactable = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(m_buttonSlot2.gameObject);
            }
            else
            {
                m_textSlot2.text = "No Save Game";
                m_buttonSlot2.interactable = false;
                m_deleteButtonSlot2.interactable = false;
            }

            if (File.Exists(m_playerPathSlot3))
            {
                tempString = File.GetLastWriteTime(m_playerPathSlot3);
                m_textSlot3.text = tempString.ToString();
                m_buttonSlot3.interactable = true;
                m_deleteButtonSlot3.interactable = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(m_buttonSlot3.gameObject);
            }
            else
            {
                m_textSlot3.text = "No Save Game";
                m_buttonSlot3.interactable = false;
                m_deleteButtonSlot3.interactable = false;
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(m_loadGameBackButton.gameObject);
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
        CheckIfSaveFileExists();
        if (m_loadGamePanel.activeInHierarchy)
        {
            m_loadGamePanel.SetActive(false);
        }

        if (m_audioSettingsPanel.activeInHierarchy)
        {
            m_audioSettingsPanel.SetActive(false);
        }

        if (m_videoSettingsPanel.activeInHierarchy)
        {
            m_videoSettingsPanel.SetActive(false);
        }

        if (m_creditsPanel.activeInHierarchy)
        {
            m_creditsPanel.SetActive(false);
        }

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

    public void LoadSaveGameSlot(int _saveSlot)
    {
        GlobalGameData.Instance.SavePlayerDataGlobalFromFile(_saveSlot);
        GlobalGameData.Instance.SaveWorldDataGlobalFromFile(_saveSlot);
        Debug.Log("Load SaveSlot " + _saveSlot);
        SceneManager.LoadScene(GlobalGameData.Instance.m_PlayerData.m_SceneIndex);
    }

    public void DeleteSaveGameSlot(int _saveSlot)
    {
        string playerPath = Application.persistentDataPath + "/PlayerData" + _saveSlot + ".pyd";
        string worldPath = Application.persistentDataPath + "/WorldData" + _saveSlot + ".wrd";

        if (File.Exists(playerPath) && File.Exists(worldPath))
        {
            File.Delete(playerPath);
            File.Delete(worldPath);
            LoadGame();
        }
    }

    public void CheckIfSaveFileExists()
    {
        m_saveFileExists = DataManager.Instance.CheckForSaveFile();

        m_continueButton.interactable = m_saveFileExists;
    }
}