using UnityEngine;
using System.Collections;

public class SlowStatusEffect : StatusEffect
{

    Stats myStats;
    int deltaMOV;

    void OnEnable()
    {
        StartCoroutine(SlowEffect());
    }

    IEnumerator SlowEffect()
    {
        yield return 0; //wait 1 frame

        float slowPower = effectPower;
        myStats = GetComponentInParent<Stats>();
        float initialMOV = myStats[StatTypes.MOV];
        deltaMOV = Mathf.FloorToInt(initialMOV * slowPower);
        myStats.SetValue(StatTypes.MOV, (initialMOV - deltaMOV), false);
    }

    void OnDisable()
    {
        myStats.SetValue(StatTypes.MOV, myStats[StatTypes.MOV] + deltaMOV, false);
    }

}
