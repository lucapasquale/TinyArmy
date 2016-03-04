using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatExceptionFeature : Feature
{
    public StatTypes type;
    public float value;
    public bool delta;

    Stats myStats;

    protected override void OnApply()
    {
        myStats = GetComponentInParent<Stats>();
        if (myStats)
            this.AddObserver(OnStatException, Stats.WillChangeNotification(type), myStats);

    }

    protected override void OnRemove()
    {
        this.RemoveObserver(OnStatException, Stats.WillChangeNotification(type), myStats);

    }

    void OnStatException(object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        if (delta)
            vce.AddModifier(new MultDeltaModifier(0, value));
        else vce.AddModifier(new MultValueModifier(0, value));

    }

}
