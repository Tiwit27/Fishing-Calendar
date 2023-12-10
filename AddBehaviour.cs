using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddBehaviour : MonoBehaviour
{
    [SerializeField] Form form;
    [SerializeField] List<NewAdd> Wyjazdy = new List<NewAdd>();
    [SerializeField] AppBehaviour appBehaviour;
<<<<<<< Updated upstream
    public void OpenAddNewTab()
    {
        appBehaviour.addNew.SetActive(true);
        form.day.text = System.DateTime.Now.ToString("dd");
        form.month.text = System.DateTime.Now.ToString("MM");
        form.year.text = System.DateTime.Now.ToString("yyyy");
=======
    [SerializeField] GameObject parent;
    [SerializeField] GameObject prefab;
    int counter = 0;
    [SerializeField] JsonBehaviour json;
    [SerializeField] DataBase DB;
    [SerializeField] ActualDayEditor ActualDayEditor;
    public void Awake()
    {
        appBehaviour.addNew.SetActive(false);
    }
    public void OpenAddNewTab()
    {
        appBehaviour.addNew.SetActive(true);
        form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
        form.month.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
        form.year.text = System.DateTime.Now.ToString("yyyy");
        form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
        form.lastRainMonth.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
        form.startHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
        form.startMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
>>>>>>> Stashed changes
    }
    public void ChangeDate()
    {
        var month = int.Parse(form.month.text);
        var day = int.Parse(form.day.text);
        var year = int.Parse(form.year.text);
        //month
        if (month <= 0 || month > 12)
        {
            form.month.text = System.DateTime.Now.ToString("MM");
        }
        //day
        if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            if(day > 30 || day <= 0)
            {
<<<<<<< Updated upstream
                form.day.text = System.DateTime.Now.ToString("dd");
=======
                form.month.text = System.DateTime.Now.ToString("MM").PadLeft(2,'0');
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
                    form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
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
                    form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                }
                else
                {
                    form.day.text = form.day.text.ToString().PadLeft(2, '0');
                }
            }
            else if (month == 2)
            {
                if (System.DateTime.IsLeapYear(int.Parse(form.year.text)) && day > 29)
                {
                    form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                }
                else if (!System.DateTime.IsLeapYear(int.Parse(form.year.text)) && day > 28)
                {
                    form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                }
                else
                {
                    form.day.text = form.day.text.ToString().PadLeft(2, '0');
                }
>>>>>>> Stashed changes
            }
        }
        else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
<<<<<<< Updated upstream
            if (day > 31 || day <= 0)
            {
                form.day.text = System.DateTime.Now.ToString("dd");
            }
        }
        //year
        if(year < 2010 || year > int.Parse(System.DateTime.Now.ToString("yyyy")))
=======
            form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
        }
        //year
        if (year < 2010 || year > int.Parse(System.DateTime.Now.ToString("yyyy")))
>>>>>>> Stashed changes
        {
            form.year.text = System.DateTime.Now.ToString("yyyy");
        }
    }
    public void ChangeTemperature()
    {
        if(int.Parse(form.temperature.text) > 50 || int.Parse(form.temperature.text) < -20 || form.temperature.text == "-0" || form.temperature.text == "-00")
        {
            form.temperature.text = "";
        }
    }
    public void ChangeLastRain()
    {
        var month = int.Parse(form.lastRainMonth.text);
        var day = int.Parse(form.lastRainDay.text);
        //month
        if (month <= 0 || month > 12)
        {
            form.lastRainMonth.text = System.DateTime.Now.ToString("MM");
        }
        //day
        if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            if (day > 30 || day <= 0)
            {
<<<<<<< Updated upstream
                form.lastRainDay.text = System.DateTime.Now.ToString("dd");
=======
                //month
                if (month <= 0 || month > 12)
                {
                    form.lastRainMonth.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
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
                        form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
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
                        form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
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
                        form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                    }
                    else if(day > 28)
                    {
                        form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                    }
                    else
                    {
                        form.lastRainDay.text = form.lastRainDay.text.ToString().PadLeft(2, '0');
                    }
                }
            }
            else
            {
                form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
>>>>>>> Stashed changes
            }
        }
        else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
