using UnityEngine;
using System.Collections;

public class WarriorStats : Stats
{
    Stats stats;

    void Awake()
    {
        stats = GetComponent<Stats>();

        stats.classType = ClassTypes.Warrior;
        stats[StatTypes.MHP] = 20;
        stats[StatTypes.HP] = stats[StatTypes.MHP];
        stats[StatTypes.HPlvl] = 3;

        stats[StatTypes.STR] = 5;
        stats[StatTypes.STRlvl] = 2;
        stats[StatTypes.AGI] = 3;
        stats[StatTypes.AGIlvl] = 2;
        stats[StatTypes.INT] = 5;
        stats[StatTypes.INTlvl] = 2;
        stats[StatTypes.RES] = 5;
        stats[StatTypes.RES] = 2;
        stats[StatTypes.LUK] = 5;
        stats[StatTypes.LUKlvl] = 2;

        stats[StatTypes.RANGE] = 15;
        stats[StatTypes.MOV] = 20f;
    }


}