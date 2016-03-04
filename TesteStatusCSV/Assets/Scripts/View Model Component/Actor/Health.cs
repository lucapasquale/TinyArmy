using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    #region Fields & Properties
    public float HP
    {
        get { return stats[StatTypes.HP]; }
        set { stats[StatTypes.HP] = (int)value; }
    }

    public float MHP
    {
        get { return stats[StatTypes.MHP]; }
        set { stats[StatTypes.MHP] = (int)value; }
    }

    int MinHP = 0;
    Stats stats;
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        stats = GetComponent<Stats>();
    }

    void OnEnable()
    {
        this.AddObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.AddObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
        this.AddObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPWillChange, Stats.WillChangeNotification(StatTypes.HP), stats);
        this.RemoveObserver(OnMHPDidChange, Stats.DidChangeNotification(StatTypes.MHP), stats);
        this.RemoveObserver(OnHPDidChange, Stats.DidChangeNotification(StatTypes.HP), stats);
    }
    #endregion

    #region Event Handlers
    void OnHPWillChange(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, MinHP, stats[StatTypes.MHP]));
    }

    void OnMHPDidChange(object sender, object args)
    {
        float oldMHP = (float)args;
        if (MHP < oldMHP)
            HP = Mathf.Clamp(HP, MinHP, MHP);   //Não deixar HP < 0 ou HP > MHP
    }

    void OnHPDidChange(object sender, object args)
    {
        float hp = stats[StatTypes.HP];
        if (hp <= 0)
        {
            Debug.Log(this.gameObject.name + " foi morto!");
            stats[StatTypes.Alive] = 0;
            this.gameObject.SetActive(false);

            
        }
    }
    #endregion
}