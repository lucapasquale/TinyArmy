using UnityEngine;
using System.Collections;
using System;

public class WalkEnemyMovement : BaseEnemyMovement
{
    float timeAttack;
   

    public override void Move(GameObject target)
    {
        Vector3 posDif = target.transform.position - transform.position;
        transform.Translate(new Vector2(Mathf.Sign(posDif.x), 0) * speed * Time.deltaTime);
    }

    public override void Attack(GameObject target)
    {
        float damage = myStats[StatTypes.DMG];
        float delay = 1f / myStats[StatTypes.ASpeed];

        if (Time.time >= timeAttack)
        {
            timeAttack = Time.time + delay;
            int damageToEnemy = DamageDealt(damage, target);
            this.target.GetComponentInParent<Stats>()[StatTypes.HP] -= damageToEnemy;
        }
        else
        {
            if (Time.time >= waitingAttack.timeWaitingAttack)
                GetComponent<BaseWaitingAttack>().WaitingAttack(target);
        }
    }

    
}