<<<<<<< Updated upstream
            if (day > 31 || day <= 0)
            {
                form.lastRainDay.text = System.DateTime.Now.ToString("dd");
            }
=======
            form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
            form.lastRainMonth.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
        }
    }
    public void ChangeStartHour()
    {
        int hour = int.Parse(form.startHour.text);
        int minute = int.Parse(form.startMinute.text);
        if (hour < 0 || hour > 23)
        {
            form.startHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
        }
        else
        {
            form.startHour.text = form.startHour.text.ToString().PadLeft(2, '0');
        }
        if(minute < 0 || minute > 59)
        {
            form.startMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
        }
        else
        {
            form.startMinute.text = form.startMinute.text.ToString().PadLeft(2, '0');
>>>>>>> Stashed changes
        }
    }

    public void Add()
    {
<<<<<<< Updated upstream
        //Instantiate()
    }
}
[System.Serializable]
class NewAdd
{
    [SerializeField] int day;
    [SerializeField] int month;
    [SerializeField] int year;
    [SerializeField] string lake;
    [SerializeField] string place;
    [SerializeField] int temperature;
    [SerializeField] int windValue;
    [SerializeField] WindDirection windDirection;
    [SerializeField] int lastRainDay;
    [SerializeField] int lastRainMonth;
    [SerializeField] Weather weather;
    
    enum WindDirection
    {
        N,
        E,
        S,
        W,
        NE,
        SE,
        SW,
        NW
    }
    enum Weather
    {
        sun,
        cloudy,
        drizzle,
        rain
    }

    NewAdd(int day,int month,int year, string lake,string place,int temperature,int windValue,WindDirection windDirection,int lastRainDay, int lastRainMonth, Weather weather)
    {
        this.day = day;
        this.month = month;
        this.year = year;
        this.lake = lake;
        this.place = place;
        this.temperature = temperature;
        this.windValue = windValue;
        this.windDirection = windDirection;
        this.lastRainDay = lastRainDay;
        this.weather = weather;
=======
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.lake.text.Length >= 2 && form.place.text.Length >= 5 && form.temperature.text.Length >= 1 && form.windValue.text.Length >= 1 && form.startHour.text.Length >= 1 && form.startMinute.text.Length >= 1)
        {
            //wykonaj
            GameObject newTrip = Instantiate(prefab);
            newTrip.transform.parent = parent.transform;
            newTrip.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter));
            newTrip.transform.Find("Name").GetComponent<TMP_Text>().text = form.lake.text;
            newTrip.transform.Find("data").GetComponent<TMP_Text>().text = $"{form.day.text}.{form.month.text}.{form.year.text}";
            newTrip.transform.Find("FishCount").GetComponent<TMP_Text>().text = "Brak Ryb";
            newTrip.transform.localScale = new Vector2(1, 1);
            newTrip.GetComponent<ScriptInPrefab>().id = counter;
            newTrip.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
            newTrip.name = "ID_" + counter;
            if (counter%2 != 0)
            {
                newTrip.transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
            }
            counter++;
            //zapis do bazy danych wewnêtrznej
            DB.saveData.Add(new NewDay(int.Parse(form.day.text), int.Parse(form.month.text), int.Parse(form.year.text), form.lake.text, form.place.text, int.Parse(form.temperature.text), int.Parse(form.windValue.text), form.windDirection.value, int.Parse(form.lastRainDay.text), int.Parse(form.lastRainMonth.text), form.weather.value, int.Parse(form.startHour.text), int.Parse(form.startMinute.text)));
            if (int.Parse(form.day.text) == System.DateTime.Now.Day && int.Parse(form.month.text) == System.DateTime.Now.Month && int.Parse(form.year.text) == System.DateTime.Now.Year)
            {
                ActualDayEditor.isTicketOpen = true;
                ActualDayEditor.OpenTicket(DB.saveData.Count - 1, DB.saveData[DB.saveData.Count - 1]);
            }
            //zapis do bazy zewnêtrznej
            json.SaveData(DB);
            //clear
            form.lake.GetComponentInParent<Image>().color = color;
            form.place.GetComponentInParent<Image>().color = color;
            form.temperature.GetComponentInParent<Image>().color = color;
            form.windValue.GetComponentInParent<Image>().color = color;
            form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
            form.month.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
            form.year.text = System.DateTime.Now.ToString("yyyy");
            form.lake.text = "";
            form.place.text = "";
            form.temperature.text = "";
            form.windValue.text = "";
            form.windDirection.value = 0;
            form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
            form.lastRainMonth.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
            form.weather.value = 0;
            appBehaviour.addNew.SetActive(false);
        }
        else
        {
            if (form.lake.text.Length < 2)
            {
                form.lake.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.lake.GetComponentInParent<Image>().color = color;
            }
            if (form.place.text.Length < 5)
            {
                form.place.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.place.GetComponentInParent<Image>().color = color;
            }
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
            if (form.startHour.text.Length < 1)
            {
                form.startHour.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.startHour.GetComponentInParent<Image>().color = color;
            }
            if (form.startMinute.text.Length < 1)
            {
                form.startMinute.GetComponentInParent<Image>().color = new Color(0.6039216f, 0.5495574f, 0.4117647f);
            }
            else
            {
                form.startMinute.GetComponentInParent<Image>().color = color;
            }
        }
