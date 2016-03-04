using UnityEngine;
using System.Collections;

public class FireStatusEffect : StatusEffect
{

    Stats myStats;
    int deltaDEF;

    void OnEnable()
    {
        StartCoroutine(FireEffect());
    }

    IEnumerator FireEffect()
    {
        yield return 0; //wait 1 frame

        float firePower = effectPower;
        myStats = GetComponentInParent<Stats>();
        float initialDEF = myStats[StatTypes.DEF];
        deltaDEF = Mathf.FloorToInt(initialDEF * firePower);
        myStats.SetValue(StatTypes.DEF, (initialDEF - deltaDEF), true);

        DurationStatusCondition dsc = GetComponentInChildren<DurationStatusCondition>();
        dsc.totalEffectValue = deltaDEF;
    }

    void OnDisable()
    {
        myStats.SetValue(StatTypes.DEF, myStats[StatTypes.DEF] + deltaDEF, false);
    }

}