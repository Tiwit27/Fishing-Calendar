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
    public void OpenAddNewTab()
    {
        appBehaviour.addNew.SetActive(true);
        form.day.text = System.DateTime.Now.Day.ToString();
        form.month.text = System.DateTime.Now.Month.ToString();
        form.year.text = System.DateTime.Now.Year.ToString();
        form.lastRainDay.text = System.DateTime.Now.Day.ToString();
        form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
    }
    public void ChangeDate()
    {
        var month = int.Parse(form.month.text);
        var day = int.Parse(form.day.text);
        var year = int.Parse(form.year.text);
        //month
        if (month <= 0 || month > 12)
        {
            form.month.text = System.DateTime.Now.Month.ToString();
        }
        //day
        if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            if(day > 30 || day <= 0)
            {
                form.day.text = System.DateTime.Now.Day.ToString();
            }
        }
        else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            if (day > 31 || day <= 0)
            {
                form.day.text = System.DateTime.Now.Day.ToString();
            }
        }
        //year
        if(year < 2010 || year > int.Parse(System.DateTime.Now.Year.ToString()))
        {
            form.year.text = System.DateTime.Now.Year.ToString();
        }
    }
    public void ChangeTemperature()
    {
        if (form.temperature.text == "-0" || form.temperature.text == "-00" || form.temperature.text == "-")
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
    public void ChangeLastRain()
    {
        if (form.lastRainMonth.text != "" && form.lastRainDay.text != "")
        {
            var month = int.Parse(form.lastRainMonth.text);
            var day = int.Parse(form.lastRainDay.text);
            //month
            if (month <= 0 || month > 12)
            {
                form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
            }
            //day
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day > 30 || day <= 0)
                {
                    form.lastRainDay.text = System.DateTime.Now.Day.ToString();
                }
            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day > 31 || day <= 0)
                {
                    form.lastRainDay.text = System.DateTime.Now.Day.ToString();
                }
            }
        }
        else
        {
            form.lastRainDay.text = System.DateTime.Now.Day.ToString();
            form.lastRainMonth.text = System.DateTime.Now.Month.ToString();
        }
    }

    public void Add()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.lake.text.Length >= 2 && form.place.text.Length >= 5 && form.temperature.text.Length >= 1 && form.windValue.text.Length >= 1)
        {
            //clear
            
            form.lake.GetComponentInParent<Image>().color = color;
            form.place.GetComponentInParent<Image>().color = color;
            form.temperature.GetComponentInParent<Image>().color = color;
            form.windValue.GetComponentInParent<Image>().color = color;
            //wykonaj
            Debug.Log("Wywo³aj");
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
        }
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
