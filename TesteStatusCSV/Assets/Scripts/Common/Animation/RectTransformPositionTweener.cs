using UnityEngine;
using System.Collections;

public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    RectTransform rt;

    protected override void Awake()
    {
        base.Awake();
        rt = transform as RectTransform;
    }

    protected override void OnUpdate(object sender, object args)
    {
        base.OnUpdate(sender, args);
        rt.anchoredPosition = currentValue;
    }
}