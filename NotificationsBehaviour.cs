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
            Name = "Wiadomoœæ Startowa",
            Importance = Importance.Default,
            Description = "Wiadomoœæ wysy³ana tylko raz na pocz¹tku ³owienia.",
            LockScreenVisibility = LockScreenVisibility.Public,
        };
        var id_2 = new AndroidNotificationChannel()
        {
            Id = "2",
            Name = "Wiadomoœæ Koñcowa",
            Importance = Importance.Default,
            Description = "Wiadomoœæ wysy³ana po zakoñczeniu ³owienia.",
            LockScreenVisibility = LockScreenVisibility.Public,
        };

        AndroidNotificationCenter.RegisterNotificationChannel(id_1);
        AndroidNotificationCenter.RegisterNotificationChannel(id_2);
    }
    public void SendStartNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Rozpoczêcie ³owienia";
        notification.Text = "Udanego Wêdkowania!";
        notification.ShouldAutoCancel = false;
        AndroidNotificationCenter.SendNotification(notification, "1");
    }
    public void SendEndNotification(int hour, int minute, int fishCount)
    {
        var notification = new AndroidNotification();
        notification.Title = "Zakoñczy³eœ wêdkowanie";
        notification.Text = "Oto podsumowanie dnia: Spêdzi³eœ " + hour + "godzin[a/y] i " + minute + " minut[a/y] nad wod¹. Z³owi³eœ " + fishCount + " ryb[ê/y]";
        notification.ShouldAutoCancel = true;
        AndroidNotificationCenter.SendNotification(notification, "2");
    }
}
