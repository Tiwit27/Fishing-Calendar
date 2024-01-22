using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptInMonthPrefab : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public AddBehaviour addBehaviour;
    [SerializeField] public bool savesActive;
    [SerializeField] public int saveCount;
    [SerializeField] public int fishCount;
    public int month;
    public int year;
    public void OpenSaveList()
    {
        addBehaviour.OpenSaveMonth(id);
    }
    public void CountFishes()
    {
        if (fishCount == 0)
        {
            this.transform.Find("MonthObject").Find("FishCount").GetComponent<TMP_Text>().text = "Brak Ryb";
        }
        else if (fishCount == 1)
        {
            this.transform.Find("MonthObject").Find("FishCount").GetComponent<TMP_Text>().text = fishCount + " Ryba";
        }
        else if (fishCount > 1 && fishCount < 5)
        {
            this.transform.Find("MonthObject").Find("FishCount").GetComponent<TMP_Text>().text = fishCount + " Ryby";
        }
        else if (fishCount > 4 && fishCount % 10 != 2 && fishCount % 10 != 3 && fishCount % 10 != 4)
        {
            this.transform.Find("MonthObject").Find("FishCount").GetComponent<TMP_Text>().text = fishCount + " Ryb";
        }
        else if (fishCount > 4 && (fishCount % 10 == 2 || fishCount % 10 == 3 || fishCount % 10 == 4))
        {
            this.transform.Find("MonthObject").Find("FishCount").GetComponent<TMP_Text>().text = fishCount + " Ryby";
        }
    }
    public void CountSaves()
    {
        if(saveCount == 1)
        {
            this.transform.Find("MonthObject").Find("SaveCount").GetComponent<TMP_Text>().text = saveCount + " wyjazd";
        }
        else if(saveCount > 1 && saveCount < 5)
        {
            this.transform.Find("MonthObject").Find("SaveCount").GetComponent<TMP_Text>().text = saveCount + " wyjazdy";
        }
        else if(saveCount > 4 && saveCount % 10 != 2 && saveCount % 10 != 3 && saveCount % 10 != 4)
        {
            this.transform.Find("MonthObject").Find("SaveCount").GetComponent<TMP_Text>().text = saveCount + " wyjazdów";
        }
        else if(saveCount > 4 && (saveCount % 10 == 2 || saveCount % 10 == 3 || saveCount % 10 == 4))
        {
            this.transform.Find("MonthObject").Find("SaveCount").GetComponent<TMP_Text>().text = saveCount + " wyjazdy";
        }
    }
}
