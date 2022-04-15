using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Com.KevinNipper.Pathfinding;
public static class SaveSystem
{

    public static void SavePlayer(Player player)
    {


        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.dot";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavePlayerData data = new SavePlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }


    public static SavePlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerData.dot";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePlayerData data = formatter.Deserialize(stream) as SavePlayerData;
            stream.Close();

            return data;
        }
        else
        {
            SavePlayerData data = new SavePlayerData();
            return data;
        }
    }
}
