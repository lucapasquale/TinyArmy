using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    #region Fields & Properties
    public float HP
    {
        get { return eStats[StatTypes.HP]; }
        set { eStats[StatTypes.HP] = (int)value; }
    }

    public float MHP
    {
        get { return eStats[StatTypes.MHP]; }
        set { eStats[StatTypes.MHP] = (int)value; }
    }

    int MinHP = 0;
    EnemyStats eStats;
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        eStats = GetComponent<EnemyStats>();
    }

    void OnEnable()
    {
        this.AddObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), eStats);
        this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), eStats);
        this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), eStats);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), eStats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), eStats);
        this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), eStats);
    }
    #endregion

    #region Event Handlers
    void OnHPWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, MinHP, eStats[StatTypes.MHP]));
    }

    void OnMHPDidChange(object sender, object args)
    {
        float oldMHP = (float)args;
        if (MHP < oldMHP)
            HP = Mathf.Clamp(HP, MinHP, MHP);   //Não deixar HP < 0 ou HP > MHP
    }

    void OnHPDidChange(object sender, object args)
    {
        float hp = eStats[StatTypes.HP];
        if (hp <= 0)
        {
            Debug.Log(this.gameObject.name + " foi morto!");
            HP = MHP;
            eStats[StatTypes.Alive] = 0;
            GameObject.FindObjectOfType<GoldManager>().Gold += eStats.gold;

            this.gameObject.SetActive(false);


        }
    }
    #endregion
}