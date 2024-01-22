using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddBehaviour : MonoBehaviour
{
    [SerializeField] Form form;
<<<<<<< Updated upstream
    [SerializeField] List<NewAdd> Wyjazdy = new List<NewAdd>();
    [SerializeField] AppBehaviour appBehaviour;
=======
    [SerializeField] List<NewDay> Wyjazdy = new List<NewDay>();
    [SerializeField] public AppBehaviour appBehaviour;
    [SerializeField] public GameObject parent;
    [SerializeField] GameObject prefab;
    public int counter = 0;
    [SerializeField] JsonBehaviour json;
    [SerializeField] DataBase DB;
    [SerializeField] ActualDayEditor ActualDayEditor;
    [SerializeField] ScrollBehaviour ScrollBehaviour;
    [SerializeField] ReadSaveInGame readSave;
    [SerializeField] GameObject monthPrefab;
    static string[] monthName = {"Styczeñ", "Luty", "Marzec", "Kwiecieñ", "Maj", "Czerwiec", "Lipiec", "Sierpieñ", "Wrzesieñ", "PaŸdziernik", "Listopad", "Grudzieñ"};
    public List<MenuStructure> months = new List<MenuStructure>();
    [SerializeField] public NotificationsBehaviour notifications;
    public void Awake()
    {
        appBehaviour.addNew.SetActive(false);
    }
>>>>>>> Stashed changes
    public void OpenAddNewTab()
    {
        appBehaviour.addNew.SetActive(true);
        form.day.text = System.DateTime.Now.ToString("dd");
        form.month.text = System.DateTime.Now.ToString("MM");
        form.year.text = System.DateTime.Now.ToString("yyyy");
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
                form.day.text = System.DateTime.Now.ToString("dd");
            }
        }
        else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            if (day > 31 || day <= 0)
            {
                form.day.text = System.DateTime.Now.ToString("dd");
            }
        }
        //year
        if(year < 2010 || year > int.Parse(System.DateTime.Now.ToString("yyyy")))
        {
            form.year.text = System.DateTime.Now.ToString("yyyy");
        }
    }
    public void ChangeTemperature()
    {
<<<<<<< Updated upstream
        if(int.Parse(form.temperature.text) > 50 || int.Parse(form.temperature.text) < -20 || form.temperature.text == "-0" || form.temperature.text == "-00")
=======
        if(form.temperature.text.Length > 0)
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
>>>>>>> Stashed changes
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
                form.lastRainDay.text = System.DateTime.Now.ToString("dd");
            }
        }
        else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            if (day > 31 || day <= 0)
            {
                form.lastRainDay.text = System.DateTime.Now.ToString("dd");
            }
        }
    }

    public void Add()
    {
<<<<<<< Updated upstream
        //Instantiate()
=======
        var color = new Color(0.4117647f, 0.6039216f, 0.5607843f);
        if (form.lake.text.Length >= 2 && form.place.text.Length >= 5 && form.temperature.text.Length >= 1 && form.windValue.text.Length >= 1 && form.startHour.text.Length >= 1 && form.startMinute.text.Length >= 1)
        {
            //wykonaj
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }
            months.Clear();
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, parent.transform.childCount);
            //zapis do bazy danych wewnêtrznej
            DB.saveData.Add(new NewDay(int.Parse(form.day.text), int.Parse(form.month.text), int.Parse(form.year.text), form.lake.text.ToLower(), form.place.text, int.Parse(form.temperature.text), int.Parse(form.windValue.text), form.windDirection.value, int.Parse(form.lastRainDay.text), int.Parse(form.lastRainMonth.text), form.weather.value, int.Parse(form.startHour.text), int.Parse(form.startMinute.text), int.Parse(form.startHour.text), int.Parse(form.startMinute.text)));
            if (int.Parse(form.day.text) == System.DateTime.Now.Day && int.Parse(form.month.text) == System.DateTime.Now.Month && int.Parse(form.year.text) == System.DateTime.Now.Year)
            {
                ActualDayEditor.isTicketOpen = true;
                ActualDayEditor.OpenTicket(DB.saveData.Count - 1, DB.saveData[DB.saveData.Count - 1]);
                notifications.SendStartNotification();
            }
            //zapis do bazy zewnêtrznej
            json.SaveData(DB);
            AddOnAppOpen();
            if(!ActualDayEditor.isTicketOpen)
            {
                foreach (var month in months)
                {
                    foreach (var save in month.saves)
                    {
                        if(save.GetComponent<ScriptInPrefab>().id == DB.saveData.Count - 1)
                        {
                            readSave.OpenSaveDayTab(DB.saveData.Count - 1, save);
                            break;
                        }
                    }
                }
            }
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
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
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
        if (DB.saveData.Count > 0)
        {
            int id = 0;
            List<SavesList> sorter = new List<SavesList>();
            List<string> monthsList = new List<string>();
            //List<>
            foreach (var save in DB.saveData)
            {
                if(save.startHour != save.editableData[0].editTime.Hours || save.startMinute != save.editableData[0].editTime.Minutes)
                {
                    save.editableData[0].editTime = new System.TimeSpan(save.startHour, save.startMinute, 0);
                    save.editableData[0].editHour = save.startHour;
                    save.editableData[0].editMinute = save.startMinute;
                }
                foreach (var item in save.editableData)
                {
                    item.editTime = new System.TimeSpan(item.editHour, item.editMinute, 0);
                }
                var info = new SavesList();
                System.DateTime date = new System.DateTime(save.year, save.month, save.day, save.startHour, save.startMinute, 0);
                info.date = date;
                info.id = DB.saveData.IndexOf(save);
                sorter.Add(info);
            }
            json.SaveData(DB);
            sorter.Sort((x, y) => y.date.CompareTo(x.date));
            foreach (var item in sorter)
            {
                if(!monthsList.Contains(item.date.Month.ToString().PadLeft(2, '0') + "." + item.date.Year))
                {
                    var monthFishCount = 0;
                    var menuStructure = new MenuStructure();
                    var monthSave = Instantiate(monthPrefab);
                    monthSave.transform.parent = parent.transform;
                    monthSave.transform.Find("MonthObject").Find("Name").GetComponent<TMP_Text>().text = monthName[item.date.Month - 1] + " " + item.date.Year;
                    monthSave.name = "ID_" + id;
                    monthSave.GetComponent<ScriptInMonthPrefab>().addBehaviour = this.GetComponent<AddBehaviour>();
                    monthSave.GetComponent<ScriptInMonthPrefab>().id = id;
                    monthSave.GetComponent<ScriptInMonthPrefab>().month = item.date.Month;
                    monthSave.GetComponent<ScriptInMonthPrefab>().year = item.date.Year;
                    monthSave.transform.localScale = new Vector2(1, 1);
                    menuStructure.isActive = false;
                    menuStructure.month = monthSave;
                    monthsList.Add(item.date.Month.ToString().PadLeft(2, '0') + "." + item.date.Year);
                    parent.transform.Find("ID_" + id).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -id * 100 - 50);
                    foreach (var save in sorter)
                    {
                        if (item.date.Year == save.date.Year && item.date.Month == save.date.Month)
                        {
                            GameObject newTrip = Instantiate(prefab);
                            newTrip.transform.parent = monthSave.transform.Find("Saves");
                            newTrip.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-100 * monthSave.transform.Find("Saves").childCount));
                            newTrip.transform.Find("Name").GetComponent<TMP_Text>().text = DB.saveData[save.id].lake;
                            //ryby
                            string text = "Brak Ryb";
                            var fishCount = DB.saveData[save.id].fishes.Count;
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
                            newTrip.transform.Find("data").GetComponent<TMP_Text>().text = save.date.Day.ToString().PadLeft(2, '0') + "." + save.date.Month.ToString().PadLeft(2, '0') + "." + save.date.Year.ToString();
                            newTrip.transform.localScale = new Vector2(1, 1);
                            newTrip.GetComponent<ScriptInPrefab>().id = save.id;
                            newTrip.GetComponent<ScriptInPrefab>().savesId = counter;
                            DB.saveData[save.id].savesId = counter;
                            newTrip.GetComponent<ScriptInPrefab>().monthId = id;
                            newTrip.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
                            newTrip.GetComponent<ScriptInPrefab>().stats = this.GetComponent<StatsBehaviour>();
                            newTrip.GetComponent<ScriptInPrefab>().month = save.date.Month;
                            newTrip.GetComponent<ScriptInPrefab>().year = save.date.Year;
                            newTrip.name = "ID_" + save.id;
                            if (monthSave.transform.Find("Saves").childCount % 2 == 0)
                            {
                                newTrip.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                            }
                            menuStructure.saves.Add(newTrip);
                            counter++;
                            monthFishCount += fishCount;
                        }
                    }
                    monthSave.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -id * 100 - 50);
                    id++;
                    monthSave.transform.Find("Saves").gameObject.SetActive(false);
                    months.Add(menuStructure);
                    //ryby
                    monthSave.GetComponent<ScriptInMonthPrefab>().fishCount = monthFishCount;
                    monthSave.GetComponent<ScriptInMonthPrefab>().CountFishes();
                    //wyjazdy
                    monthSave.transform.Find("MonthObject").Find("SaveCount").GetComponent<TMP_Text>().text = menuStructure.saves.Count + " Wyjazdy";
                    monthSave.GetComponent<ScriptInMonthPrefab>().saveCount = menuStructure.saves.Count;
                    monthSave.GetComponent<ScriptInMonthPrefab>().CountSaves();
                }
            }
            /*for (int year = System.DateTime.Now.Year; year > 2009; year--)
            {
                for (int month = 12; month > 0; month--)
                {
                    var monthFishCount = 0;
                    var menuStructure = new MenuStructure();
                    var monthSave = Instantiate(monthPrefab);
                    monthSave.transform.parent = parent.transform;
                    monthSave.transform.Find("MonthObject").Find("Name").GetComponent<TMP_Text>().text = monthName[month - 1] + " " + year;
                    monthSave.name = "ID_" + id;
                    monthSave.GetComponent<ScriptInMonthPrefab>().addBehaviour = this.GetComponent<AddBehaviour>();
                    monthSave.GetComponent<ScriptInMonthPrefab>().id = id;
                    monthSave.transform.localScale = new Vector2(1, 1);
                    menuStructure.isActive = false;
                    menuStructure.month = monthSave;
                    for (int day = 32; day > 0; day--)
                    {
                        foreach (var save in DB.saveData)
                        {
                            if (save.year == year && save.month == month && save.day == day)
                            {   
                                GameObject newTrip = Instantiate(prefab);
                                newTrip.transform.parent = monthSave.transform.Find("Saves");
                                newTrip.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (-100 * monthSave.transform.Find("Saves").childCount));
                                newTrip.transform.Find("Name").GetComponent<TMP_Text>().text = save.lake;
                                //ryby
                                string text = "Brak Ryb";
                                var fishCount = save.fishes.Count;
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
                                newTrip.transform.Find("data").GetComponent<TMP_Text>().text = save.day.ToString().PadLeft(2, '0') + "." + save.month.ToString().PadLeft(2, '0') + "." + save.year.ToString();
                                newTrip.transform.localScale = new Vector2(1, 1);
                                newTrip.GetComponent<ScriptInPrefab>().id = DB.saveData.IndexOf(save);
                                newTrip.GetComponent<ScriptInPrefab>().savesId = counter;
                                DB.saveData[DB.saveData.IndexOf(save)].savesId = counter;
                                newTrip.GetComponent<ScriptInPrefab>().monthId = id;
                                newTrip.GetComponent<ScriptInPrefab>().readSave = this.GetComponent<ReadSaveInGame>();
                                newTrip.GetComponent<ScriptInPrefab>().stats = this.GetComponent<StatsBehaviour>();
                                newTrip.GetComponent<ScriptInPrefab>().month = month;
                                newTrip.GetComponent<ScriptInPrefab>().year = year;
                                newTrip.name = "ID_" + DB.saveData.IndexOf(save);
                                if (monthSave.transform.Find("Saves").childCount % 2 == 0)
                                {
                                    newTrip.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                                }
                                menuStructure.saves.Add(newTrip);
                                counter++;
                                monthFishCount += fishCount;
                            }
                        }
                    }
                    if (monthSave.transform.Find("Saves").childCount == 0)
                    {
                        Destroy(monthSave.gameObject);
                    }
                    else
                    {
                        monthSave.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -id * 100 - 50);
                        id++;
                        monthSave.transform.Find("Saves").gameObject.SetActive(false);
                        months.Add(menuStructure);
                        //ryby
                        monthSave.GetComponent<ScriptInMonthPrefab>().fishCount = monthFishCount;
                        monthSave.GetComponent<ScriptInMonthPrefab>().CountFishes();
                        //wyjazdy
                        monthSave.transform.Find("MonthObject").Find("SaveCount").GetComponent<TMP_Text>().text = menuStructure.saves.Count + " Wyjazdy";
                        monthSave.GetComponent<ScriptInMonthPrefab>().saveCount = menuStructure.saves.Count;
                        monthSave.GetComponent<ScriptInMonthPrefab>().CountSaves();
                    }
                }
            }*/
        }
