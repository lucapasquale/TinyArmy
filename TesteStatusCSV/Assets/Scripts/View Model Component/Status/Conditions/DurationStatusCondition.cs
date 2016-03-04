using UnityEngine;
using System.Collections;

public class DurationStatusCondition : StatusCondition
{
    public float duration = 100f;
    public int totalEffectValue;

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
        duration--;
        if (duration <= 0)
        {
            StatusEffect se = GetComponentInParent<StatusEffect>();
            switch (se.name)
            {
                case "PoisonStatusEffect":
                    //Debug.Log("Total Poison damage: " + totalEffectValue);
                    totalEffectValue = 0;
                    break;
                case "FireStatusEffect":
                    //Debug.Log("Total DEF reduction: " + totalEffectValue);
                    totalEffectValue = 0;
                    break;
                default:
                    Debug.Log("erro status: " + totalEffectValue);
                    break;
            }
            
            Remove();
        }
            
    }
}