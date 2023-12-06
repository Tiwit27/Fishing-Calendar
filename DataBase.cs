using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
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
    [SerializeField] public int temperature;
    [SerializeField] public int windValue;
    [SerializeField] public WindDirection windDirection;
    [SerializeField] public int lastRainDay;
    [SerializeField] public int lastRainMonth;
    [SerializeField] public Weather weather;
    [SerializeField] public int startHour;
    [SerializeField] public int startMinute;
    [SerializeField] public List<NewFish> fishes = new List<NewFish>();

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
        //drizzle,
        rain
    }

    public NewDay(int day, int month, int year, string lake, string place, int temperature, int windValue, int windDirectionINT, int lastRainDay, int lastRainMonth, int weatherINT, int startHour, int startMinute)
    {
        this.day = day;
        this.month = month;
        this.year = year;
        this.lake = lake;
        this.place = place;
        this.temperature = temperature;
        this.windValue = windValue;
        switch (windDirectionINT)
        {
            case 0:
                windDirection = WindDirection.N;
                break;
            case 1:
                windDirection = WindDirection.E;
                break;
            case 2:
                windDirection = WindDirection.S;
                break;
            case 3:
                windDirection = WindDirection.W;
                break;
            case 4:
                windDirection = WindDirection.NE;
                break;
            case 5:
                windDirection = WindDirection.SE;
                break;
            case 6:
                windDirection = WindDirection.SW;
                break;
            case 7:
                windDirection = WindDirection.NW;
                break;
        }
        this.lastRainDay = lastRainDay;
        this.lastRainMonth = lastRainMonth;
        switch (weatherINT)
        {
            case 0:
                weather = Weather.sun;
                break;
            case 1:
                weather = Weather.cloudy;
                break;
            case 2:
                weather = Weather.rain;
                break;
        }
        this.startHour = startHour;
        this.startMinute = startMinute;
    }
}

[System.Serializable]
public class NewFish
{
    [SerializeField] public string fish;
    [SerializeField] public int length;
    [SerializeField] public int weigth;
    [SerializeField] public string bait;
    [SerializeField] public int hour;
    [SerializeField] public int minute;
}