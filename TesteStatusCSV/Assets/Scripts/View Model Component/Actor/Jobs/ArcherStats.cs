using UnityEngine;
using System.Collections;

public class ArcherStats : Stats
{
    Stats stats;

    void Awake()
    {
        stats = GetComponent<Stats>();

        stats.classType = ClassTypes.Archer;
        stats[StatTypes.MHP] = 8;
        stats[StatTypes.HPlvl] = 4;

        stats[StatTypes.STR] = 5;
        stats[StatTypes.STRlvl] = 2;
        stats[StatTypes.AGI] = 5;
        stats[StatTypes.AGIlvl] = 2;
        stats[StatTypes.INT] = 5;
        stats[StatTypes.INTlvl] = 2;
        stats[StatTypes.RES] = 5;
        stats[StatTypes.RES] = 2;
        stats[StatTypes.LUK] = 5;
        stats[StatTypes.LUKlvl] = 2;    
    }


}