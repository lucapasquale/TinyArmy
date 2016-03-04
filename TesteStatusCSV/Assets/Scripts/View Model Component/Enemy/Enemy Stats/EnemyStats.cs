using UnityEngine;
using System.Collections;

public class EnemyStats : Stats
{
    Stats stats;
    public float MHP = 80f;
    public float HP;
    public float DMG = 3f;
    public float ASpeed = 0.5f;
    public float DEF = 10f;
    public float RANGE = 40f;
    public float MOV = 30f;

    public int gold = 50;

    void Awake()
    {
        stats = GetComponent<Stats>();

        stats.classType = ClassTypes.Enemy;
        stats[StatTypes.MHP] = MHP;
        stats[StatTypes.HP] = MHP;
        stats[StatTypes.DMG] = DMG;
        stats[StatTypes.ASpeed] = ASpeed;
        stats[StatTypes.DEF] = DEF;
        stats[StatTypes.RANGE] = RANGE;
        stats[StatTypes.MOV] = MOV;

        stats[StatTypes.Alive] = 1;
    }



}