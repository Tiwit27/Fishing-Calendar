using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EditBehaviour : MonoBehaviour
{
    [SerializeField] ReadSaveInGame ReadSave;
    [SerializeField] GameObject editDayTab;
    [SerializeField] GameObject editWeatherTab;
    [SerializeField] EditSaveDayForm form;
    [SerializeField] DataBase DB;
    [SerializeField] JsonBehaviour Json;
    [SerializeField] AddBehaviour AddBehaviour;
    [SerializeField] ActualDayEditor ActualDayEditor;
    [SerializeField] GameObject addFish;
    [SerializeField] AddFishForm formFish;
    [SerializeField] AddFishForm EditFishForm;
    [SerializeField] GameObject editFish;
    [SerializeField] GameObject addNewWeatherSave;
    [SerializeField] AddNewWeatherSave newWeatherSave;
    [SerializeField] GameObject fishesMainSide;
 
    private void Start()
    {
        addNewWeatherSave.SetActive(false);
        editFish.SetActive(false);
        addFish.SetActive(false);
        editWeatherTab.SetActive(false);
        editDayTab.SetActive(false);
    }
    public void OpenEditDay()
    {
        int id = ReadSave.actualEdit;
        editDayTab.SetActive(true);
        form.day.text = DB.saveData[id].day.ToString().PadLeft(2, '0');
        form.month.text = DB.saveData[id].month.ToString().PadLeft(2, '0');
        form.year.text = DB.saveData[id].year.ToString();
        form.lake.text = DB.saveData[id].lake;
        form.place.text = DB.saveData[id].place;
        form.lastRainDay.text = DB.saveData[id].lastRainDay.ToString().PadLeft(2, '0'); ;
        form.lastRainMonth.text = DB.saveData[id].lastRainMonth.ToString().PadLeft(2, '0');
        form.endHour.text = DB.saveData[id].endHour.ToString().PadLeft(2,'0');
        form.endMinute.text = DB.saveData[id].endMinute.ToString().PadLeft(2, '0');
        ChangeWeatherData();
    }
    public void ChangeWeatherData()
    {
        int id = ReadSave.actualEdit;
        form.timeValue.maxValue = DB.saveData[id].editableData.Count - 1;
        form.weatherTimeValue.text = DB.saveData[id].editableData[(int)form.timeValue.value].editHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[id].editableData[(int)form.timeValue.value].editMinute.ToString().PadLeft(2, '0');
        form.temperature.text = DB.saveData[id].editableData[(int)form.timeValue.value].temperature.ToString();
        form.windValue.text = DB.saveData[id].editableData[(int)form.timeValue.value].windValue.ToString();
        switch (DB.saveData[id].editableData[(int)form.timeValue.value].windDirection)
        {
            case NewDay.WindDirection.N:
                form.windDirection.value = 0;
                break;
            case NewDay.WindDirection.E:
                form.windDirection.value = 1;
                break;
            case NewDay.WindDirection.S:
                form.windDirection.value = 2;
                break;
            case NewDay.WindDirection.W:
                form.windDirection.value = 3;
                break;
            case NewDay.WindDirection.NE:
                form.windDirection.value = 4;
                break;
            case NewDay.WindDirection.SE:
                form.windDirection.value = 5;
                break;
            case NewDay.WindDirection.SW:
                form.windDirection.value = 6;
                break;
            case NewDay.WindDirection.NW:
                form.windDirection.value = 7;
                break;
        }
        switch (DB.saveData[id].editableData[(int)form.timeValue.value].weather)
        {
            case NewDay.Weather.sun:
                form.weather.value = 0;
                break;
            case NewDay.Weather.cloudy:
                form.weather.value = 1;
                break;
            case NewDay.Weather.rain:
                form.weather.value = 2;
                break;
        }
        form.editHour.text = DB.saveData[id].editableData[(int)form.timeValue.value].editHour.ToString().PadLeft(2, '0');
        form.editMinute.text = DB.saveData[id].editableData[(int)form.timeValue.value].editMinute.ToString().PadLeft(2, '0');
        form.time.text = "Edycja zapisu z godziny: " + DB.saveData[id].editableData[(int)form.timeValue.value].editHour.ToString().PadLeft(2, '0') + ":" + DB.saveData[id].editableData[(int)form.timeValue.value].editMinute.ToString().PadLeft(2, '0');
    }
    public void OpenEditWeather()
    {
        editWeatherTab.SetActive(true);
    }
    //validation
    public void ChangeTemperature()
    {
        if (form.temperature.text == "-0" || form.temperature.text == "-00" || form.temperature.text == "-" || form.temperature.text == "")
        {
            form.temperature.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].temperature.ToString();
        }
        else
        {
            if (int.Parse(form.temperature.text) > 50 || int.Parse(form.temperature.text) < -20)
            {
                form.temperature.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].temperature.ToString();
            }
        }
    }
    public void ChangeWindValue()
    {
        if (form.windValue.text == "-0" || form.windValue.text == "-00" || form.windValue.text == "-" || form.windValue.text == "")
        {
            form.windValue.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windValue.ToString();
        }
        else
        {
            if (int.Parse(form.windValue.text) > 99 || int.Parse(form.windValue.text) < 0)
            {
                form.windValue.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windValue.ToString();
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
                    form.editHour.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editHour.ToString().PadLeft(2, '0');
                }
                else
                {
                    form.editHour.text = form.editHour.text.ToString().PadLeft(2, '0');
                }
                if (minute < 0 || minute > 59)
                {
                    form.editMinute.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editMinute.ToString().PadLeft(2, '0');
                }
                else
                {
                    form.editMinute.text = form.editMinute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                form.editHour.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editHour.ToString().PadLeft(2, '0');
                form.editMinute.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editMinute.ToString().PadLeft(2, '0');
            }
        }
        else
        {
            form.editHour.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editHour.ToString().PadLeft(2, '0');
            form.editMinute.text = DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editMinute.ToString().PadLeft(2, '0');
        }
    }
    //
    public void ChangeDate()
    {
        if (form.month.text.Length > 0 && form.day.text.Length > 0 && form.year.text.Length > 0)
        {
            if (form.month.text[0] != '-' && form.day.text[0] != '-' && form.year.text[0] != '-')
            {
                var month = int.Parse(form.month.text);
                var day = int.Parse(form.day.text);
                var year = int.Parse(form.year.text);
                if (day > 0)
                {
                    //month
                    if (month <= 0 || month > 12)
                    {
                        form.month.text = DB.saveData[ReadSave.actualEdit].month.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        form.month.text = form.month.text.ToString().PadLeft(2, '0');
                    }
                    //day
                    if (month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        if (day > 30)
                        {
                            form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            form.day.text = form.day.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    {
                        if (day > 31)
                        {
                            form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            form.day.text = form.day.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (month == 2)
                    {
                        if (System.DateTime.IsLeapYear(year) && day > 29)
                        {
                            form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0');
                        }
                        else if (!System.DateTime.IsLeapYear(year) && day > 28)
                        {
                            form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            form.day.text = form.day.text.ToString().PadLeft(2, '0');
                        }
                    }
                }
                else
                {
                    form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0');
                }
                //year
                if (year < 2010 || year > int.Parse(System.DateTime.Now.ToString("yyyy")))
                {
                    form.year.text = DB.saveData[ReadSave.actualEdit].year.ToString().PadLeft(4,'0');
                }
            }
            else
            {
                form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2,'0');
                form.month.text = DB.saveData[ReadSave.actualEdit].month.ToString().PadLeft(2,'0');
                form.year.text = DB.saveData[ReadSave.actualEdit].year.ToString().PadLeft(4,'0');
            }
        }
        else
        {
            form.day.text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0');
            form.month.text = DB.saveData[ReadSave.actualEdit].month.ToString().PadLeft(2, '0');
            form.year.text = DB.saveData[ReadSave.actualEdit].year.ToString().PadLeft(4, '0');
        }
    }
    public void ChangeLastRain()
    {
        if (form.lastRainMonth.text.Length > 0 && form.lastRainDay.text.Length > 0)
        {
            if (form.lastRainMonth.text[0] != '-' && form.lastRainDay.text[0] != '-')
            {
                var month = int.Parse(form.lastRainMonth.text);
                var day = int.Parse(form.lastRainDay.text);
                if (day >= 0)
                {
                    //month
                    if (month <= 0 || month > 12)
                    {
                        form.lastRainMonth.text = DB.saveData[ReadSave.actualEdit].lastRainMonth.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        form.lastRainMonth.text = form.lastRainMonth.text.ToString().PadLeft(2, '0');
                    }
                    //day
                    if (month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        if (day > 30)
                        {
                            form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            form.lastRainDay.text = form.lastRainDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    {
                        if (day > 31)
                        {
                            form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            form.lastRainDay.text = form.lastRainDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (month == 2)
                    {
                        if (System.DateTime.IsLeapYear(int.Parse(form.year.text)) && day > 29)
                        {
                            form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
                        }
                        else if (day > 28)
                        {
                            form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            form.lastRainDay.text = form.lastRainDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                }
                else
                {
                    form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
                form.lastRainMonth.text = DB.saveData[ReadSave.actualEdit].lastRainMonth.ToString().PadLeft(2, '0');
            }
        }
        else
        {
            form.lastRainDay.text = DB.saveData[ReadSave.actualEdit].lastRainDay.ToString().PadLeft(2, '0');
            form.lastRainMonth.text = DB.saveData[ReadSave.actualEdit].lastRainMonth.ToString().PadLeft(2, '0');
        }
    }
    public void ChangeEndHour()
    {
        if (form.endHour.text.Length > 0 && form.endMinute.text.Length > 0)
        {
            if (form.endHour.text[0] != '-' && form.endMinute.text[0] != '-')
            {
                int hour = int.Parse(form.endHour.text);
                int minute = int.Parse(form.endMinute.text);
                if (hour < 0 || hour > 23)
                {
                    form.endHour.text = DB.saveData[ReadSave.actualEdit].endHour.ToString().PadLeft(2, '0');
                }
                else
                {
                    form.endHour.text = form.endHour.text.ToString().PadLeft(2, '0');
                }
                if (minute < 0 || minute > 59)
                {
                    form.endMinute.text = DB.saveData[ReadSave.actualEdit].endMinute.ToString().PadLeft(2, '0');
                }
                else
                {
                    form.endMinute.text = form.endMinute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                form.endHour.text = DB.saveData[ReadSave.actualEdit].endHour.ToString().PadLeft(2, '0');
                form.endMinute.text = DB.saveData[ReadSave.actualEdit].endMinute.ToString().PadLeft(2, '0');
            }
        }
        else
        {
            form.endHour.text = DB.saveData[ReadSave.actualEdit].endHour.ToString().PadLeft(2, '0');
            form.endMinute.text = DB.saveData[ReadSave.actualEdit].endMinute.ToString().PadLeft(2, '0');
        }
    }
    public void SaveNewData()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.lake.text.Length > 2 && form.place.text.Length > 5)
        {
            DB.saveData[ReadSave.actualEdit].day = int.Parse(form.day.text);
            DB.saveData[ReadSave.actualEdit].month = int.Parse(form.month.text);
            DB.saveData[ReadSave.actualEdit].year = int.Parse(form.year.text);
            DB.saveData[ReadSave.actualEdit].lake = form.lake.text;
            DB.saveData[ReadSave.actualEdit].place = form.place.text;
            DB.saveData[ReadSave.actualEdit].lastRainDay = int.Parse(form.lastRainDay.text);
            DB.saveData[ReadSave.actualEdit].lastRainMonth = int.Parse(form.lastRainMonth.text);
            DB.saveData[ReadSave.actualEdit].endHour = int.Parse(form.endHour.text);
            DB.saveData[ReadSave.actualEdit].endMinute = int.Parse(form.endMinute.text);
            form.lake.GetComponentInParent<Image>().color = color;
            form.place.GetComponentInParent<Image>().color = color;
            Json.SaveData(DB);
            editDayTab.SetActive(false);
            ReadSave.OpenSaveDayTab(ReadSave.actualEdit);
            AddBehaviour.parent.transform.Find("ID_" + ReadSave.actualEdit).Find("Name").GetComponent<TMP_Text>().text = DB.saveData[ReadSave.actualEdit].lake;
            AddBehaviour.parent.transform.Find("ID_" + ReadSave.actualEdit).Find("data").GetComponent<TMP_Text>().text = DB.saveData[ReadSave.actualEdit].day.ToString().PadLeft(2, '0') + "." + DB.saveData[ReadSave.actualEdit].month.ToString().PadLeft(2, '0') + "." + DB.saveData[ReadSave.actualEdit].year.ToString();
        }
        else
        {
            if(form.lake.text.Length < 3)
            {
                form.lake.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.lake.GetComponentInParent<Image>().color = color;
            }
            if (form.place.text.Length < 6)
            {
                form.place.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.place.GetComponentInParent<Image>().color = color;
            }
        }
    }
    //save new data
    public void SaveNewWeatherData()
    {
        DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].temperature = int.Parse(form.temperature.text);
        DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windValue = int.Parse(form.windValue.text);
        DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editHour = int.Parse(form.editHour.text);
        DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].editMinute = int.Parse(form.editMinute.text);
        switch (form.windDirection.value)
        {
            case 0:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.N;
                break;
            case 1:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.E;
                break;
            case 2:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.S;
                break;
            case 3:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.W;
                break;
            case 4:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.NE;
                break;
            case 5:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.SE;
                break;
            case 6:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.SW;
                break;
            case 7:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].windDirection = NewDay.WindDirection.NW;
                break;
        }
        switch (form.weather.value)
        {
            case 0:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].weather = NewDay.Weather.sun;
                break;
            case 1:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].weather = NewDay.Weather.cloudy;
                break;
            case 2:
                DB.saveData[ReadSave.actualEdit].editableData[(int)form.timeValue.value].weather = NewDay.Weather.rain;
                break;
        }
        if((int)form.timeValue.value == 0)
        {
            DB.saveData[ReadSave.actualEdit].startHour = int.Parse(form.editHour.text);
            DB.saveData[ReadSave.actualEdit].startMinute = int.Parse(form.editMinute.text);
        }
        Json.SaveData(DB);
        ChangeWeatherData();
        editWeatherTab.SetActive(false);
    }
    public void ExitEdit()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        editDayTab.SetActive(false);
        form.lake.GetComponent<Image>().color = color;
        form.place.GetComponent<Image>().color = color;
    }
    public void ExitWeatherEdit()
    {
        editWeatherTab.SetActive(false);
    }
    //add fish
    public void OpenTabAddFishInSaveDay()
    {
        addFish.SetActive(true);
        formFish.catchHour.text = "00";
        formFish.catchMinute.text = "00";
    }
    public void AddFish()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        //add
        if (formFish.fish.text.Length >= 2 && formFish.bait.text.Length >= 2 && formFish.catchHour.text.Length > 0 && formFish.catchMinute.text.Length > 0)
        {
            var data = new NewFish();
            data.fish = formFish.fish.text;
            if (formFish.length.text.Length > 0)
            {
                data.length = int.Parse(formFish.length.text);
            }
            if (formFish.weight.text.Length > 0)
            {
                data.weight = float.Parse(formFish.weight.text);
            }
            data.bait = formFish.bait.text;
            data.groundBait = formFish.groundBait.text;
            data.hour = int.Parse(formFish.catchHour.text);
            data.minute = int.Parse(formFish.catchMinute.text);
            DB.saveData[ReadSave.actualEdit].fishes.Add(data);
            Json.SaveData(DB);
            //ryby
            string prefab = "Brak Ryb";
            var fishCount = DB.saveData[ReadSave.actualEdit].fishes.Count;
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
            ActualDayEditor.mainSide.transform.Find("ID_" + ReadSave.actualEdit).Find("FishCount").GetComponent<TMP_Text>().text = prefab;
            //clear
            formFish.length.text = "";
            formFish.weight.text = "";
            formFish.fish.text = "";
            formFish.bait.text = "";
            formFish.groundBait.text = "";
            formFish.catchHour.text = "";
            formFish.catchMinute.text = "";
            formFish.fish.GetComponentInParent<Image>().color = color;
            formFish.bait.GetComponentInParent<Image>().color = color;
            formFish.catchHour.GetComponentInParent<Image>().color = color;
            formFish.catchMinute.GetComponentInParent<Image>().color = color;
            //exit
            for (int i = 0; i < fishesMainSide.transform.childCount; i++)
            {
                Destroy(fishesMainSide.transform.GetChild(i).gameObject);
            }
            ReadSave.OpenSaveFishesList();
            addFish.SetActive(false);
        }
        else
        {
            //check
            if (formFish.fish.text.Length < 2)
            {
                formFish.fish.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                formFish.fish.GetComponentInParent<Image>().color = color;
            }
            if (formFish.bait.text.Length < 2)
            {
                formFish.bait.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                formFish.bait.GetComponentInParent<Image>().color = color;
            }
            if(formFish.catchHour.text.Length < 1)
            {
                formFish.catchHour.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                formFish.catchHour.GetComponentInParent<Image>().color = color;
            }
            if (formFish.catchMinute.text.Length < 1)
            {
                formFish.catchMinute.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                formFish.catchMinute.GetComponentInParent<Image>().color = color;
            }
        }
    }
    public void ExitAddFish()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        addFish.SetActive(false);
        formFish.length.text = "";
        formFish.weight.text = "";
        formFish.fish.text = "";
        formFish.bait.text = "";
        formFish.groundBait.text = "";
        formFish.catchHour.text = "";
        formFish.catchMinute.text = "";
        formFish.fish.GetComponent<Image>().color = color;
        formFish.bait.GetComponent<Image>().color = color;
        formFish.catchHour.GetComponent<Image>().color = color;
        formFish.catchMinute.GetComponent<Image>().color = color;
    }
    public void ChangeTimeChatch()
    {
        if (formFish.catchHour.text.Length > 0 && formFish.catchMinute.text.Length > 0)
        {
            if (formFish.catchHour.text[0] != '-' && formFish.catchMinute.text[0] != '-')
            {
                if (int.Parse(formFish.catchHour.text) < 0 || int.Parse(formFish.catchMinute.text) < 0 || int.Parse(formFish.catchHour.text) > 23 || int.Parse(formFish.catchMinute.text) > 59)
                {
                    formFish.catchHour.text = "00";
                    formFish.catchMinute.text = "00";
                }
                else
                {
                    formFish.catchHour.text = formFish.catchHour.text.ToString().PadLeft(2, '0');
                    formFish.catchMinute.text = formFish.catchMinute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                formFish.catchHour.text = "00";
                formFish.catchMinute.text = "00";
            }
        }
        else
        {
            formFish.catchHour.text = "00";
            formFish.catchMinute.text = "00";
        }
    }
    public void ChangeLength()
    {
        if (formFish.length.text.Length > 0)
        {
            if (formFish.length.text[0] != '-')
            {
                if (int.Parse(formFish.length.text) <= 0)
                {
                    formFish.length.text = "";
                }
            }
            else
            {
                formFish.length.text = "";
            }
        }
        else
        {
            formFish.length.text = "";
        }
    }
    public void ChangeWeight()
    {
        formFish.weight.text = formFish.weight.text.Replace('.', ',');
        if(formFish.weight.text[0] == ',')
        {
            formFish.weight.text = "0" + formFish.weight.text;
        }
        if (formFish.weight.text.Length > 0)
        {
            if (formFish.weight.text[0] != '-')
            {
                if (float.Parse(formFish.weight.text) <= 0)
                {
                    formFish.weight.text = "";
                }
            }
            else
            {
                formFish.weight.text = "";
            }
        }
        else
        {
            formFish.weight.text = "";
        }
    }
    //edit fish
    public void OpenEditFish()
    {
        int id = ReadSave.actualEdit;
        EditFishForm.fish.text = DB.saveData[id].fishes[ReadSave.actualEditFish].fish;
        EditFishForm.length.text = DB.saveData[id].fishes[ReadSave.actualEditFish].length.ToString();
        EditFishForm.weight.text = DB.saveData[id].fishes[ReadSave.actualEditFish].weight.ToString();
        EditFishForm.bait.text = DB.saveData[id].fishes[ReadSave.actualEditFish].bait;
        EditFishForm.groundBait.text = DB.saveData[id].fishes[ReadSave.actualEditFish].groundBait;
        EditFishForm.catchHour.text = DB.saveData[id].fishes[ReadSave.actualEditFish].hour.ToString().PadLeft(2,'0');
        EditFishForm.catchMinute.text = DB.saveData[id].fishes[ReadSave.actualEditFish].minute.ToString().PadLeft(2, '0');
        editFish.SetActive(true);
    }
    public void EditFish()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        //add
        if (EditFishForm.fish.text.Length >= 2 && EditFishForm.bait.text.Length >= 2 && EditFishForm.catchHour.text.Length > 0 && EditFishForm.catchMinute.text.Length > 0)
        {
            var data = new NewFish();
            data.fish = EditFishForm.fish.text;
            if (EditFishForm.length.text.Length > 0)
            {
                data.length = int.Parse(EditFishForm.length.text);
            }
            if (EditFishForm.weight.text.Length > 0)
            {
                data.weight = float.Parse(EditFishForm.weight.text);
            }
            data.bait = EditFishForm.bait.text;
            data.groundBait = EditFishForm.groundBait.text;
            data.hour = int.Parse(EditFishForm.catchHour.text);
            data.minute = int.Parse(EditFishForm.catchMinute.text);
            DB.saveData[ReadSave.actualEdit].fishes[ReadSave.actualEditFish] = data;
            Json.SaveData(DB);
            //clear
            EditFishForm.length.text = "";
            EditFishForm.weight.text = "";
            EditFishForm.fish.text = "";
            EditFishForm.bait.text = "";
            EditFishForm.groundBait.text = "";
            EditFishForm.catchHour.text = "";
            EditFishForm.catchMinute.text = "";
            EditFishForm.fish.GetComponentInParent<Image>().color = color;
            EditFishForm.bait.GetComponentInParent<Image>().color = color;
            EditFishForm.catchHour.GetComponentInParent<Image>().color = color;
            EditFishForm.catchMinute.GetComponentInParent<Image>().color = color;
            //exit
            ReadSave.OpenFish(ReadSave.actualEditFish);
            ReadSave.OpenSaveFishesList();
            editFish.SetActive(false);
        }
        else
        {
            //check
            if (EditFishForm.fish.text.Length < 2)
            {
                EditFishForm.fish.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                EditFishForm.fish.GetComponentInParent<Image>().color = color;
            }
            if (EditFishForm.bait.text.Length < 2)
            {
                EditFishForm.bait.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                EditFishForm.bait.GetComponentInParent<Image>().color = color;
            }
            if (EditFishForm.catchHour.text.Length < 1)
            {
                EditFishForm.catchHour.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                EditFishForm.catchHour.GetComponentInParent<Image>().color = color;
            }
            if (EditFishForm.catchMinute.text.Length < 1)
            {
                EditFishForm.catchMinute.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                EditFishForm.catchMinute.GetComponentInParent<Image>().color = color;
            }
        }
    }
    public void ChangeTimeChatchInEdit()
    {
        if (EditFishForm.catchHour.text.Length > 0 && EditFishForm.catchMinute.text.Length > 0)
        {
            if (EditFishForm.catchHour.text[0] != '-' && EditFishForm.catchMinute.text[0] != '-')
            {
                if (int.Parse(EditFishForm.catchHour.text) < 0 || int.Parse(EditFishForm.catchMinute.text) < 0 || int.Parse(EditFishForm.catchHour.text) > 23 || int.Parse(EditFishForm.catchMinute.text) > 59)
                {
                    EditFishForm.catchHour.text = "00";
                    EditFishForm.catchMinute.text = "00";
                }
                else
                {
                    EditFishForm.catchHour.text = EditFishForm.catchHour.text.ToString().PadLeft(2, '0');
                    EditFishForm.catchMinute.text = EditFishForm.catchMinute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                EditFishForm.catchHour.text = "00";
                EditFishForm.catchMinute.text = "00";
            }
        }
        else
        {
            EditFishForm.catchHour.text = "00";
            EditFishForm.catchMinute.text = "00";
        }
    }
    public void ChangeLengthInEdit()
    {
        if (EditFishForm.length.text.Length > 0)
        {
            if (EditFishForm.length.text[0] != '-')
            {
                if (int.Parse(EditFishForm.length.text) <= 0)
                {
                    EditFishForm.length.text = "";
                }
            }
            else
            {
                EditFishForm.length.text = "";
            }
        }
        else
        {
            EditFishForm.length.text = "";
        }
    }
    public void ChangeWeightInEdit()
    {
        EditFishForm.weight.text = EditFishForm.weight.text.Replace('.', ',');
        if (EditFishForm.weight.text[0] == ',')
        {
            EditFishForm.weight.text = "0" + EditFishForm.weight.text;
        }
        if (EditFishForm.weight.text.Length > 0)
        {
            if (EditFishForm.weight.text[0] != '-')
            {
                if (float.Parse(EditFishForm.weight.text) <= 0)
                {
                    EditFishForm.weight.text = "";
                }
            }
            else
            {
                EditFishForm.weight.text = "";
            }
        }
        else
        {
            EditFishForm.weight.text = "";
        }
    }
    public void ExitEditFish()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        editFish.SetActive(false);
        EditFishForm.length.text = "";
        EditFishForm.weight.text = "";
        EditFishForm.fish.text = "";
        EditFishForm.bait.text = "";
        EditFishForm.groundBait.text = "";
        EditFishForm.catchHour.text = "";
        EditFishForm.catchMinute.text = "";
        EditFishForm.fish.GetComponent<Image>().color = color;
        EditFishForm.bait.GetComponent<Image>().color = color;
        EditFishForm.catchHour.GetComponent<Image>().color = color;
        EditFishForm.catchMinute.GetComponent<Image>().color = color;
    }
    //add weather save
    public void OpenAddNewWeatherSave()
    {
        addNewWeatherSave.SetActive(true);
        newWeatherSave.editHour.text = DB.saveData[ReadSave.actualEdit].editableData[DB.saveData[ReadSave.actualEdit].editableData.Count - 1].editHour.ToString().PadLeft(2,'0');
        newWeatherSave.editMinute.text = DB.saveData[ReadSave.actualEdit].editableData[DB.saveData[ReadSave.actualEdit].editableData.Count - 1].editMinute.ToString().PadLeft(2, '0');
    }
    public void ExitAddNewWeatherSave()
    {
        addNewWeatherSave.SetActive(false);
        newWeatherSave.temperature.text = "";
        newWeatherSave.windValue.text = "";
        newWeatherSave.windDirection.value = 0;
        newWeatherSave.weather.value = 0;
        newWeatherSave.editHour.text = "";
        newWeatherSave.editMinute.text = "";
    }
    public void SaveNewWeatherEdit()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (newWeatherSave.temperature.text.Length > 0 && newWeatherSave.windValue.text.Length > 0 && newWeatherSave.editHour.text.Length > 0 && newWeatherSave.editMinute.text.Length > 0)
        {
            var save = new EditableData();
            save.temperature = int.Parse(newWeatherSave.temperature.text);
            save.windValue = int.Parse(newWeatherSave.windValue.text);
            switch (newWeatherSave.windDirection.value)
            {
                case 0:
                    save.windDirection = NewDay.WindDirection.N;
                    break;
                case 1:
                save.windDirection = NewDay.WindDirection.E;
                    break;
                case 2:
                    save.windDirection = NewDay.WindDirection.S;
                    break;
                case 3:
                    save.windDirection = NewDay.WindDirection.W;
                    break;
                case 4:
                    save.windDirection = NewDay.WindDirection.NE;
                    break;
                case 5:
                    save.windDirection = NewDay.WindDirection.SE;
                    break;
                case 6:
                    save.windDirection = NewDay.WindDirection.SW;
                    break;
                case 7:
                    save.windDirection = NewDay.WindDirection.NW;
                    break;
            }
            switch (newWeatherSave.weather.value)
            {
                case 0:
                    save.weather = NewDay.Weather.sun;
                    break;
                case 1:
                    save.weather = NewDay.Weather.cloudy;
                    break;
                case 2:
                save.weather = NewDay.Weather.rain;
                break;
            }
            save.editHour = int.Parse(newWeatherSave.editHour.text);
            save.editMinute = int.Parse(newWeatherSave.editMinute.text);
            DB.saveData[ReadSave.actualEdit].editableData.Add(save);
            Json.SaveData(DB);
            ChangeWeatherData();
            addNewWeatherSave.SetActive(false);
        }
        else
        {
            //check
            if (newWeatherSave.temperature.text.Length == 0)
            {
                newWeatherSave.temperature.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                newWeatherSave.temperature.GetComponentInParent<Image>().color = color;
            }
            if (newWeatherSave.windValue.text.Length == 0)
            {
                newWeatherSave.windValue.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                newWeatherSave.windValue.GetComponentInParent<Image>().color = color;
            }
        }
    }
    public void ChangeTemperatureAddNew()
    {
        if (newWeatherSave.temperature.text == "-0" || newWeatherSave.temperature.text == "-00" || newWeatherSave.temperature.text == "-" || newWeatherSave.temperature.text == "")
        {
            newWeatherSave.temperature.text = "";
        }
        else if (int.Parse(newWeatherSave.temperature.text) > 50 || int.Parse(newWeatherSave.temperature.text) < -20)
        {
            newWeatherSave.temperature.text = "";
        }
        else
        {
            newWeatherSave.temperature.text = newWeatherSave.temperature.text;
        }
    }
    public void ChangeWindValueAddNew()
    {
        if (newWeatherSave.windValue.text == "-0" || newWeatherSave.windValue.text == "-00" || newWeatherSave.windValue.text == "-" || newWeatherSave.windValue.text == "")
        {
            newWeatherSave.windValue.text = "";
        }
        else if (int.Parse(newWeatherSave.windValue.text) > 99 || int.Parse(newWeatherSave.windValue.text) < 0)
        {
            newWeatherSave.windValue.text = "";
        }
        else
        {
            newWeatherSave.windValue.text = newWeatherSave.windValue.text;
        }
    }
    public void ChangeTimeAddNew()
    {
        if (newWeatherSave.editHour.text.Length > 0 && newWeatherSave.editMinute.text.Length > 0)
        {
            if (newWeatherSave.editHour.text[0] != '-' && newWeatherSave.editMinute.text[0] != '-')
            {
                if (int.Parse(newWeatherSave.editHour.text) < 0 || int.Parse(newWeatherSave.editMinute.text) < 0 || int.Parse(newWeatherSave.editHour.text) > 23 || int.Parse(newWeatherSave.editMinute.text) > 59)
                {
                    newWeatherSave.editHour.text = "00";
                    newWeatherSave.editMinute.text = "00";
                }
                else
                {
                    newWeatherSave.editHour.text = newWeatherSave.editHour.text.ToString().PadLeft(2, '0');
                    newWeatherSave.editMinute.text = newWeatherSave.editMinute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                newWeatherSave.editHour.text = "00";
                newWeatherSave.editMinute.text = "00";
            }
        }
        else
        {
            newWeatherSave.editHour.text = "00";
            newWeatherSave.editMinute.text = "00";
        }
    }
}
[System.Serializable]
public class EditSaveDayForm
{
    public TMP_InputField day;
    public TMP_InputField month;
    public TMP_InputField year;
    public TMP_InputField lake;
    public TMP_InputField place;
    public TMP_InputField lastRainDay;
    public TMP_InputField lastRainMonth;
    public TMP_InputField endHour;
    public TMP_InputField endMinute;
    //weather
    public TMP_Text weatherTimeValue;
    public Slider timeValue;
    public TMP_InputField temperature;
    public TMP_InputField windValue;
    public TMP_Dropdown windDirection;
    public TMP_Dropdown weather;
    public TMP_InputField editHour;
    public TMP_InputField editMinute;
    public TMP_Text time;
}
[System.Serializable]
public class AddFishForm
{
    public TMP_InputField fish;
    public TMP_InputField length;
    public TMP_InputField weight;
    public TMP_InputField bait;
    public TMP_InputField groundBait;
    public TMP_InputField catchHour;
    public TMP_InputField catchMinute;
}
[System.Serializable]
public class AddNewWeatherSave
{
    public TMP_InputField temperature;
    public TMP_InputField windValue;
    public TMP_Dropdown windDirection;
    public TMP_Dropdown weather;
    public TMP_InputField editHour;
    public TMP_InputField editMinute;
}
