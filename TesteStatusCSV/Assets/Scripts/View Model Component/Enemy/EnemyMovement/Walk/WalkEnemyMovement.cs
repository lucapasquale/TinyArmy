using UnityEngine;
using System.Collections;
using System;

public class WalkEnemyMovement : BaseEnemyMovement
{
    float timeAttack;

    //Enemy AI
    void FixedUpdate()
    {
        float distToBase = GetComponent<Collider2D>().bounds.extents.y;
        bool grounded = Physics2D.Raycast(transform.localPosition, -Vector2.up, distToBase + 0.1f, LayerMask.GetMask("Ground"));

        target = null;
        target = GetClosestTarget(range);
        if (!target)
            GetClosestTarget(Mathf.Infinity);
        if (!target)
            return;

        if (target && grounded)
        {
            float distance = (target.transform.position - transform.position).magnitude;
            Debug.DrawRay(transform.position, target.transform.position - transform.position, (distance <= range) ? Color.green : Color.red, 0f);

            if (distance > range)
                Move(target);
            else Attack(target);
        }
    }

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
