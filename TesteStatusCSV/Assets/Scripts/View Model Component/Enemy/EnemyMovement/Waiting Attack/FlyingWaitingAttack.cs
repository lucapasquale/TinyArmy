using UnityEngine;
using System.Collections;

public class FlyWaitingAttack : BaseWaitingAttack
{
    public float jumpDelay;
    public float flySpeed;
    [Range(0, 90)]
    public int angle = 45;


    public override void WaitingAttack(GameObject target)
    {
        timeWaitingAttack = Time.time + jumpDelay;
        float sideToFly = Mathf.Sign(target.transform.position.x - transform.position.x);
        transform.Translate(new Vector2(sideToFly,0) * flySpeed * Time.deltaTime);
    }
}