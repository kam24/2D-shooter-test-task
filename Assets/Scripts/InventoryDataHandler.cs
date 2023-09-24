using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventoryDataHandler
{
    private string dataPath;

    public InventoryDataHandler()
    {
        dataPath = Application.dataPath + "/Saves/data.dat";
    }

    public void SaveData(IReadOnlyList<InventoryCell> data)
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (FileStream file = File.Create(dataPath))
        {
            bf.Serialize(file, data);
        }

        Debug.Log("Data saved at " + dataPath);
    }

    public List<InventoryCell> LoadData()
    {
        List<InventoryCell> data = null;

        if (File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream file = File.Open(dataPath, FileMode.Open))
            {
                data = (List<InventoryCell>)bf.Deserialize(file);
            }

            Debug.Log("Data loaded from " + dataPath);
        }

        return data;
    }
}

