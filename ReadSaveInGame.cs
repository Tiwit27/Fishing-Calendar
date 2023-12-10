using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadSaveInGame : MonoBehaviour
{
    [SerializeField] GameObject readSaveDay;
    [SerializeField] GameObject readSaveFishesList;
    [SerializeField] ActualDayEditor actualDayEditor;
    [SerializeField] Info info;
    [SerializeField] DataBase DB;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject parent;
    int actualEdit;
    public GameObject plus;

    private void Start()
    {
        readSaveFishesList.SetActive(false);
        readSaveDay.SetActive(false);
    }
    public void OpenSaveDaysList()
    {
        actualDayEditor.plus.SetActive(false);
        actualDayEditor.backArrow.SetActive(true);
        actualDayEditor.editDay.SetActive(false);
    }
    public void BackFromSaveDaysList()
    {
        actualDayEditor.editDay.SetActive(true);
        actualDayEditor.plus.SetActive(true);
        actualDayEditor.backArrow.SetActive(false);
    }
    public void OpenSaveDayTab(int id)
    {
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
        int counter = 0;
        readSaveFishesList.SetActive(true);
        foreach (var save in DB.saveData[actualEdit].fishes)
        {
            //tworzenie ryb
            var newFish = Instantiate(this.prefab);
            newFish.transform.parent = parent.transform;
            newFish.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter));
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
        }
    }
    public void ExitFishesList()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
        readSaveFishesList.SetActive(false);
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
