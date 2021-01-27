﻿using UnityEngine;
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

    
    public void SaveWorld(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/WorldData"+_saveSlot+".wrd";
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData();

        bf.Serialize(fs, data);
        fs.Close();
    }
    
    public WorldData LoadWorld(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/WorldData"+_saveSlot+".wrd";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            WorldData data = (WorldData)bf.Deserialize(fs);
            fs.Close();

            return data;
        }
        else
        {
            Debug.Log("Data not found :" + path);

            return null;
        }
    }
    
    public void SaveInventory(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/InventoryData"+_saveSlot+".inv";
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData();

        bf.Serialize(fs, data);
        fs.Close();
    }
    
    public InventoryData LoadInventory(int _saveSlot)
    {
        string path = Application.persistentDataPath + "/InventoryData"+_saveSlot+".inv";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            InventoryData data = (InventoryData)bf.Deserialize(fs);
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
    public bool CheckForSaveFile()
    {
        string playerDataPath = Application.persistentDataPath + "/PlayerData1.pyd";
        string worldDataPath = Application.persistentDataPath + "/WorldData1.wrd";
        string inventoryDataPath = Application.persistentDataPath + "/InventoryData1.inv";

        // System.DateTime recentSave = File.GetLastWriteTime(path);

        if (File.Exists(playerDataPath) && File.Exists(worldDataPath) && File.Exists(inventoryDataPath))
        {
            return true;
        }
        
        playerDataPath = Application.persistentDataPath + "/PlayerData2.pyd";
        worldDataPath = Application.persistentDataPath + "/WorldData2.wrd";
        inventoryDataPath = Application.persistentDataPath + "/InventoryData2.inv";

        if (File.Exists(playerDataPath) && File.Exists(worldDataPath) && File.Exists(inventoryDataPath))
        {
            return true;
        }
        
        playerDataPath = Application.persistentDataPath + "/PlayerData3.pyd";
        worldDataPath = Application.persistentDataPath + "/WorldData3.wrd";
        inventoryDataPath = Application.persistentDataPath + "/InventoryData3.inv";

        if (File.Exists(playerDataPath) && File.Exists(worldDataPath) && File.Exists(inventoryDataPath))
        {
            return true;
        }
        
        return false;
    }
}
