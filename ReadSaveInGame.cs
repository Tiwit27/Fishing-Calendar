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
    [SerializeField] Info info;
    [SerializeField] FishInfo fishInfo;
    [SerializeField] DataBase DB;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject parent;
    [SerializeField] ScrollBehaviour ScrollBehaviour;
    [SerializeField] JsonBehaviour Json;
    public int actualEdit;
    public int actualEditFish;
    public GameObject plus;
    [SerializeField] GameObject delete;
    [SerializeField] GameObject deleteFish;

    private void Start()
    {
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
    public void OpenSaveDayTab(int id)
    {
        ScrollBehaviour.SetStartPosition();
        actualEdit = id;
        readSaveDay.SetActive(true);
        info.date.text = "Data: " + DB.saveData[id].day.ToString().PadLeft(2, '0') + "." + DB.saveData[id].month.ToString().PadLeft(2, '0') + "." + DB.saveData[id].year.ToString().PadLeft(2, '0');
        info.lake.text = "£owisko: " + DB.saveData[id].lake;
        info.place.text = "Miejsce: " + DB.saveData[id].place;
        info.lastRain.text = "Ostatni Opad: " + DB.saveData[id].lastRainDay.ToString().PadLeft(2, '0') + "." + DB.saveData[id].lastRainMonth.ToString().PadLeft(2, '0');
        info.startTime.text = "Godzina startowa: " + DB.saveData[id].startHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[id].startMinute.ToString().PadLeft(2, '0');
        info.endTime.text = "Godzina koñcowa: " + DB.saveData[id].endHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[id].endMinute.ToString().PadLeft(2, '0');
        info.timeValue.maxValue = DB.saveData[id].editableData.Count - 1;
        info.weatherTimeValue.text = DB.saveData[id].editableData[0].editHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[actualEdit].editableData[0].editMinute.ToString().PadLeft(2, '0');
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
        info.weatherTimeValue.text = DB.saveData[actualEdit].editableData[(int)info.timeValue.value].editHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[actualEdit].editableData[(int)info.timeValue.value].editMinute.ToString().PadLeft(2, '0');
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
    }
    public void OpenSaveFishesList()
    {
        int counter = DB.saveData[actualEdit].fishes.Count - 1;
        readSaveFishesList.SetActive(true);
        for (int i = 0; i < DB.saveData[actualEdit].fishes.Count; i++)
        {
            var save = DB.saveData[actualEdit].fishes[counter];
            var newFish = Instantiate(this.prefab);
            newFish.transform.parent = parent.transform;
            newFish.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-100 * counter) - 50);
            //newFish.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter * Screen.width/1080));
            newFish.transform.Find("Name").GetComponent<TMP_Text>().text = save.fish;
            newFish.transform.Find("Bait").GetComponent<TMP_Text>().text = save.bait;
            newFish.transform.Find("Time").GetComponent<TMP_Text>().text = save.hour.ToString().PadLeft(2, '0') + ":" + save.minute.ToString().PadLeft(2, '0');
            newFish.transform.localScale = new Vector2(1, 1);
            newFish.GetComponent<ScriptInPrefab>().id = counter;
            newFish.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
            newFish.name = "ID_" + counter;
            if (counter % 2 != 0)
            {
                newFish.transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
            }
            counter--;
        }
            /*foreach (var save in DB.saveData[actualEdit].fishes)
            {
                //tworzenie ryb
                var newFish = Instantiate(this.prefab);
                newFish.transform.parent = parent.transform;
                newFish.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-100 * counter) - 50);
                //newFish.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter * Screen.width/1080));
                newFish.transform.Find("Name").GetComponent<TMP_Text>().text = save.fish;
                newFish.transform.Find("Bait").GetComponent<TMP_Text>().text = save.bait;
                newFish.transform.Find("Time").GetComponent<TMP_Text>().text = save.hour.ToString().PadLeft(2, '0') + ":" + save.minute.ToString().PadLeft(2, '0');
                newFish.transform.localScale = new Vector2(1, 1);
                newFish.GetComponent<ScriptInPrefab>().id = counter;
                newFish.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
                newFish.name = "ID_" + counter;
                if (counter % 2 != 0)
                {
                    newFish.transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
                }
                counter++;
            }*/
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
        Destroy(AddBehaviour.parent.transform.Find("ID_" + actualEdit).gameObject);
        for (int i = actualEdit; i < AddBehaviour.parent.transform.childCount; i++)
        {
            AddBehaviour.parent.transform.Find("ID_" + i).transform.position = new Vector2(AddBehaviour.parent.transform.Find("ID_" + i).transform.position.x, AddBehaviour.parent.transform.Find("ID_" + i).transform.position.y + (100 * Screen.width / 1080));
            AddBehaviour.parent.transform.Find("ID_" + i).GetComponent<ScriptInPrefab>().id = i - 1;
            if (i % 2 == 0)
            {
                AddBehaviour.parent.transform.Find("ID_" + i).transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
            }
            else
            {
                AddBehaviour.parent.transform.Find("ID_" + i).transform.Find("background").GetComponent<RawImage>().color = new Color(0.2570755f, 0.5f, 0.4447899f);
            }
            AddBehaviour.parent.transform.Find("ID_" + i).name = "ID_" + (i - 1);
        }
        AddBehaviour.counter--;
        DB.saveData.RemoveAt(actualEdit);
        delete.SetActive(false);
        readSaveDay.SetActive(false);
        Json.SaveData(DB);
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
        Destroy(parent.transform.Find("ID_" + actualEditFish).gameObject);
        for (int i = actualEditFish; i < parent.transform.childCount; i++)
        {
            parent.transform.Find("ID_" + i).transform.position = new Vector2(parent.transform.Find("ID_" + i).transform.position.x, parent.transform.Find("ID_" + i).transform.position.y + (100 * Screen.width / 1080));
            parent.transform.Find("ID_" + i).GetComponent<ScriptInPrefab>().id = i - 1;
            if (i % 2 == 0)
            {
                parent.transform.Find("ID_" + i).transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
            }
            else
            {
                parent.transform.Find("ID_" + i).transform.Find("background").GetComponent<RawImage>().color = new Color(0.2570755f, 0.5f, 0.4447899f);
            }
            parent.transform.Find("ID_" + i).name = "ID_" + (i - 1);
        }
        DB.saveData[actualEdit].fishes.RemoveAt(actualEditFish);
        deleteFish.SetActive(false);
        readSaveFish.SetActive(false);
        Json.SaveData(DB);
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
        AddBehaviour.parent.transform.Find("ID_" + actualEdit).Find("FishCount").GetComponent<TMP_Text>().text = prefab;
    }
    public void CancelDeleteSaveFish()
    {
        deleteFish.SetActive(false);
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
