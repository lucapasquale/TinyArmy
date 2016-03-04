using UnityEngine;
using System.Collections;


public class GoldManager : MonoBehaviour
{
    #region Fields / Properties
    public int Gold
    {
        get { return gold; }
        set { ChangeGold(value); }
    }

    [SerializeField]
    int gold;
    #endregion

    #region Notifications
    public const string GoldReducedNotification = "GoldManager.GoldReducedNotification";
    public const string GoldIncreasedNotification = "GoldManager.GoldReducedNotification";
    #endregion


    void ChangeGold(int value)
    {
        int oldGold = gold;

        if (value < 0)
            this.PostNotification(GoldReducedNotification, value);

        if (value > 0)
            this.PostNotification(GoldIncreasedNotification, value);

        gold = value;
        Debug.Log("Gold: " + gold + ", received: " + (gold - oldGold));
    }
}

