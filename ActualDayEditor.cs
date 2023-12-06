using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActualDayEditor : MonoBehaviour
{
    [SerializeField] public bool isTicketOpen;
    [SerializeField] GameObject editDay;
    [SerializeField] DataBase DB;
    [SerializeField] JsonBehaviour Json;
    [SerializeField] EditForm form;

    void Start()
    {
        editDay.SetActive(false);
        if (PlayerPrefs.GetInt("bool") == 1)
        {
            Json.ReadData();
            OpenTicket(PlayerPrefs.GetInt("id"), DB.saveData[PlayerPrefs.GetInt("id")]);
        }
    }
    public void OpenTicket(int idTicket, NewDay info)
    {
        if (PlayerPrefs.GetInt("bool") != 1)
        {
            PlayerPrefs.SetInt("bool", 1);
            PlayerPrefs.SetInt("id", idTicket);
            PlayerPrefs.Save();
        }
        editDay.SetActive(true);
        form.temperature.text = info.temperature.ToString();
        form.windValue.text = info.windValue.ToString();
        form.windDirection.value = (int)info.windDirection;
        form.weather.value = (int)info.weather;
        form.editHour.text = System.DateTime.Now.Hour.ToString();
        form.editMinute.text = System.DateTime.Now.Minute.ToString();
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
    public GameObject updateData;
    public GameObject addFish;
    public GameObject endDay;
}