>>>>>>> Stashed changes
    }
    public void OpenSaveMonth(int id)
    {
        if (parent.transform.Find("ID_" + id).Find("Saves").gameObject.activeSelf)
        {
            months[id].isActive = false;
            parent.transform.Find("ID_" + id).Find("Saves").gameObject.SetActive(false);
            if (id + 1 < parent.transform.childCount)
            {
                for (int i = id + 1; i < parent.transform.childCount; i++)
                {
                    parent.transform.Find("ID_" + i).GetComponent<RectTransform>().anchoredPosition += new Vector2(0, parent.transform.Find("ID_" + id).Find("Saves").childCount * 100);
                }
            }
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, CheckMainSideHeight());
            ScrollBehaviour.CheckScroll();
        }
        else
        {
            months[id].isActive = true;
            parent.transform.Find("ID_" + id).Find("Saves").gameObject.SetActive(true);
            for (int i = id + 1; i < parent.transform.childCount; i++)
            {
                //mimo dzia³ania nie przypisuje wartoœci
                parent.transform.Find("ID_" + i).GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, parent.transform.Find("ID_" + id).Find("Saves").childCount * 100);
            }
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, CheckMainSideHeight());
            ScrollBehaviour.CheckScroll();
        }
    }
    public void ReloadAfterDeleteSave(int id)
    {
        months[id].isActive = true;
        months[id].month.transform.Find("Saves").gameObject.SetActive(true);
        for (int i = id + 1; i < parent.transform.childCount; i++)
        {
            parent.transform.Find("ID_" + i).GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, parent.transform.Find("ID_" + id).Find("Saves").childCount * 100);
        }
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, CheckMainSideHeight());
        ScrollBehaviour.CheckScroll();
    }
    public int CheckMainSideHeight()
    {
        var actualOpen = 0;
        foreach (var month in months)
        {
            if (month.month.transform.Find("Saves").gameObject.activeSelf)
            {
                actualOpen += month.saves.Count;
            }
            actualOpen++;
        }
        return actualOpen * 100;
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
[System.Serializable]
public class MenuStructure
{
    public GameObject month;
    public List<GameObject> saves = new List<GameObject>();
    public bool isActive;
}
/*class MonthList
{
    publi
}*/
[System.Serializable]
public class SavesList
{
    public int id;
    public System.DateTime date;
}
