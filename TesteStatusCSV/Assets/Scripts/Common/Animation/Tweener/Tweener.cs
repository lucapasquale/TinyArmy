using UnityEngine;
using System;
using System.Collections;

public abstract class Tweener : MonoBehaviour
{
    #region Properties
    public static float DefaultDuration = 1f;
    public static Func<float, float, float, float> DefaultEquation = EasingEquations.EaseInOutQuad;

    public EasingControl easingControl;
    public bool destroyOnComplete = true;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        easingControl = gameObject.AddComponent<EasingControl>();
    }

    protected virtual void OnEnable()
    {
        this.AddObserver(OnUpdate, EasingControl.UpdateNotification);
        this.AddObserver(OnComplete, EasingControl.CompletedNotification);
    }

    protected virtual void OnDisable()
    {
        this.RemoveObserver(OnUpdate, EasingControl.UpdateNotification);
        this.RemoveObserver(OnComplete, EasingControl.CompletedNotification);
    }

    protected virtual void OnDestroy()
    {
        if (easingControl != null)
            Destroy(easingControl);
    }
    #endregion

    #region Event Handlers
    protected abstract void OnUpdate(object sender, object args);

    protected virtual void OnComplete(object sender, object args)
    {
        if (destroyOnComplete)
            Destroy(this);
    }
    #endregion
}