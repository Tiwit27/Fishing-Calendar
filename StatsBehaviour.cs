using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class StatsBehaviour : Stats
{
    [SerializeField] GameObject statsIcon;
    [SerializeField] GameObject stats;
    [SerializeField] DataBase DB;
    [SerializeField] AddBehaviour addBehaviour;
    //moreStats
    [SerializeField] GameObject moreStats;
    [SerializeField] TMP_Text topInfoText;
    [SerializeField] GameObject prefab;
    private void Start()
    {
        stats.SetActive(false);
        moreStats.SetActive(false);
    }
    public void OpenStats(int id)
    {
        time.transform.parent.GetComponent<Button>().interactable = false;
        lakes.transform.parent.GetComponent<Button>().interactable = false;
        //obliczenia
        //time
        var startTime = new System.TimeSpan(DB.saveData[id].startHour, DB.saveData[id].startMinute, 0);
        var endTime = new System.TimeSpan(DB.saveData[id].endHour, DB.saveData[id].endMinute, 0);
        var fullTime = endTime - startTime;
        //baits
        List<string> baitsList = new List<string>();
        foreach (var item in DB.saveData[id].fishes)
        {
            if(!baitsList.Contains(item.bait.ToLower()))
            {
                baitsList.Add(item.bait.ToLower());
            }
        }
        if (DB.saveData[id].fishes.Count == 0)
        {
            fishes.transform.parent.GetComponent<Button>().interactable = false;
        }
        else
        {
            fishes.transform.parent.GetComponent<Button>().interactable = true;
        }
        if (baitsList.Count == 0)
        {
            baits.transform.parent.GetComponent<Button>().interactable = false;
        }
        else
        {
            baits.transform.parent.GetComponent<Button>().interactable = true;
        }
        //koniec obliczen
        stats.SetActive(true);
        startDay.text = DB.saveData[id].day.ToString().PadLeft(2,'0');
        startMonth.text = DB.saveData[id].month.ToString().PadLeft(2, '0');
        startYear.text = DB.saveData[id].year.ToString().PadLeft(4, '0');
        endDay.text = DB.saveData[id].day.ToString().PadLeft(2, '0');
        endMonth.text = DB.saveData[id].month.ToString().PadLeft(2, '0');
        endYear.text = DB.saveData[id].year.ToString().PadLeft(4, '0');
        lakeName.text = DB.saveData[id].lake;
        fishes.text = "Iloœæ Z³owionych Ryb: " + DB.saveData[id].fishes.Count;
        time.text = "Czas Spêdzony nad Wod¹: " + fullTime.Hours + ":" + fullTime.Minutes;
        baits.text = "Iloœæ u¿ytych przynêt: " + baitsList.Count;
        saves.text = "Iloœæ wyjazdów: 1";
        lakes.text = "Iloœc odwiedzonych ³owisk: 1"; 
    }

    public void OpenStats(System.DateTime startDate, System.DateTime endDate)
    {
        int fishesCount = 0;
        System.TimeSpan deltaTime;
        List<string> baitsList = new List<string>();
        int savesCount = 0;
        List<string> lakesList = new List<string>();
        if (lakeName.text.Length > 0)
        {
            foreach (var item in DB.saveData)
            {
                if(item.lake == lakeName.text)
                {
                    var itemData = new System.DateTime(item.year, item.month, item.day);
                    if (itemData >= startDate && itemData <= endDate)
                    {
                        fishesCount += item.fishes.Count;
                        var startTime = new System.TimeSpan(item.startHour, item.startMinute, 0);
                        var endTime = new System.TimeSpan(item.endHour, item.endMinute, 0);
                        deltaTime += endTime - startTime;
                        foreach (var fish in item.fishes)
                        {
                            if (!baitsList.Contains(fish.bait))
                            {
                                baitsList.Add(fish.bait);
                            }
                        }
                        savesCount++;
                        if (!lakesList.Contains(item.lake))
                        {
                            lakesList.Add(item.lake);
                        }
                    }
                }
            }
        }
        else
        {
            time.transform.parent.GetComponent<Button>().interactable = true;
            lakes.transform.parent.GetComponent<Button>().interactable = true;
            foreach (var item in DB.saveData)
            {
                var itemData = new System.DateTime(item.year, item.month, item.day);
                if (itemData >= startDate && itemData <= endDate)
                {
                    fishesCount += item.fishes.Count;
                    var startTime = new System.TimeSpan(item.startHour, item.startMinute, 0);
                    var endTime = new System.TimeSpan(item.endHour, item.endMinute, 0);
                    deltaTime += endTime - startTime;
                    foreach (var fish in item.fishes)
                    {
                        if (!baitsList.Contains(fish.bait))
                        {
                            baitsList.Add(fish.bait);
                        }
                    }
                    savesCount++;
                    if (!lakesList.Contains(item.lake))
                    {
                        lakesList.Add(item.lake);
                    }
                }
            }
            //koniec obliczen
        }
        if (fishesCount == 0)
        {
            fishes.transform.parent.GetComponent<Button>().interactable = false;
        }
        else
        {
            fishes.transform.parent.GetComponent<Button>().interactable = true;
        }
        if (baitsList.Count == 0)
        {
            baits.transform.parent.GetComponent<Button>().interactable = false;
        }
        else
        {
            baits.transform.parent.GetComponent<Button>().interactable = true;
        }
        stats.SetActive(true);
        startDay.text = startDate.Day.ToString().PadLeft(2, '0');
        startMonth.text = startDate.Month.ToString().PadLeft(2, '0');
        startYear.text = startDate.Year.ToString().PadLeft(4, '0');
        endDay.text = endDate.Day.ToString().PadLeft(2, '0');
        endMonth.text = endDate.Month.ToString().PadLeft(2, '0');
        endYear.text = endDate.Year.ToString().PadLeft(4, '0');
        fishes.text = "Iloœæ Z³owionych ryb: " + fishesCount;
        time.text = "Czas spêdzony nad wod¹: " + ((int)deltaTime.TotalHours).ToString().PadLeft(2, '0') + ":" + deltaTime.Minutes.ToString().PadLeft(2,'0');
        baits.text = "Iloœæ u¿ytych przynêt: " + baitsList.Count;
        saves.text = "Iloœæ wyjazdów: " + savesCount;
        lakes.text = "Iloœæ odwiedzonych ³owisk: " + lakesList.Count;
    }
    public void ExitStats()
    {
        stats.SetActive(false);
        lakeName.text = "";
    }
    public void EditStartDate()
    {
        var data = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id];
        if (startMonth.text.Length > 0 && startDay.text.Length > 0 && startYear.text.Length > 0)
        {
            if (startMonth.text[0] != '-' && startDay.text[0] != '-' && startYear.text[0] != '-')
            {
                var startMonthInt = int.Parse(startMonth.text);
                var startDayInt = int.Parse(startDay.text);
                var startYearInt = int.Parse(startYear.text);
                if (startDayInt > 0)
                {
                    //startMonth
                    if (startMonthInt <= 0 || startMonthInt > 12)
                    {
                        startMonth.text = data.month.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        startMonth.text = startMonth.text.ToString().PadLeft(2, '0');
                    }
                    //startDay
                    if (startMonthInt == 4 || startMonthInt == 6 || startMonthInt == 9 || startMonthInt == 11)
                    {
                        if (startDayInt > 30)
                        {
                            startDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            startDay.text = startDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (startMonthInt == 1 || startMonthInt == 3 || startMonthInt == 5 || startMonthInt == 7 || startMonthInt == 8 || startMonthInt == 10 || startMonthInt == 12)
                    {
                        if (startDayInt > 31)
                        {
                            startDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            startDay.text = startDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (startMonthInt == 2)
                    {
                        if (System.DateTime.IsLeapYear(startYearInt) && startDayInt > 29)
                        {
                            startDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else if (!System.DateTime.IsLeapYear(startYearInt) && startDayInt > 28)
                        {
                            startDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            startDay.text = startDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                }
                else
                {
                    startDay.text = data.day.ToString().PadLeft(2, '0');
                }
                //startYear
                if (startYearInt < 2010 || startYearInt > int.Parse(System.DateTime.Now.ToString("yyyy")))
                {
                    startYear.text = data.year.ToString().PadLeft(4, '0');
                }
            }
            else
            {
                startDay.text = data.day.ToString().PadLeft(2, '0');
                startMonth.text = data.month.ToString().PadLeft(2, '0');
                startYear.text = data.year.ToString().PadLeft(4, '0');
            }
        }
        ChangeData();
    }
    public void EditEndDate()
    {
        var data = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id];
        if (endMonth.text.Length > 0 && endDay.text.Length > 0 && endYear.text.Length > 0)
        {
            if (endMonth.text[0] != '-' && endDay.text[0] != '-' && endYear.text[0] != '-')
            {
                var endMonthInt = int.Parse(endMonth.text);
                var endDayInt = int.Parse(endDay.text);
                var endYearInt = int.Parse(endYear.text);
                if (endDayInt > 0)
                {
                    //endMonth
                    if (endMonthInt <= 0 || endMonthInt > 12)
                    {
                        endMonth.text = data.month.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        endMonth.text = endMonth.text.ToString().PadLeft(2, '0');
                    }
                    //endDay
                    if (endMonthInt == 4 || endMonthInt == 6 || endMonthInt == 9 || endMonthInt == 11)
                    {
                        if (endDayInt > 30)
                        {
                            endDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            endDay.text = endDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (endMonthInt == 1 || endMonthInt == 3 || endMonthInt == 5 || endMonthInt == 7 || endMonthInt == 8 || endMonthInt == 10 || endMonthInt == 12)
                    {
                        if (endDayInt > 31)
                        {
                            endDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            endDay.text = endDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                    else if (endMonthInt == 2)
                    {
                        if (System.DateTime.IsLeapYear(endYearInt) && endDayInt > 29)
                        {
                            endDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else if (!System.DateTime.IsLeapYear(endYearInt) && endDayInt > 28)
                        {
                            endDay.text = data.day.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            endDay.text = endDay.text.ToString().PadLeft(2, '0');
                        }
                    }
                }
                else
                {
                    endDay.text = data.day.ToString().PadLeft(2, '0');
                }
                //endYear
                if (endYearInt < 2010 || endYearInt > int.Parse(System.DateTime.Now.ToString("yyyy")))
                {
                    endYear.text = data.year.ToString().PadLeft(4, '0');
                }
            }
            else
            {
                endDay.text = data.day.ToString().PadLeft(2, '0');
                endMonth.text = data.month.ToString().PadLeft(2, '0');
                endYear.text = data.year.ToString().PadLeft(4, '0');
            }
        }
        ChangeData();
    }
    public void ChangeData()
    {
        System.DateTime start = new System.DateTime(int.Parse(startYear.text), int.Parse(startMonth.text), int.Parse(startDay.text));
        System.DateTime end = new System.DateTime(int.Parse(endYear.text), int.Parse(endMonth.text), int.Parse(endDay.text));
        if(end < start)
        {
            endYear.text = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].year.ToString().PadLeft(4,'0');
            endMonth.text = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].month.ToString().PadLeft(2, '0');
            endDay.text = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].day.ToString().PadLeft(2, '0');
            startYear.text = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].year.ToString().PadLeft(4, '0');
            startMonth.text = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].month.ToString().PadLeft(2, '0');
            startDay.text = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].day.ToString().PadLeft(2, '0');
        }
        /*if(end > new System.DateTime(DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].year, DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].month, DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].day))
        {
            endYear.text = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].year.ToString().PadLeft(4, '0');
            endMonth.text = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].month.ToString().PadLeft(2, '0');
            endDay.text = DB.saveData[addBehaviour.months[0].saves[0].GetComponent<ScriptInPrefab>().id].day.ToString().PadLeft(2, '0');
        }
        if (start < new System.DateTime(DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].year, DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].month, DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].day))
        {
            startYear.text = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].year.ToString().PadLeft(4, '0');
            startMonth.text = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].month.ToString().PadLeft(2, '0');
            startDay.text = DB.saveData[addBehaviour.months[addBehaviour.months.Count - 1].saves[addBehaviour.months[addBehaviour.months.Count - 1].saves.Count - 1].GetComponent<ScriptInPrefab>().id].day.ToString().PadLeft(2, '0');
        }*/
        start = new System.DateTime(int.Parse(startYear.text), int.Parse(startMonth.text), int.Parse(startDay.text));
        end = new System.DateTime(int.Parse(endYear.text), int.Parse(endMonth.text), int.Parse(endDay.text));
        OpenStats(start, end);
    }
    public void ChangeLake()
    {
        lakeName.text = lakeName.text.ToLower();
        System.DateTime start = new System.DateTime(int.Parse(startYear.text), int.Parse(startMonth.text), int.Parse(startDay.text));
        System.DateTime end = new System.DateTime(int.Parse(endYear.text), int.Parse(endMonth.text), int.Parse(endDay.text));
        if(lakeName.text.Length > 0)
        {
            time.transform.parent.GetComponent<Button>().interactable = false;
            lakes.transform.parent.GetComponent<Button>().interactable = false;
        }
        OpenStats(start, end);
    }
    //MoreStats
    public void OpenMoreStats(int infoId)
    {
        System.DateTime start = new System.DateTime(int.Parse(startYear.text), int.Parse(startMonth.text), int.Parse(startDay.text));
        System.DateTime end = new System.DateTime(int.Parse(endYear.text), int.Parse(endMonth.text), int.Parse(endDay.text));
        moreStats.SetActive(true);
        switch(infoId)
        {
            case 0:
                topInfoText.text = "Z³owione Ryby";
                //obliczenia
                IDictionary<string, int> fishes = new Dictionary<string, int>();
                foreach (var item in DB.saveData)
                {
                    System.DateTime itemDate = new System.DateTime(item.year, item.month, item.day);
                    if (itemDate >= start && itemDate <= end)
                    {
                        if (lakeName.text.Length > 0)
                        {
                            if (lakeName.text == item.lake)
                            {
                                foreach (var fish in item.fishes)
                                {
                                    if (!fishes.ContainsKey(fish.fish.ToLower()))
                                    {
                                        fishes.Add(fish.fish, 1);
                                    }
                                    else
                                    {
                                        fishes[fish.fish]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var fish in item.fishes)
                            {
                                if (!fishes.ContainsKey(fish.fish.ToLower()))
                                {
                                    fishes.Add(fish.fish, 1);
                                }
                                else
                                {
                                    fishes[fish.fish]++;
                                }
                            }
                        }
                    }
                }
                fishes = fishes.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var item in fishes)
                {
                    var info = Instantiate(prefab);
                    info.transform.parent = moreStats.transform.Find("ScrollRect").Find("Info").transform;
                    if (moreStats.transform.Find("ScrollRect").Find("Info").childCount % 2 == 0)
                    {
                        info.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                    }
                    info.transform.localScale = new Vector3(1, 1, 1);
                    info.transform.Find("Name").GetComponent<TMP_Text>().text = item.Key;
                    info.transform.Find("Data").GetComponent<TMP_Text>().text = item.Value + " szt.";
                }
                break;
            case 1:
                topInfoText.text = "Czas nad Wod¹";
                IDictionary<string, System.TimeSpan> time = new Dictionary<string, System.TimeSpan>();
                foreach (var item in DB.saveData)
                {
                    System.DateTime itemDate = new System.DateTime(item.year, item.month, item.day);
                    if (itemDate >= start && itemDate <= end)
                    {
                        System.TimeSpan itemStart = new System.TimeSpan(item.startHour, item.startMinute, 0);
                        System.TimeSpan itemEnd = new System.TimeSpan(item.endHour, item.endMinute, 0);
                        if (!time.ContainsKey(item.lake.ToLower()))
                        {
                            time.Add(item.lake, (itemEnd - itemStart));
                        }
                        else
                        {
                            time[item.lake] += (itemEnd - itemStart);
                        }
                    }
                }
                time = time.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var item in time)
                {
                    var info = Instantiate(prefab);
                    info.transform.parent = moreStats.transform.Find("ScrollRect").Find("Info").transform;
                    if (moreStats.transform.Find("ScrollRect").Find("Info").childCount % 2 == 0)
                    {
                        info.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                    }
                    info.transform.localScale = new Vector3(1, 1, 1);
                    info.transform.Find("Name").GetComponent<TMP_Text>().text = item.Key;
                    info.transform.Find("Data").GetComponent<TMP_Text>().text = ((int)item.Value.TotalHours).ToString().PadLeft(2,'0') + ":" + item.Value.Minutes.ToString().PadLeft(2,'0');
                }
                break;
            case 2:
                topInfoText.text = "U¿yte przynêty";
                IDictionary<string, int> baits = new Dictionary<string, int>();
                foreach (var item in DB.saveData)
                {
                    System.DateTime itemDate = new System.DateTime(item.year, item.month, item.day);
                    if (itemDate >= start && itemDate <= end)
                    {
                        if (lakeName.text.Length > 0)
                        {
                            if (lakeName.text == item.lake)
                            {
                                foreach (var fish in item.fishes)
                                {
                                    if (!baits.ContainsKey(fish.bait.ToLower()))
                                    {
                                        baits.Add(fish.bait, 1);
                                    }
                                    else
                                    {
                                        baits[fish.bait]++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var fish in item.fishes)
                            {
                                if (!baits.ContainsKey(fish.bait.ToLower()))
                                {
                                    baits.Add(fish.bait, 1);
                                }
                                else
                                {
                                    baits[fish.bait]++;
                                }
                            }
                        }   
                    }
                }
                baits = baits.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var item in baits)
                {
                    var info = Instantiate(prefab);
                    info.transform.parent = moreStats.transform.Find("ScrollRect").Find("Info").transform;
                    if (moreStats.transform.Find("ScrollRect").Find("Info").childCount % 2 == 0)
                    {
                        info.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                    }
                    info.transform.localScale = new Vector3(1, 1, 1);
                    info.transform.Find("Name").GetComponent<TMP_Text>().text = item.Key;
                    if(item.Value == 1)
                    {
                        info.transform.Find("Data").GetComponent<TMP_Text>().text = item.Value + " raz";
                    }
                    else
                    {
                        info.transform.Find("Data").GetComponent<TMP_Text>().text = item.Value + " razy";
                    }
                }
                break;
            case 4:
                topInfoText.text = "Odwedzone £owiska";
                IDictionary<string, int> lakes = new Dictionary<string, int>();
                foreach (var item in DB.saveData)
                {
                    System.DateTime itemDate = new System.DateTime(item.year, item.month, item.day);
                    if (itemDate >= start && itemDate <= end)
                    {
                        if (!lakes.ContainsKey(item.lake.ToLower()))
                        {
                            lakes.Add(item.lake, 1);
                        }
                        else
                        {
                            lakes[item.lake]++;
                        }
                    }
                }
                lakes = lakes.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                foreach (var item in lakes)
                {
                    var info = Instantiate(prefab);
                    info.transform.parent = moreStats.transform.Find("ScrollRect").Find("Info").transform;
                    if (moreStats.transform.Find("ScrollRect").Find("Info").childCount % 2 == 0)
                    {
                        info.transform.Find("background").GetComponent<RawImage>().color = new Color(0.3392221f, 0.5943396f, 0.5381272f);
                    }
                    info.transform.localScale = new Vector3(1, 1, 1);
                    info.transform.Find("Name").GetComponent<TMP_Text>().text = item.Key;
                    if (item.Value == 1)
                    {
                        info.transform.Find("Data").GetComponent<TMP_Text>().text = item.Value + " raz";
                    }
                    else
                    {
                        info.transform.Find("Data").GetComponent<TMP_Text>().text = item.Value + " razy";
                    }
                }
                break;
        }
    }
    public void ExitMoreStats()
    {
        moreStats.SetActive(false);
        for (int i = moreStats.transform.Find("ScrollRect").Find("Info").childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(moreStats.transform.Find("ScrollRect").Find("Info").GetChild(i).gameObject);
        }
    }
}
[System.Serializable]
public class Stats : MonoBehaviour
{
    public TMP_InputField startDay;
    public TMP_InputField startMonth;
    public TMP_InputField startYear;
    public TMP_InputField endDay;
    public TMP_InputField endMonth;
    public TMP_InputField endYear;
    public TMP_InputField lakeName;
    public TMP_Text fishes;
    public TMP_Text time;
    public TMP_Text baits;
    public TMP_Text saves;
    public TMP_Text lakes;
}

