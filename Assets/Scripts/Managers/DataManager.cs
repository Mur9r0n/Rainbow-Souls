﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

    public void SavePlayer(PlayerStats _playerStats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.pyd";
        FileStream fs = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(_playerStats);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/PlayerData.pyd";

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
}
