using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    public float moveSpeed = 2f;


    private Animator anim;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    private bool playerInRange = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        transform.localScale = new Vector3(0.58f, 0.58f, 0.58f); // 固定向左
    }

    void Update()
    {
        // 如果不是攻擊狀態 → 左移
        if (!isAttacking)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // 玩家在範圍內且怪物不是正在攻擊 → 攻擊
        if (playerInRange && !isAttacking && Time.time >= lastAttackTime)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        anim.SetBool("isAttacking", true);
    }

    // 動畫事件呼叫，攻擊結束回 Idle
    public void EndAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", false);
    }

    public void DealDamage()
    {
        Debug.Log("攻擊玩家");
        // 這裡可以寫扣血：
        // player.GetComponent<PlayerHealth>().TakeDamage(10);
    }

    // 玩家碰到怪物 Collider 時觸發
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // 玩家離開怪物 Collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

            // 玩家離開 → 強制回 Idle，停止攻擊
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }
    }
}
