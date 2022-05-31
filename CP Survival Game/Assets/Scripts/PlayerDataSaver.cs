using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class PlayerDataSaver 
{
    public static void SaveGame(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Survive.save";
        FileStream stream = new FileStream(Path, FileMode.Create);
        PlayerData data = new PlayerData(inventory);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData LoadGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Survive.save";
        FileStream stream = new FileStream(Path, FileMode.Open);
        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();
        return data;
    }
}