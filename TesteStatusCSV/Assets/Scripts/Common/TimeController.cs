using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeController : MonoBehaviour
{
    public const string TickNotification = "TimeController.TickNotification";

    public int ticks;

    void OnEnable()
    {

        InvokeRepeating("TickNotifications", 0, 0.1f);
    }

    void TickNotifications()
    {
        ticks++;
        this.PostNotification(TickNotification);

    }


}
