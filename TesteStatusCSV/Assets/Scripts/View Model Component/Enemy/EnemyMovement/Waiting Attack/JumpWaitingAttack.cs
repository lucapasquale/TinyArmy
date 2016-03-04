using UnityEngine;
using System.Collections;

public class JumpWaitingAttack : BaseWaitingAttack
{
    public float jumpDelay;
    public float jumpForce;
    [Range(0, 90)]
    public int jumpAngle = 45;


    public override void WaitingAttack(GameObject target)
    {
        timeWaitingAttack = Time.time + jumpDelay;
        float sideToJump = Mathf.Sign(target.transform.position.x - transform.position.x);
        float angleRad = jumpAngle * Mathf.Deg2Rad;
        GetComponent<Rigidbody2D>().velocity = new Vector2(sideToJump * jumpForce * Mathf.Cos(angleRad), jumpForce * Mathf.Sin(angleRad));
    }
}
