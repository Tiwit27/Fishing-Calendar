using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataBase : MonoBehaviour
{
    [SerializeField] public List<NewDay> saveData = new List<NewDay>();
}

[System.Serializable]
public class NewDay
{
    [SerializeField] public int day;
    [SerializeField] public int month;
    [SerializeField] public int year;
    [SerializeField] public string lake;
    [SerializeField] public string place;
    [SerializeField] public int lastRainDay;
    [SerializeField] public int lastRainMonth;
    [SerializeField] public int startHour;
    [SerializeField] public int startMinute;
    [SerializeField] public int endHour;
    [SerializeField] public int endMinute;
    /*[SerializeField] public int endDay;
    [SerializeField] public int endMonth;
    [SerializeField] public int endYear;*/
    [SerializeField] public int savesId;
    [SerializeField] public List<NewFish> fishes = new List<NewFish>();
    [SerializeField] public List<EditableData> editableData = new List<EditableData>();

    public enum WindDirection
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
    public enum Weather
    {
        sun,
        cloudy,
        rain
    }

    public NewDay(int day, int month, int year, string lake, string place, int temperature, int windValue, int windDirectionINT, int lastRainDay, int lastRainMonth, int weatherINT, int startHour, int startMinute, int endHour, int endMinute)
    {
        var data = new EditableData();
        this.day = day;
        this.month = month;
        this.year = year;
        this.lake = lake.ToLower();
        this.place = place.ToLower();
        data.temperature = temperature;
        data.windValue = windValue;
        switch (windDirectionINT)
        {
            case 0:
                data.windDirection = WindDirection.N;
                break;
            case 1:
                data.windDirection = WindDirection.E;
                break;
            case 2:
                data.windDirection = WindDirection.S;
                break;
            case 3:
                data.windDirection = WindDirection.W;
                break;
            case 4:
                data.windDirection = WindDirection.NE;
                break;
            case 5:
                data.windDirection = WindDirection.SE;
                break;
            case 6:
                data.windDirection = WindDirection.SW;
                break;
            case 7:
                data.windDirection = WindDirection.NW;
                break;
        }
        this.lastRainDay = lastRainDay;
        this.lastRainMonth = lastRainMonth;
        switch (weatherINT)
        {
            case 0:
                data.weather = Weather.sun;
                break;
            case 1:
                data.weather = Weather.cloudy;
                break;
            case 2:
                data.weather = Weather.rain;
                break;
        }
        this.startHour = startHour;
        this.startMinute = startMinute;
        this.endHour = endHour;
        this.endMinute = endMinute;
        data.editHour = startHour;
        data.editMinute = startMinute;
        data.editTime = new System.TimeSpan(data.editHour, data.editMinute, 0);
        editableData.Add(data);
    }
}

[System.Serializable]
public class NewFish
{
    [SerializeField] public string fish;
    [SerializeField] public int length;
    [SerializeField] public float weight;
    [SerializeField] public string bait;
    [SerializeField] public string groundBait;
    [SerializeField] public int hour;
    [SerializeField] public int minute;

    public NewFish(string fish, int length, float weight, string bait, string groundBait, int hour, int minute)
    {
        this.fish = fish.ToLower();
        this.length = length;
        this.weight = weight;
        this.bait = bait.ToLower();
        this.groundBait = groundBait.ToLower();
        this.hour = hour;
        this.minute = minute;
    }
}
[System.Serializable]
public class EditableData
{
    [SerializeField] public int temperature;
    [SerializeField] public int windValue;
    [SerializeField] public NewDay.WindDirection windDirection;
    [SerializeField] public NewDay.Weather weather;
    [SerializeField] public System.TimeSpan editTime;
    [SerializeField] public int editHour;
    [SerializeField] public int editMinute;
}