using UnityEngine;
using System.Collections;

public class PoisonStatusEffect : StatusEffect
{
    public float poisonDamage;

    void OnEnable()
    {
        this.AddObserver(OnNewTick, TimeController.TickNotification);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnNewTick, TimeController.TickNotification);
    }

    void OnNewTick(object sender, object args)
    {
        Stats s = this.GetComponentInParent<Stats>();
        float currentHP = s[StatTypes.HP];
        poisonDamage = effectPower;

        int whole = Mathf.FloorToInt(poisonDamage);
        float fraction = poisonDamage - whole;

        int damage = whole;
        if (UnityEngine.Random.value > (1f - fraction))
            damage++;

        s.SetValue(StatTypes.HP, (currentHP - damage), true);

        DurationStatusCondition dsc = GetComponentInChildren<DurationStatusCondition>();
        dsc.totalEffectValue += damage;
    }
}