>>>>>>> Stashed changes
    }
    public void AddOnAppOpen(NewDay NewDay)
    {
        GameObject newTrip = Instantiate(prefab);
        newTrip.transform.parent = parent.transform;
        newTrip.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter));
        newTrip.transform.Find("Name").GetComponent<TMP_Text>().text = NewDay.lake;
        //ryby
        string text = "Brak Ryb";
        var fishCount = NewDay.fishes.Count;
        if (fishCount == 0)
        {
            text = "Brak Ryb";
        }
        else if (fishCount == 1)
        {
            text = fishCount + " Ryba";
        }
        else if (fishCount > 1 && fishCount < 5)
        {
            text = fishCount + " Ryby";
        }
        else if (fishCount > 4 && fishCount % 10 != 2 && fishCount % 10 != 3 && fishCount % 10 != 4)
        {
            text = fishCount + " Ryb";
        }
        else if (fishCount > 4 && (fishCount % 10 == 2 || fishCount % 10 == 3 || fishCount % 10 == 4))
        {
            text = fishCount + " Ryby";
        }
        newTrip.transform.Find("FishCount").GetComponent<TMP_Text>().text = text;
        newTrip.transform.Find("data").GetComponent<TMP_Text>().text = NewDay.day.ToString().PadLeft(2, '0') + "." + NewDay.month.ToString().PadLeft(2, '0') + "." + NewDay.year.ToString();
        newTrip.transform.localScale = new Vector2(1, 1);
        newTrip.GetComponent<ScriptInPrefab>().id = counter;
        newTrip.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
        newTrip.name = "ID_" + counter;
        if (counter % 2 != 0)
        {
            newTrip.transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
        }
        counter++;
    }
}
[System.Serializable]
public class Form
{
    public TMP_InputField day;
    public TMP_InputField month;
    public TMP_InputField year;
    public TMP_InputField lake;
    public TMP_InputField place;
    public TMP_InputField temperature;
    public TMP_InputField windValue;
    public TMP_Dropdown windDirection;
    public TMP_InputField lastRainDay;
    public TMP_InputField lastRainMonth;
    public TMP_InputField weather;
}
