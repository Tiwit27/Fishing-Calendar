using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationsBehaviour : MonoBehaviour
{
    private void Start()
    {
        var id_1 = new AndroidNotificationChannel()
        {
            Id = "1",
            Name = "Wiadomo�� Startowa",
            Importance = Importance.Default,
            Description = "Wiadomo�� wysy�ana tylko raz na pocz�tku �owienia.",
            LockScreenVisibility = LockScreenVisibility.Public,
        };
        var id_2 = new AndroidNotificationChannel()
        {
            Id = "2",
            Name = "Wiadomo�� Ko�cowa",
            Importance = Importance.Default,
            Description = "Wiadomo�� wysy�ana po zako�czeniu �owienia.",
            LockScreenVisibility = LockScreenVisibility.Public,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(id_1);
        AndroidNotificationCenter.RegisterNotificationChannel(id_2);
    }
    public void SendStartNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Rozpocz�cie �owienia";
        notification.Text = "Udanego W�dkowania!";
        notification.ShouldAutoCancel = false;
        AndroidNotificationCenter.SendNotification(notification, "1");
    }
    public void SendEndNotification(int hour, int minute, int fishCount)
    {
        var notification = new AndroidNotification();
        notification.Title = "Zako�czy�e� w�dkowanie";
        notification.Text = "Oto podsumowanie dnia: Sp�dzi�e� " + hour + "godzin[a/y] i " + minute + " minut[a/y] nad wod�. Z�owi�e� " + fishCount + " ryb[�/y]";
        notification.ShouldAutoCancel = true;
        AndroidNotificationCenter.SendNotification(notification, "2");
    }
}
