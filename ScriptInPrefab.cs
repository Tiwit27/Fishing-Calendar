using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInPrefab : MonoBehaviour
{
    public int id;
    public ReadSaveInGame readSave;
    public void Click()
    {
        readSave.OpenSaveDayTab(id);
    }
    public void OpenFish()
    {
        readSave.OpenFish(id);
    }
}