using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddBehaviour : MonoBehaviour
{
    [SerializeField] Form form;
    [SerializeField] List<NewDay> Wyjazdy = new List<NewDay>();
    [SerializeField] AppBehaviour appBehaviour;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject prefab;
    int counter = 0;
    [SerializeField] JsonBehaviour json;
    [SerializeField] DataBase DB;
    [SerializeField] ActualDayEditor ActualDayEditor;
    public void OpenAddNewTab()
    {
        appBehaviour.addNew.SetActive(true);
        form.day.text = System.DateTime.Now.Day.ToString();
        form.month.text = System.DateTime.Now.Month.ToString();
        form.year.text = System.DateTime.Now.Year.ToString();
        form.lastRainDay.text = System.DateTime.Now.Day.ToString();
        form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
        form.startHour.text = System.DateTime.Now.Hour.ToString();
        form.startMinute.text = System.DateTime.Now.Minute.ToString();
    }
    public void ChangeDate()
    {
        var month = int.Parse(form.month.text);
        var day = int.Parse(form.day.text);
        var year = int.Parse(form.year.text);
        if (day > 0)
        {
            //month
            if (month <= 0 || month > 12)
            {
                form.month.text = System.DateTime.Now.Month.ToString();
            }
            //day
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day > 30)
                {
                    form.day.text = System.DateTime.Now.Day.ToString();
                }
            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day > 31)
                {
                    form.day.text = System.DateTime.Now.Day.ToString();
                }
            }
            else if (month == 2)
            {
                if (System.DateTime.IsLeapYear(int.Parse(form.year.text)) && day > 29)
                {
                    form.day.text = System.DateTime.Now.Month.ToString();
                }
                else if (!System.DateTime.IsLeapYear(int.Parse(form.year.text)) && day > 28)
                {
                    form.day.text = System.DateTime.Now.Month.ToString();
                }
            }
        }
        else
        {
            form.lastRainDay.text = System.DateTime.Now.Month.ToString();
        }
        //year
        if (year < 2010 || year > int.Parse(System.DateTime.Now.Year.ToString()))
        {
            form.year.text = System.DateTime.Now.Year.ToString();
        }
    }
    public void ChangeTemperature()
    {
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
    public void ChangeLastRain()
    {
        if (form.lastRainMonth.text != "" && form.lastRainDay.text != "")
        {
            var month = int.Parse(form.lastRainMonth.text);
            var day = int.Parse(form.lastRainDay.text);
            if (day >= 0)
            {
                //month
                if (month <= 0 || month > 12)
                {
                    form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
                }
                //day
                if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    if (day > 30)
                    {
                        form.lastRainDay.text = System.DateTime.Now.Day.ToString();
                    }
                }
                else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                {
                    if (day > 31)
                    {
                        form.lastRainDay.text = System.DateTime.Now.Day.ToString();
                    }
                }
                else if (month == 2)
                {
                    if (System.DateTime.IsLeapYear(int.Parse(form.year.text)) && day > 29)
                    {
                        form.lastRainDay.text = System.DateTime.Now.Month.ToString();
                    }
                    else if(day > 28)
                    {
                        form.lastRainDay.text = System.DateTime.Now.Month.ToString();
                    }
                }
            }
            else
            {
                form.lastRainDay.text = System.DateTime.Now.Month.ToString();
            }
        }
        else
        {
            form.lastRainDay.text = System.DateTime.Now.Day.ToString();
            form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
        }
    }
    public void ChangeStartHour()
    {
        int hour = int.Parse(form.startHour.text);
        int minute = int.Parse(form.startMinute.text);
        if (hour < 0 || hour > 23)
        {
            form.startHour.text = System.DateTime.Now.Hour.ToString();
        }
        if(minute < 0 || minute > 59)
        {
            form.startMinute.text = System.DateTime.Now.Minute.ToString();
        }
    }

    public void Add()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.lake.text.Length >= 2 && form.place.text.Length >= 5 && form.temperature.text.Length >= 1 && form.windValue.text.Length >= 1 && form.startHour.text.Length >= 1 && form.startMinute.text.Length >= 1)
        {
            //wykonaj
            GameObject newTrip = Instantiate(prefab);
            newTrip.transform.parent = parent.transform;
            newTrip.transform.position = new Vector2(parent.transform.position.x, parent.transform.position.y - (100 * counter));
            newTrip.transform.Find("Name").GetComponent<TMP_Text>().text = form.lake.text;
            newTrip.transform.Find("data").GetComponent<TMP_Text>().text = $"{form.day.text}.{form.month.text}.{form.year.text}";
            newTrip.transform.Find("Weather").GetComponent<TMP_Text>().text = form.weather.GetComponentInChildren<TMP_Text>().text;
            counter++;
            //zapis do bazy danych wewnêtrznej
            DB.saveData.Add(new NewDay(int.Parse(form.day.text), int.Parse(form.month.text), int.Parse(form.year.text), form.lake.text, form.place.text, int.Parse(form.temperature.text), int.Parse(form.windValue.text), form.windDirection.value, int.Parse(form.lastRainDay.text), int.Parse(form.lastRainMonth.text), form.weather.value, int.Parse(form.startHour.text), int.Parse(form.startMinute.text)));
            if (int.Parse(form.day.text) == System.DateTime.Now.Day && int.Parse(form.month.text) == System.DateTime.Now.Month && int.Parse(form.year.text) == System.DateTime.Now.Year)
            {
                ActualDayEditor.isTicketOpen = true;
                ActualDayEditor.OpenTicket(DB.saveData.Count - 1, DB.saveData[DB.saveData.Count - 1]);
            }
            json.SaveData(DB);
            //clear
            form.lake.GetComponentInParent<Image>().color = color;
            form.place.GetComponentInParent<Image>().color = color;
            form.temperature.GetComponentInParent<Image>().color = color;
            form.windValue.GetComponentInParent<Image>().color = color;
            form.day.text = System.DateTime.Now.Day.ToString();
            form.month.text = System.DateTime.Now.Month.ToString();
            form.year.text = System.DateTime.Now.Year.ToString();
            form.lake.text = "";
            form.place.text = "";
            form.temperature.text = "";
            form.windValue.text = "";
            form.windDirection.value = 0;
            form.lastRainDay.text = System.DateTime.Now.Day.ToString();
            form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
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
    public TMP_Dropdown weather;
    public TMP_InputField startHour;
    public TMP_InputField startMinute;
}
