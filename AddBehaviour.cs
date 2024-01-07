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
    [SerializeField] public GameObject parent;
    [SerializeField] GameObject prefab;
    public int counter = 0;
    [SerializeField] JsonBehaviour json;
    [SerializeField] DataBase DB;
    [SerializeField] ActualDayEditor ActualDayEditor;
    [SerializeField] ScrollBehaviour ScrollBehaviour;
    [SerializeField] ReadSaveInGame readSave;
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
        ScrollBehaviour.SetStartPosition();
    }
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
                        form.month.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
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
                    }
                }
                else
                {
                    form.day.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                }
                //year
                if (year < 2010 || year > int.Parse(System.DateTime.Now.ToString("yyyy")))
                {
                    form.year.text = System.DateTime.Now.ToString("yyyy");
                }
            }
            else
            {
                form.day.text = System.DateTime.Now.ToString("dd");
                form.month.text = System.DateTime.Now.ToString("MM");
                form.year.text = System.DateTime.Now.ToString("yyyy");
            }
        }
        else
        {
            form.day.text = System.DateTime.Now.ToString("dd");
            form.month.text = System.DateTime.Now.ToString("MM");
            form.year.text = System.DateTime.Now.ToString("yyyy");
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
                        else if (day > 28)
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
                }
            }
            else
            {
                form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
                form.lastRainMonth.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
            }
        }
        else
        {
            form.lastRainDay.text = System.DateTime.Now.ToString("dd").PadLeft(2, '0');
            form.lastRainMonth.text = System.DateTime.Now.ToString("MM").PadLeft(2, '0');
        }
    }
    public void ChangeStartHour()
    {
        if (form.startHour.text.Length > 0 && form.startMinute.text.Length > 0)
        {
            if (form.startHour.text[0] != '-' && form.startMinute.text[0] != '-')
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
                if (minute < 0 || minute > 59)
                {
                    form.startMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
                }
                else
                {
                    form.startMinute.text = form.startMinute.text.ToString().PadLeft(2, '0');
                }
            }
            else
            {
                form.startHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
                form.startMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
            }
        }
        else
        {
            form.startHour.text = System.DateTime.Now.ToString("HH").PadLeft(2, '0');
            form.startMinute.text = System.DateTime.Now.ToString("mm").PadLeft(2, '0');
        }
    }

    public void Add()
    {
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.lake.text.Length >= 2 && form.place.text.Length >= 5 && form.temperature.text.Length >= 1 && form.windValue.text.Length >= 1 && form.startHour.text.Length >= 1 && form.startMinute.text.Length >= 1)
        {
            //wykonaj
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }
            /*GameObject newTrip = Instantiate(prefab);
            newTrip.transform.parent = parent.transform;
            newTrip.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-100 * counter) - 50);
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
            counter++;*/
            //zapis do bazy danych wewnêtrznej
            DB.saveData.Add(new NewDay(int.Parse(form.day.text), int.Parse(form.month.text), int.Parse(form.year.text), form.lake.text, form.place.text, int.Parse(form.temperature.text), int.Parse(form.windValue.text), form.windDirection.value, int.Parse(form.lastRainDay.text), int.Parse(form.lastRainMonth.text), form.weather.value, int.Parse(form.startHour.text), int.Parse(form.startMinute.text)));
            if (int.Parse(form.day.text) == System.DateTime.Now.Day && int.Parse(form.month.text) == System.DateTime.Now.Month && int.Parse(form.year.text) == System.DateTime.Now.Year)
            {
                ActualDayEditor.isTicketOpen = true;
                ActualDayEditor.OpenTicket(DB.saveData.Count - 1, DB.saveData[DB.saveData.Count - 1]);
            }
            else
            {
                readSave.OpenSaveDayTab(DB.saveData.Count - 1);
            }
            //zapis do bazy zewnêtrznej
            json.SaveData(DB);
            AddOnAppOpen();
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
            ScrollBehaviour.CheckScroll();
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
    public void Exit()
    {
        //clear
        form.lake.text = "";
        form.place.text = "";
        form.temperature.text = "";
        form.windValue.text = "";
        form.windDirection.value = 0;
        form.weather.value = 0;
        appBehaviour.addNew.SetActive(false);
        ScrollBehaviour.SetStartPosition();
    }
    public void AddOnAppOpen()
    {
        if (DB.saveData.Count > 0)
        {
            for (int i = DB.saveData.Count - 1; i >= 0; i--)
            {
                var NewDay = DB.saveData[i];
                GameObject newTrip = Instantiate(prefab);
                newTrip.transform.parent = parent.transform;
                newTrip.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-100 * counter) - 50);
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
                newTrip.GetComponent<ScriptInPrefab>().id = i;
                newTrip.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
                newTrip.name = "ID_" + i;
                if (counter % 2 != 0)
                {
                    newTrip.transform.Find("background").GetComponent<RawImage>().color = new Color(0.4018334f, 0.6603774f, 0.6003582f);
                }
                counter++;
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
