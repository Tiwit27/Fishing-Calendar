using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ActualDayEditor : FishForm
{
    [SerializeField] public bool isTicketOpen;
    [SerializeField] public GameObject editDay;
    [SerializeField] DataBase DB;
    [SerializeField] JsonBehaviour Json;
    [SerializeField] EditForm form;
    [SerializeField] public GameObject addFishTab;
    [SerializeField] ReadSaveInGame readSaveInGame;
    [SerializeField] public GameObject plus;
    [SerializeField] public GameObject backArrow;
    [SerializeField] TMP_Text lastUpdate;
    [SerializeField] public GameObject mainSide;
    [SerializeField] GameObject endDayTab;
    [SerializeField] GameObject editButton;
    int counter;
    [SerializeField] TMP_InputField[] endTime = new TMP_InputField[2];
    bool reloadTime = true;
    [SerializeField] public GameObject trash;
    [SerializeField] public GameObject trashWeather;
    [SerializeField] public NotificationsBehaviour notifications;
    [SerializeField] public AddBehaviour addBehaviour;
    public GameObject stats;
    [SerializeField] StatsBehaviour statsBehaviour;

    void Start()
    {
        endDayTab.SetActive(false);
        editDay.SetActive(false);
        addFishTab.SetActive(false);
        backArrow.SetActive(false);
        string path = Application.persistentDataPath + "/Save.json";
        if(System.IO.File.Exists(path))
        {
            Json.ReadData();
        }
        if (PlayerPrefs.GetInt("bool") == 1 && DB.saveData.Count > 0)
        {
            OpenTicket(PlayerPrefs.GetInt("id"), DB.saveData[PlayerPrefs.GetInt("id")]);
        }
        else
        {
            PlayerPrefs.SetInt("bool", 0);
            PlayerPrefs.DeleteKey("id");
            PlayerPrefs.Save();
        }
        StartCoroutine(ReloadTime());
    }
    public void OpenTicket(int idTicket, NewDay info)
    {
        if (PlayerPrefs.GetInt("bool") != 1)
        {
            PlayerPrefs.SetInt("bool", 1);
            PlayerPrefs.SetInt("id", idTicket);
            PlayerPrefs.Save();
        }
        stats.transform.position = plus.transform.position;
        editButton.SetActive(false);
        trash.SetActive(false);
        trashWeather.SetActive(false);
        readSaveInGame.actualEdit = idTicket;
        readSaveInGame.plus.SetActive(false);
        plus.SetActive(false);
        backArrow.SetActive(true);
        editDay.SetActive(true);
        foreach (var item in addBehaviour.months)
        {
            foreach (var save in item.saves)
            {
                if (save.GetComponent<ScriptInPrefab>().id == PlayerPrefs.GetInt("id"))
                {
                    readSaveInGame.actualOpen = save;
                }
            }
        }
        form.temperature.text = info.editableData[info.editableData.Count - 1].temperature.ToString();
        form.windValue.text = info.editableData[info.editableData.Count - 1].windValue.ToString();
        form.windDirection.value = (int)info.editableData[info.editableData.Count - 1].windDirection;
        form.weather.value = (int)info.editableData[info.editableData.Count - 1].weather;
        form.editHour.text = System.DateTime.Now.ToString("HH").PadLeft(2,'0');
        form.editMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
    }
    public void ChangeActualEdit()
    {
        readSaveInGame.actualEdit = PlayerPrefs.GetInt("id");
    }
    //update data in ticket
    public void ChangeTemperature()
    {
        if (form.temperature.text.Length > 0)
        {
            form.temperature.text = form.temperature.text.Replace('.', '-');
            for (int i = 1; i < form.temperature.text.Length; i++)
            {
                if (form.temperature.text[i] == '-')
                {
                    form.temperature.text = "";
                }
            }
        }
        if (form.temperature.text == "-0" || form.temperature.text == "-00" || form.temperature.text == "-" || form.temperature.text == "")
        {
            form.temperature.text = "";
        }
        else
        {
            if (int.Parse(form.temperature.text) > 50 || int.Parse(form.temperature.text) < -20)
            {
                form.temperature.text = "";
            }
        }
    }
    public void ChangeWindValue()
    {
        if (form.windValue.text == "-0" || form.windValue.text == "-00" || form.windValue.text == "-" || form.windValue.text == "")
        {
            form.windValue.text = "";
        }
        else
        {
            if (int.Parse(form.windValue.text) > 99 || int.Parse(form.windValue.text) < 0)
            {
                form.windValue.text = "";
            }
        }
    }
    public void ChangeEditHour()
    {
        if (form.editHour.text.Length > 0 && form.editMinute.text.Length > 0)
        {
            if (form.editHour.text[0] != '-' && form.editMinute.text[0] != '-')
            {
                int hour = int.Parse(form.editHour.text);
                int minute = int.Parse(form.editMinute.text);
                if (hour < 0 || hour > 23)
                {
                    form.editHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
                }
                else
                {
                    form.editHour.text = form.editHour.text.ToString().PadLeft(2, '0');
                }
                if (minute < 0 || minute > 59)
                {
                    form.editMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
                }
                else
                {
                    form.editMinute.text = form.editMinute.text.ToString().PadLeft(2, '0');
                }
                var time = new System.TimeSpan(hour, minute, 0);
                var startTime = new System.TimeSpan(DB.saveData[PlayerPrefs.GetInt("id")].startHour, DB.saveData[PlayerPrefs.GetInt("id")].startMinute, 0);
                if(time < startTime)
                {
                    form.editHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
                    form.editMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
                }
            }
            else
            {
                form.editHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
                form.editMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
            }
        }
        else
        {
            form.editHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
            form.editMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
        }
    }
    public void StopReloadTime()
    {
        reloadTime = false;
        Invoke("Wait", 60);
    }
    void Wait()
    {
        reloadTime = true;
    }
    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(30);
        if(reloadTime == true)
        {
            form.editHour.text = System.DateTime.Now.ToString("HH").PadLeft(2,'0');
            form.editMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
        }
        StartCoroutine(ReloadTime());
    }
    public void UpdateData()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.temperature.text.Length >= 1 && form.windValue.text.Length >= 1 && form.editHour.text.Length >= 1 && form.editMinute.text.Length >= 1)
        {
            var data = new EditableData();
            data.temperature = int.Parse(form.temperature.text);
            data.windValue = int.Parse(form.windValue.text);
            switch (form.windDirection.value)
            {
                case 0:
                    data.windDirection = NewDay.WindDirection.N;
                    break;
                case 1:
                    data.windDirection = NewDay.WindDirection.E;
                    break;
                case 2:
                    data.windDirection = NewDay.WindDirection.S;
                    break;
                case 3:
                    data.windDirection = NewDay.WindDirection.W;
                    break;
                case 4:
                    data.windDirection = NewDay.WindDirection.NE;
                    break;
                case 5:
                    data.windDirection = NewDay.WindDirection.SE;
                    break;
                case 6:
                    data.windDirection = NewDay.WindDirection.SW;
                    break;
                case 7:
                    data.windDirection = NewDay.WindDirection.NW;
                    break;
            }
            switch (form.weather.value)
            {
                case 0:
                    data.weather = NewDay.Weather.sun;
                    break;
                case 1:
                    data.weather = NewDay.Weather.cloudy;
                    break;
                case 2:
                    data.weather = NewDay.Weather.rain;
                    break;
            }
            data.editHour = int.Parse(form.editHour.text);
            data.editMinute = int.Parse(form.editMinute.text);
            data.editTime = new System.TimeSpan(int.Parse(form.editHour.text), int.Parse(form.editMinute.text), 0);
            DB.saveData[PlayerPrefs.GetInt("id")].editableData.Add(data);
            DB.saveData[PlayerPrefs.GetInt("id")].editableData = DB.saveData[PlayerPrefs.GetInt("id")].editableData.OrderBy(x => x.editTime).ToList();
            Json.SaveData(DB);
            lastUpdate.text = "Ostatnia aktualizacja: " + DB.saveData[PlayerPrefs.GetInt("id")].editableData[DB.saveData[PlayerPrefs.GetInt("id")].editableData.Count - 1].editTime.Hours.ToString().PadLeft(2, '0') + ":" + DB.saveData[PlayerPrefs.GetInt("id")].editableData[DB.saveData[PlayerPrefs.GetInt("id")].editableData.Count - 1].editTime.Minutes.ToString().PadLeft(2, '0');
            //clear
            form.temperature.GetComponentInParent<Image>().color = color;
            form.windValue.GetComponentInParent<Image>().color = color;
        }
        else
        {
            if (form.temperature.text.Length < 1)
            {
                form.temperature.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.temperature.GetComponentInParent<Image>().color = color;
            }
            if (form.windValue.text.Length < 1)
            {
                form.windValue.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.windValue.GetComponentInParent<Image>().color = color;
            }
            if (form.editHour.text.Length < 1)
            {
                form.editHour.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.editHour.GetComponentInParent<Image>().color = color;
            }
            if (form.editMinute.text.Length < 1)
            {
                form.editMinute.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.editMinute.GetComponentInParent<Image>().color = color;
            }
        }
    }
    //addFish
    public void OpenTabAddFish()
    {
        addFishTab.SetActive(true);
        hour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
        minute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
    }
    public void ChangeLength()
    {
        if(length.text.Length > 0)
        {
            if (length.text[0] != '-')
            {
                if (int.Parse(length.text) <= 0)
                {
                    length.text = "";
                }
            }
            else
            {
                length.text = "";
            }
        }
        else
        {
            length.text = "";
        }
    }
    public void ChangeWeight()
    {
        if (weight.text.Length > 0)
        {
            weight.text = weight.text.Replace('.', ',');
            if (weight.text[0] == ',')
            {
                weight.text = "0" + weight.text;
            }
            if (weight.text[0] != '-')
            {
                if (float.Parse(weight.text) <= 0)
                {
                    weight.text = "";
                }
            }
            else
            {
                weight.text = "";
            }
        }
        else
        {
            weight.text = "";
        }
    }
    public void ChangeTimeChatch()
    {
        if (hour.text.Length > 0 && minute.text.Length > 0)
        {
            if (hour.text[0] != '-' && minute.text[0] != '-')
            {
                if (int.Parse(hour.text) < 0 || int.Parse(minute.text) < 0 || int.Parse(hour.text) > 23 || int.Parse(minute.text) > 59)
                {
                    hour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
                    minute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
                }
                else
                {
                    hour.text = hour.text.ToString().PadLeft(2, '0');
                    minute.text = minute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                hour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
                minute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
            }
        }
        else
        {
            hour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
            minute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
        }
    }
    public void AddFish()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        //add
        if (fish.text.Length >= 2 && bait.text.Length >= 2)
        {
            var lengthConv = 0;
            var weightConv = 0f;
            if (length.text.Length > 0)
            {
                lengthConv = int.Parse(length.text);
            }
            if (weight.text.Length > 0)
            {
                weightConv = float.Parse(weight.text);
            }
            var data = new NewFish(fish.text, lengthConv, weightConv, bait.text, groundBait.text, int.Parse(hour.text), int.Parse(minute.text));
            DB.saveData[PlayerPrefs.GetInt("id")].fishes.Add(data);
            Json.SaveData(DB);
            //ryby
            string prefab = "Brak Ryb";
            var fishCount = DB.saveData[PlayerPrefs.GetInt("id")].fishes.Count;
            if (fishCount == 0)
            {
                prefab = "Brak Ryb";
            }
            else if(fishCount == 1)
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
            foreach (var item in addBehaviour.months)
            {
                foreach (var save in item.saves)
                {
                    if (save.GetComponent<ScriptInPrefab>().id == PlayerPrefs.GetInt("id"))
                    {
                        mainSide.transform.Find("ID_" + save.GetComponent<ScriptInPrefab>().monthId).Find("Saves").Find("ID_" + PlayerPrefs.GetInt("id")).Find("FishCount").GetComponent<TMP_Text>().text = prefab;
                    }
                }
            }
            foreach (var month in addBehaviour.months)
            {
                foreach (var save in month.saves)
                {
                    if (save.GetComponent<ScriptInPrefab>().id == readSaveInGame.actualEdit)
                    {
                        month.month.GetComponent<ScriptInMonthPrefab>().fishCount++;
                        month.month.GetComponent<ScriptInMonthPrefab>().CountFishes();
                        break;
                    }
                }
            }
            //clear
            length.text = "";
            weight.text = "";
            //exit
            addFishTab.SetActive(false);
        }
        else
        {
            //check
            if (fish.text.Length < 2)
            {
                fish.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                fish.GetComponentInParent<Image>().color = color;
            }
            if (bait.text.Length < 2)
            {
                bait.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                bait.GetComponentInParent<Image>().color = color;
            }
        }
    }
    public void CancelAddFish()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        length.text = "";
        weight.text = "";
        addFishTab.SetActive(false);
        fish.GetComponent<Image>().color = color;
        bait.GetComponent<Image>().color = color;
    }
    //end day
    public void EndDayOpen()
    {
        endDayTab.SetActive(true);
        endTime[0].text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
        endTime[1].text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
    }
    public void CancelEndDay()
    {
        endDayTab.SetActive(false);
    }
    public void EndDay()
    {
        DB.saveData[PlayerPrefs.GetInt("id")].endHour = int.Parse(endTime[0].text);
        DB.saveData[PlayerPrefs.GetInt("id")].endMinute = int.Parse(endTime[1].text);
        PlayerPrefs.SetInt("bool", 0);
        stats.transform.position = backArrow.transform.position;
        //obliczanie czasu
        var db = DB.saveData[PlayerPrefs.GetInt("id")];
        //start
        System.DateTime startDate = new System.DateTime(db.year,db.month,db.day,db.startHour,db.startMinute, 0);
        System.DateTime endDate = new System.DateTime(db.year, db.month, db.day, db.endHour, db.endMinute, 0);
        var hours = (int)((endDate - startDate).TotalMinutes) / 60;
        var minutes = (int)((endDate - startDate).TotalMinutes) % 60;
        notifications.SendEndNotification(hours, minutes, db.fishes.Count);
        PlayerPrefs.DeleteKey("id");
        PlayerPrefs.Save();
        editButton.SetActive(true);
        trash.SetActive(true);
        trashWeather.SetActive(true);
        readSaveInGame.plus.SetActive(true);
        endDayTab.SetActive(false);
        editDay.SetActive(false);
        plus.SetActive(true);
        backArrow.SetActive(false);
        Json.SaveData(DB);
    }
    public void ChangeTimeOnEndDayTab()
    {
        if (endTime[0].text.Length > 0 && endTime[0].text[0] != '-' && endTime[1].text.Length > 0 && endTime[1].text[0] != '-')
        {
            if (int.Parse(endTime[0].text) > 23 || int.Parse(endTime[0].text) < 0)
            {
                endTime[0].text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
            }
            else
            {
                endTime[0].text = endTime[0].text.PadLeft(2, '0');
            }
            if (int.Parse(endTime[1].text) > 59 || int.Parse(endTime[1].text) < 0)
            {
                endTime[1].text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
            }
            else
            {
                endTime[1].text = endTime[1].text.PadLeft(2, '0');
            }
            System.TimeSpan start = new System.TimeSpan(DB.saveData[readSaveInGame.actualEdit].editableData[0].editTime.Hours, DB.saveData[readSaveInGame.actualEdit].editableData[0].editTime.Minutes, 0);
            System.TimeSpan end = new System.TimeSpan(int.Parse(endTime[0].text), int.Parse(endTime[1].text), 0);
            if(end < start)
            {
                endTime[0].text = System.DateTime.Now.Hour.ToString().PadLeft(2, '0');
                endTime[1].text = System.DateTime.Now.Minute.ToString().PadLeft(2, '0');
            }
        }
        else
        {
            endTime[0].text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
            endTime[1].text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
        }
    }
    public void OpenStats()
    {
        if(DB.saveData.Count > 0)
        {
            var endDate = new System.DateTime(DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].year, DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].month, DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].day);
            var startDate = new System.DateTime(DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].year, DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].month, DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].day);
            statsBehaviour.OpenStats(startDate, endDate);
        }
    }
}
[System.Serializable]
class EditForm
{
    public TMP_InputField temperature;
    public TMP_InputField windValue;
    public TMP_Dropdown windDirection;
    public TMP_Dropdown weather;
    public TMP_InputField editHour;
    public TMP_InputField editMinute;
}
[System.Serializable]
public class FishForm : MonoBehaviour
{
    public TMP_InputField fish;
    public TMP_InputField length;
    public TMP_InputField weight;
    public TMP_InputField bait;
    public TMP_InputField groundBait;
    public TMP_InputField hour;
    public TMP_InputField minute;
}

