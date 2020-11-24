using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public void SavePlayer(PlayerStats _playerStats, int _saveSlot)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData"+_saveSlot+".pyd";
        FileStream fs = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(_playerStats);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public PlayerData LoadPlayer(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/PlayerData"+_saveSlot+".pyd";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(fs);
            fs.Close();

            return data;
        }
        else
        {
            Debug.Log("Data not found :" + path);

            return null;
        }
    }

    //TODO Most Recent File to continue
    public bool CheckForSaveFile(string _pathSlot1, string _pathSlot2, string _pathSlot3)
    {
        string path = Application.persistentDataPath + "/PlayerData1.pyd";

        // System.DateTime recentSave = File.GetLastWriteTime(path);

        if (File.Exists(path))
        {
            return true;
        }
        
        path = Application.persistentDataPath + "/PlayerData2.pyd";

        if (File.Exists(path))
        {
            return true;
        }
        
        path = Application.persistentDataPath + "/PlayerData3.pyd";

        if (File.Exists(path))
        {
            return true;
        }
        

        return false;
    }
}
