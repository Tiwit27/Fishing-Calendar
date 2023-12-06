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
        //sprawdziæ na telefonie
        string read = System.IO.File.ReadAllText(Application.persistentDataPath + "/Save.json");
        JsonUtility.FromJsonOverwrite(read, DB);
    }
}
