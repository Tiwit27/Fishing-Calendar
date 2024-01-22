using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptInPrefab : MonoBehaviour
{
    public int id;
    public int savesId;
    public int monthId;
    public ReadSaveInGame readSave;
    public StatsBehaviour stats;
    public int month;
    public int year;
    float deltaTime;
    bool count = false;
    public void Click()
    {
        readSave.OpenSaveDayTab(id, this.gameObject);
    }
    public void StartHolding()
    {
        deltaTime = 0;
        count = true;
    }
    public void StopHolding()
    {
        count = false;
        if(deltaTime < 2)
        {
            Click();
        }
        deltaTime = 0;
    }
    public void OpenFish()
    {
        readSave.OpenFish(id);
    }
    private void Update()
    {
        if(count == true)
        {
            deltaTime += Time.deltaTime;
        }
        if(deltaTime > 2)
        {
            stats.OpenStats(id);
        }
    }
}