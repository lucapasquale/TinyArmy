using UnityEngine;
using System.Collections;

public abstract class BaseWaitingAttack : MonoBehaviour
{
    [HideInInspector]
    public float timeWaitingAttack;

    public abstract void WaitingAttack(GameObject target);
}
