using UnityEngine;
using System.Collections;
using System;


public class FlyEnemyMovement : BaseEnemyMovement
{
    void FixedUpdate()
    {
        target = null;
        target = GetClosestTarget(Mathf.Infinity);
        
        if (!target)
            return;

        if (target)
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
        transform.Translate(posDif.normalized * speed * Time.deltaTime);
    }

    public override void Attack(GameObject target)
    {
        
    }
}

