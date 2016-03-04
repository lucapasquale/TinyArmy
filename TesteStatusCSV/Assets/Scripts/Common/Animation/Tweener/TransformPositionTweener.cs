using UnityEngine;
using System;
using System.Collections;

public class TransformPositionTweener : Vector3Tweener
{
    protected override void OnUpdate(object sender, object args)
    {
        base.OnUpdate(sender, args);
        transform.position = currentValue;
    }
}