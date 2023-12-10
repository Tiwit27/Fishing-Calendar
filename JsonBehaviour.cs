using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonBehaviour : MonoBehaviour
{
    [SerializeField] AddBehaviour addBehaviour;
    [SerializeField] DataBase DB;


    public void SaveData(DataBase newObject)
    {
        string json = JsonUtility.ToJson(newObject, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Save.json", json);
        
    }
    public void ReadData()
    {
        string path = Application.persistentDataPath + "/Save.json";
        if (System.IO.File.Exists(path))
        {
            string read = System.IO.File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(read, DB);
            foreach (var item in DB.saveData)
            {
                addBehaviour.AddOnAppOpen(item);
            }
        }
        else
        {
            PlayerPrefs.SetInt("bool", 0);
            PlayerPrefs.Save();
        }
    }
}
