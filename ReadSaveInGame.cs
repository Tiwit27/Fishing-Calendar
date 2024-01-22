using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadSaveInGame : MonoBehaviour
{
    [SerializeField] GameObject readSaveDay;
    [SerializeField] GameObject readSaveFishesList;
    [SerializeField] GameObject readSaveFish;
    [SerializeField] ActualDayEditor actualDayEditor;
    [SerializeField] AddBehaviour AddBehaviour;
    [SerializeField] public Info info;
    [SerializeField] FishInfo fishInfo;
    [SerializeField] DataBase DB;
    [SerializeField] GameObject prefab;
    [SerializeField] public GameObject parent;
    [SerializeField] ScrollBehaviour ScrollBehaviour;
    [SerializeField] JsonBehaviour Json;
    public int actualEdit;
    public int actualEditFish;
    public GameObject actualOpen;
    public GameObject plus;
    [SerializeField] GameObject delete;
    [SerializeField] GameObject deleteFish;
    [SerializeField] GameObject trashFish;
    [SerializeField] GameObject editFish;
    [SerializeField] StatsBehaviour stats;
    [SerializeField] EditBehaviour editBehaviour;
    [SerializeField] GameObject deleteWeather;

    private void Start()
    {
        deleteWeather.SetActive(false);
        deleteFish.SetActive(false);
        readSaveFish.SetActive(false);
        readSaveFishesList.SetActive(false);
        readSaveDay.SetActive(false);
        delete.SetActive(false);
    }
    public void OpenSaveDaysList()
    {
        actualDayEditor.plus.SetActive(false);
        actualDayEditor.backArrow.SetActive(true);
        actualDayEditor.editDay.SetActive(false);
        AddBehaviour.appBehaviour.addNew.SetActive(false);
        actualDayEditor.trash.SetActive(false);
        ScrollBehaviour.SetStartPosition();
    }
    public void BackFromSaveDaysList()
    {
        actualDayEditor.editDay.SetActive(true);
        actualDayEditor.plus.SetActive(true);
        actualDayEditor.backArrow.SetActive(false);
        actualDayEditor.trash.SetActive(true);
        ScrollBehaviour.SetStartPosition();
    }
    public void OpenSaveDayTab(int id, GameObject gameObject)
    {
        ScrollBehaviour.SetStartPosition();
        actualEdit = id;
        actualOpen = gameObject;
        readSaveDay.SetActive(true);
        info.date.text = "Data: " + DB.saveData[id].day.ToString().PadLeft(2, '0') + "." + DB.saveData[id].month.ToString().PadLeft(2, '0') + "." + DB.saveData[id].year.ToString().PadLeft(2, '0');
        info.lake.text = "£owisko: " + DB.saveData[id].lake;
        info.place.text = "Miejsce: " + DB.saveData[id].place;
        info.lastRain.text = "Ostatni Opad: " + DB.saveData[id].lastRainDay.ToString().PadLeft(2, '0') + "." + DB.saveData[id].lastRainMonth.ToString().PadLeft(2, '0');
        info.startTime.text = "Godzina startowa: " + DB.saveData[id].startHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[id].startMinute.ToString().PadLeft(2, '0');
        info.endTime.text = "Godzina koñcowa: " + DB.saveData[id].endHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[id].endMinute.ToString().PadLeft(2, '0');
        info.timeValue.maxValue = DB.saveData[id].editableData.Count - 1;
        info.weatherTimeValue.text = DB.saveData[id].editableData[0].editTime.Hours.ToString().PadLeft(2, '0') + ":" + DB.saveData[actualEdit].editableData[0].editTime.Minutes.ToString().PadLeft(2, '0');
        info.temperature.text = "Temperatura: " + DB.saveData[id].editableData[0].temperature + "°C";
        info.windValue.text = "Si³a Wiatru: " + DB.saveData[id].editableData[0].windValue + " km/h";
        switch (DB.saveData[id].editableData[0].windDirection)
        {
            case NewDay.WindDirection.N:
                info.windDirection.text = "Kierunek Wiatru: Pó³nocny";
                break;
            case NewDay.WindDirection.E:
                info.windDirection.text = "Kierunek Wiatru: Wschodni";
                break;
            case NewDay.WindDirection.S:
                info.windDirection.text = "Kierunek Wiatru: Po³udniowy";
                break;
            case NewDay.WindDirection.W:
                info.windDirection.text = "Kierunek Wiatru: Zachodni";
                break;
            case NewDay.WindDirection.NE:
                info.windDirection.text = "Kierunek Wiatru: Pó³nocno - Wschodni";
                break;
            case NewDay.WindDirection.SE:
                info.windDirection.text = "Kierunek Wiatru: Po³udniowo - Wschodni";
                break;
            case NewDay.WindDirection.SW:
                info.windDirection.text = "Kierunek Wiatru: Po³udniowo - Zachodni";
                break;
            case NewDay.WindDirection.NW:
                info.windDirection.text = "Kierunek Wiatru: Pó³nocno - Zachodni";
                break;
        }
        switch (DB.saveData[id].editableData[0].weather)
        {
            case NewDay.Weather.sun:
                info.weather.text = "Pogoda: S³onecznie";
                break;
            case NewDay.Weather.cloudy:
                info.weather.text = "Pogoda: Pochmurnie";
                break;
            case NewDay.Weather.rain:
                info.weather.text = "Pogoda: Deszczowo";
                break;
        }
    }
    public void ChangeWeatherData()
    {
        info.weatherTimeValue.text = DB.saveData[actualEdit].editableData[(int)info.timeValue.value].editTime.Hours.ToString().PadLeft(2, '0') + ":" + DB.saveData[actualEdit].editableData[(int)info.timeValue.value].editTime.Minutes.ToString().PadLeft(2, '0');
        info.temperature.text = "Temperatura: " + DB.saveData[actualEdit].editableData[(int)info.timeValue.value].temperature + "°C";
        info.windValue.text = "Si³a Wiatru: " + DB.saveData[actualEdit].editableData[(int)info.timeValue.value].windValue + " km/h";
        //info.windDirection.text = DB.saveData[actualEdit].editableData[(int)info.timeValue.value].windValue;
        switch (DB.saveData[actualEdit].editableData[(int)info.timeValue.value].windDirection)
        {
            case NewDay.WindDirection.N:
                info.windDirection.text = "Kierunek Wiatru: Pó³nocny";
                break;
            case NewDay.WindDirection.E:
                info.windDirection.text = "Kierunek Wiatru: Wschodni";
                break;
            case NewDay.WindDirection.S:
                info.windDirection.text = "Kierunek Wiatru: Po³udniowy";
                break;
            case NewDay.WindDirection.W:
                info.windDirection.text = "Kierunek Wiatru: Zachodni";
                break;
            case NewDay.WindDirection.NE:
                info.windDirection.text = "Kierunek Wiatru: Pó³nocno - Wschodni";
                break;
            case NewDay.WindDirection.SE:
                info.windDirection.text = "Kierunek Wiatru: Po³udniowo - Wschodni";
                break;
            case NewDay.WindDirection.SW:
                info.windDirection.text = "Kierunek Wiatru: Po³udniowo - Zachodni";
                break;
            case NewDay.WindDirection.NW:
                info.windDirection.text = "Kierunek Wiatru: Pó³nocno - Zachodni";
                break;
        }
        switch (DB.saveData[actualEdit].editableData[(int)info.timeValue.value].weather)
        {
            case NewDay.Weather.sun:
                info.weather.text = "Pogoda: S³onecznie";
                break;
            case NewDay.Weather.cloudy:
                info.weather.text = "Pogoda: Pochmurnie";
                break;
            case NewDay.Weather.rain:
                info.weather.text = "Pogoda: Deszczowo";
                break;
        }
    }
    public void Exit()
    {
        readSaveDay.SetActive(false);
        info.timeValue.value = 0;
        editBehaviour.formFish.fish.text = "";
        editBehaviour.formFish.bait.text = "";
    }
    public void OpenSaveFishesList()
    {
        int counter = 0;
        readSaveFishesList.SetActive(true);
        for (int hour = 23; hour >= 0; hour--)
        {
            for (int minute = 59; minute >= 0; minute--)
            {
                foreach (var item in DB.saveData[actualEdit].fishes)
                {
                    if (hour == item.hour && minute == item.minute)
                    {
                        Debug.Log(item.fish + " " + parent.transform.childCount);
                        var save = DB.saveData[actualEdit].fishes[DB.saveData[actualEdit].fishes.IndexOf(item)];
                        var newFish = Instantiate(this.prefab);
                        newFish.transform.parent = parent.transform;
                        if(parent.transform.childCount == 1)
                        {
                            newFish.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, /*(-100 * parent.transform.childCount) */- 50);
                        }
                        //newFish.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter * Screen.width/1080));
                        newFish.transform.Find("Name").GetComponent<TMP_Text>().text = save.fish;
                        newFish.transform.Find("Bait").GetComponent<TMP_Text>().text = save.bait;
                        newFish.transform.Find("Time").GetComponent<TMP_Text>().text = save.hour.ToString().PadLeft(2, '0') + ":" + save.minute.ToString().PadLeft(2, '0');
                        newFish.transform.localScale = new Vector2(1, 1);
                        //newFish.GetComponent<ScriptInPrefab>().id = counter;
                        newFish.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
                        //newFish.name = "ID_" + counter;
                        newFish.name = "ID_" + DB.saveData[actualEdit].fishes.IndexOf(item);
                        newFish.GetComponent<ScriptInPrefab>().id = DB.saveData[actualEdit].fishes.IndexOf(item);
                        if (counter % 2 != 0)
                        {
                            newFish.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                        }
                        counter++;
                    }
                }
            }
        }
        ScrollBehaviour.SetStartPositionFishes();
        ScrollBehaviour.CheckFishesScroll();
    }
    public void ExitFishesList()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
        readSaveFishesList.SetActive(false);
    }
    public void OpenFish(int id)
    {
        actualEditFish = id;
        readSaveFish.SetActive(true);
        fishInfo.fish.text = "Ryba: " + DB.saveData[actualEdit].fishes[id].fish;
        fishInfo.length.text = "D³ugoœæ: " + DB.saveData[actualEdit].fishes[id].length;
        fishInfo.weight.text = "Waga: " + DB.saveData[actualEdit].fishes[id].weight;
        fishInfo.bait.text = "Przynêta: " + DB.saveData[actualEdit].fishes[id].bait;
        fishInfo.groundbait.text = "Zanêta: " + DB.saveData[actualEdit].fishes[id].groundBait;
        fishInfo.time.text = "Godzina Z³owienia: " + DB.saveData[actualEdit].fishes[id].hour.ToString().PadLeft(2,'0') + ":" + DB.saveData[actualEdit].fishes[id].minute.ToString().PadLeft(2, '0');
    }
    public void ExitFish()
    {
        readSaveFish.SetActive(false);
    }
    //Delete
    public void OpenTabDeleteSaveDay()
    {
        delete.SetActive(true);
    }
    public void DeleteSaveDay()
    {
        //zmiana id
        foreach (var months in AddBehaviour.months)
        {
            if (months.isActive)
            {
                AddBehaviour.OpenSaveMonth(months.month.GetComponent<ScriptInMonthPrefab>().id);
                months.isActive = true;
            }
            foreach (var saves in months.saves)
            {
                if(saves.GetComponent<ScriptInPrefab>().savesId > actualOpen.GetComponent<ScriptInPrefab>().savesId)
                {
                    saves.GetComponent<ScriptInPrefab>().savesId--;
                }
                if(saves.GetComponent<ScriptInPrefab>().id > actualEdit)
                {
                    saves.name = "ID_" + (saves.GetComponent<ScriptInPrefab>().id - 1);
                    saves.GetComponent<ScriptInPrefab>().id--;
                }
            }
        }
        foreach (var dbSave in DB.saveData)
        {
            if(dbSave.savesId > actualOpen.GetComponent<ScriptInPrefab>().savesId)
            {
                dbSave.savesId--;
            }
        }
        foreach (var item in AddBehaviour.months[actualOpen.GetComponent<ScriptInPrefab>().monthId].saves)
        {
            if (item.transform.Find("background").GetComponent<RawImage>().color == new Color(0.2570755f, 0.5f, 0.4447899f))
            {
                item.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
            }
            else
            {
                item.transform.Find("background").GetComponent<RawImage>().color = new Color(0.2570755f, 0.5f, 0.4447899f);
            }
        }
        AddBehaviour.months[actualOpen.GetComponent<ScriptInPrefab>().monthId].month.GetComponent<ScriptInMonthPrefab>().saveCount--;
        AddBehaviour.months[actualOpen.GetComponent<ScriptInPrefab>().monthId].month.GetComponent<ScriptInMonthPrefab>().CountSaves();
        //usuwanie wirtualne
        AddBehaviour.months[actualOpen.GetComponent<ScriptInPrefab>().monthId].saves.Remove(actualOpen);
        AddBehaviour.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, AddBehaviour.CheckMainSideHeight());
        DB.saveData.RemoveAt(actualEdit);
        AddBehaviour.counter--;
        DestroyImmediate(actualOpen);
        foreach (var month in AddBehaviour.months)
        {
            if(month.saves.Count == 0)
            {
                for (int i = AddBehaviour.months.IndexOf(month) + 1; i < AddBehaviour.months.Count; i++)
                {
                    AddBehaviour.months[i].month.name = "ID_" + (AddBehaviour.months[i].month.GetComponent<ScriptInMonthPrefab>().id - 1);
                    AddBehaviour.months[i].month.GetComponent<ScriptInMonthPrefab>().id--;
                    AddBehaviour.months[i].month.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 100);
                    foreach (var item in AddBehaviour.months[i].saves)
                    {
                        item.GetComponent<ScriptInPrefab>().monthId--;
                    }
                }
                AddBehaviour.months.Remove(month);
                DestroyImmediate(month.month);
                AddBehaviour.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, AddBehaviour.CheckMainSideHeight());
                break;
            }
        }
        delete.SetActive(false);
        readSaveDay.SetActive(false);
        editBehaviour.editDayTab.SetActive(false);
        Json.SaveData(DB);
        foreach (var month in AddBehaviour.months)
        {
            if (month.isActive)
            {
                AddBehaviour.ReloadAfterDeleteSave(AddBehaviour.months.IndexOf(month));
            }
        }

    }
    public void CancelDeleteSaveDay()
    {
        delete.SetActive(false);
    }
    //Delete Fish
    public void OpenTabDeleteSaveFish()
    {
        deleteFish.SetActive(true);
    }
    public void DeleteSaveFish()
    {
        DestroyImmediate(parent.transform.Find("ID_" + actualEditFish).gameObject);
        for (int i = actualEditFish + 1; i < parent.transform.childCount; i++)
        {
            //parent.transform.Find("ID_" + actualOpen.GetComponent<ScriptInPrefab>().monthId).Find("Saves").Find("ID_" + i).transform.position = new Vector2(parent.transform.Find("ID_" + i).transform.position.x, parent.transform.Find("ID_" + i).transform.position.y + (100 * Screen.width / 1080));
            parent.transform.Find("ID_" + i).GetComponent<ScriptInPrefab>().id = i - 1;
            parent.transform.Find("ID_" + i).name = "ID_" + (i - 1);
        }
        DB.saveData[actualEdit].fishes.RemoveAt(actualEditFish);
        deleteFish.SetActive(false);
        readSaveFish.SetActive(false);
        Json.SaveData(DB);
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (i % 2 == 0)
            {
                parent.transform.GetChild(i).transform.Find("background").GetComponent<RawImage>().color = new Color(0.2570755f, 0.5f, 0.4447899f);
            }
            else
            {
                parent.transform.GetChild(i).transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
            }
        }
        //ilosc ryb na spisie wyjazdów
        string prefab = "Brak Ryb";
        var fishCount = DB.saveData[actualEdit].fishes.Count;
        if (fishCount == 0)
        {
            prefab = "Brak Ryb";
        }
        else if (fishCount == 1)
        {
            prefab = fishCount + " Ryba";
        }
        else if (fishCount > 1 && fishCount < 5)
        {
            prefab = fishCount + " Ryby";
        }
        else if (fishCount > 4 && fishCount % 10 != 2 && fishCount % 10 != 3 && fishCount % 10 != 4)
        {
            prefab = fishCount + " Ryb";
        }
        else if (fishCount > 4 && (fishCount % 10 == 2 || fishCount % 10 == 3 || fishCount % 10 == 4))
        {
            prefab = fishCount + " Ryby";
        }
        AddBehaviour.parent.transform.Find("ID_" + actualOpen.GetComponent<ScriptInPrefab>().monthId).Find("Saves").Find("ID_" + actualEdit).Find("FishCount").GetComponent<TMP_Text>().text = prefab;
        foreach (var month in AddBehaviour.months)
        {
            foreach (var save in month.saves)
            {
                if (save.GetComponent<ScriptInPrefab>().id == actualEdit)
                {
                    month.month.GetComponent<ScriptInMonthPrefab>().fishCount--;
                    month.month.GetComponent<ScriptInMonthPrefab>().CountFishes();
                    break;
                }
            }
        }
    }
    public void CancelDeleteSaveFish()
    {
        deleteFish.SetActive(false);
    }
    public void OpenStatsOfSave()
    {
        stats.OpenStats(actualEdit);
    }
    public void OpenDeleteSaveWeather()
    {
        deleteWeather.SetActive(true);
    }
    public void DeleteSaveWeather()
    {
        DB.saveData[actualEdit].editableData.RemoveAt((int)editBehaviour.form.timeValue.value);
        editBehaviour.form.timeValue.value = 0;
        info.timeValue.value = 0;
        foreach (var months in AddBehaviour.months)
        {
            foreach (var saves in months.saves)
            {
                if (saves.name == "ID_" + actualEdit)
                {
                    OpenSaveDayTab(actualEdit, saves);
                }
            }
        }
        deleteWeather.SetActive(false);
        editBehaviour.OpenEditDay();
        editBehaviour.editWeatherTab.SetActive(false);
        Json.SaveData(DB);
    }
}
[System.Serializable]
public class Info
{
    public TMP_Text date;
    public TMP_Text lake;
    public TMP_Text place;
    public TMP_Text lastRain;
    public TMP_Text startTime;
    public TMP_Text endTime;
    public Slider timeValue;
    public TMP_Text weatherTimeValue;
    public TMP_Text temperature;
    public TMP_Text windValue;
    public TMP_Text windDirection;
    public TMP_Text weather;
}
[System.Serializable]
public class FishInfo
{
    public TMP_Text fish;
    public TMP_Text length;
    public TMP_Text weight;
    public TMP_Text bait;
    public TMP_Text groundbait;
    public TMP_Text time;
}
