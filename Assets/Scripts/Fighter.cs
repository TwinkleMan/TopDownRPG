using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;
    public float stunTime = 5.0f;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;

    //needed for stunlock
    private float enemyXSpeedOld = 0f;
    private float enemyYSpeedOld = 0f;
    private Enemy enemy;

    //All fighters can ReceiveDamage / Die
    protected virtual void RecieveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //particle effect of hit
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            //Debug.Log($"Got hit, I am {this.GetType().ToString()}");
            if (this.GetType().ToString().Equals("Enemy"))
            {
                enemy = this.GetComponent<Enemy>();

                enemyXSpeedOld = enemy.enemyXSpeed;
                enemyYSpeedOld = enemy.enemyYSpeed;

                enemy.enemyXSpeed = 0;
                enemy.enemyYSpeed = 0;

                StartCoroutine(WaitForStunToEnd());
            }


            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    IEnumerator WaitForStunToEnd()
    {
        yield return new WaitForSeconds(stunTime);
        enemy.enemyXSpeed = enemyXSpeedOld;
        enemy.enemyYSpeed = enemyYSpeedOld;
    }

    protected virtual void Death()
    {

    }
}
