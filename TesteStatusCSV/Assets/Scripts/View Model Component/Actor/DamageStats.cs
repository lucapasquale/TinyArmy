using UnityEngine;
using System.Collections;

public class DamageStats : MonoBehaviour
{
    #region Fields / Properties
    float STR { get { return stats[StatTypes.STR]; } }

    float AGI { get { return stats[StatTypes.AGI]; } }

    float INT { get { return stats[StatTypes.INT]; } }

    float LVL { get { return stats[StatTypes.LVL]; } }

    float weaponDMG { get { return stats[StatTypes.weaponDMG]; } }

    public float damage
    {
        get { return stats[StatTypes.DMG]; }
        set { stats[StatTypes.DMG] = value; }
    }

    public float attackSpeed
    {
        get { return stats[StatTypes.ASpeed]; }
        set { stats[StatTypes.ASpeed] = value; }
    }

    public float magicPower
    {
        get { return stats[StatTypes.MPOWER]; }
        set { stats[StatTypes.MPOWER] = value; }
    }

    Stats stats;
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        stats = GetComponent<Stats>();
    }

    void OnEnable()
    {
        this.AddObserver(OnDamageChanged, Stats.DidChangeNotification(StatTypes.STR), stats);
        this.AddObserver(OnDamageChanged, Stats.DidChangeNotification(StatTypes.weaponDMG), stats);
        this.AddObserver(OnSpeedChanged, Stats.DidChangeNotification(StatTypes.AGI), stats);
        this.AddObserver(OnMagicChanged, Stats.DidChangeNotification(StatTypes.INT), stats);
        
    }

    void OnDisable()
    {
        this.RemoveObserver(OnDamageChanged, Stats.DidChangeNotification(StatTypes.STR), stats);
        this.RemoveObserver(OnDamageChanged, Stats.DidChangeNotification(StatTypes.weaponDMG), stats);
        this.RemoveObserver(OnSpeedChanged, Stats.DidChangeNotification(StatTypes.AGI), stats);
        this.RemoveObserver(OnMagicChanged, Stats.DidChangeNotification(StatTypes.INT), stats);
        
    }
    #endregion

    #region Event Handlers
    void OnDamageChanged(object sender, object args)
    {
        float _damage = Mathf.FloorToInt(0.75f*Mathf.Pow(STR,1.2f) + LVL*0.5f);
        damage = weaponDMG + _damage;
    }

    void OnSpeedChanged(object sender, object args)
    {
        //float _aRate = 0.5f + 0.001f * LVL + 0.005f * AGI;
        attackSpeed = 0.8f;
    }

    void OnMagicChanged(object sender, object args)
    {
        float mBonus = 1 + 0.01f * INT;
        magicPower = mBonus;
    }
    #endregion
}