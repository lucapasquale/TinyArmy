using UnityEngine;
using System.Collections;

public abstract class BaseEnemyMovement : MonoBehaviour
{
    #region Fields / Properties
    public GameObject target;
    public float speed { get { return myStats[StatTypes.MOV]; } }

    public float range { get { return myStats[StatTypes.RANGE]; } }

    protected Stats myStats;
    protected BaseWaitingAttack waitingAttack;
    #endregion

    #region MonoBehaviour
    //Get Stats and WaitingAttack
    void Start()
    {
        myStats = GetComponent<Stats>();
        waitingAttack = GetComponent<BaseWaitingAttack>();
    }
    #endregion

    #region Abstract Functions
    public abstract void Move(GameObject target);

    public abstract void Attack(GameObject target);
    #endregion

    #region Public
    //Add Status Effect
    public void Add<T>(GameObject target, float duration, float statusPower) where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>(statusPower);
        condition.duration = duration;
    } 

    //Calculate Damage dealt
    public int DamageDealt(float totalDamage, GameObject target)
    {
        float targetDEF = target.GetComponentInParent<Stats>()[StatTypes.DEF];
        float resPercent = 100f / (100f + targetDEF);
        float _dmg = totalDamage * resPercent;
        return Mathf.FloorToInt(_dmg);
    }
    #endregion

    #region Protected
    //Get the nearest target
    protected GameObject GetClosestTarget(float range)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        if (gos != null)
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && diff.y < range)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }
        else
        {
            Debug.Log("Player nao encontrado");
            return null;
        }
    }
    #endregion
}
