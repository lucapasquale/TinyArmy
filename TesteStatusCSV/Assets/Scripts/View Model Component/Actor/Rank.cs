using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour
{
    #region Consts
    public const int minLevel = 1;
    public const int maxLevel = 100;
    public const int maxExperience = 999999;
    #endregion

    #region Fields / Properties
    public float LVL
    {
        get { return stats[StatTypes.LVL]; }
    }

    public float EXP
    {
        get { return stats[StatTypes.EXP]; }
        set { stats[StatTypes.EXP] = value; }
    }

    public float ExpPercent { get; set; }

    Stats stats;
    #endregion

    #region MonoBehaviour
    void Awake()
    {
        stats = GetComponent<Stats>();
    }

    void OnEnable()
    {
        this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), stats);
        this.AddObserver(OnLvlDidChange, Stats.DidChangeNotification(StatTypes.LVL), stats);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnLvlDidChange, Stats.DidChangeNotification(StatTypes.LVL), stats);
    }
    #endregion

    #region Event Handlers
    void OnLvlDidChange(object sender, object args)
    {
        Stats stats = sender as Stats;
        float oldLvl = (float)args;
        int lvl = (int) stats[StatTypes.LVL];
        for (; oldLvl < lvl; oldLvl++)
        {
            LevelStats();
            //Debug.Log(stats.classType + " leveled up to: " + lvl);
        }         
    }

    void OnExpDidChange(object sender, object args)
    {
        stats.SetValue(StatTypes.LVL, LevelForExperience((int)EXP), false); //Verifica se precisa atualizar Level

        int curLevelExp = ExperienceForLevel((int)LVL);         //XP necessaria para lvl atual
        int nextLevelExp = ExperienceForLevel((int)LVL + 1);    //XP necessaria para prox lvl
        ExpPercent = ((EXP - curLevelExp) / (nextLevelExp - curLevelExp)) * 100f;   //Atualiza Exp %
    }
    #endregion

    #region Public
    public static int ExperienceForLevel(int level)
    {
        int aux = level * (level - 1);
        return aux * 50;
    }

    public static int LevelForExperience(int exp)
    {
        int lvl = maxLevel;
        for (; lvl >= minLevel; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }

    public void Init(int level)
    {
        stats.SetValue(StatTypes.LVL, level, false);
        stats.SetValue(StatTypes.EXP, ExperienceForLevel(level), false);
    }
    #endregion

    #region Private
    void LevelStats()
    {
        stats[StatTypes.STR] += stats[StatTypes.STRlvl];
        stats[StatTypes.AGI] += stats[StatTypes.AGIlvl];
        stats[StatTypes.INT] += stats[StatTypes.INTlvl];
        stats[StatTypes.RES] += stats[StatTypes.RESlvl];
        stats[StatTypes.LUK] += stats[StatTypes.LUKlvl];

        stats[StatTypes.MHP] += stats[StatTypes.HPlvl];
        stats[StatTypes.HP] += stats[StatTypes.MHP];
    }
    #endregion
}