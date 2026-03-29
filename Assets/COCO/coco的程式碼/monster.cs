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

    private GameObject playerObj; // 新增

    public float minX = 22f; // 左邊界


    void Start()
    {
        anim = GetComponent<Animator>();
        transform.localScale = new Vector3(0.58f, 0.58f, 0.58f); // 固定向左
    }

    void Update()
    {
        // 攻擊優先
        if (playerInRange && !isAttacking)
        {
            Attack();
        }
        else
        {
            // 如果不是攻擊狀態 → 左移，但限制不超過 minX
            if (!isAttacking)
            {
                float nextX = transform.position.x - moveSpeed * Time.deltaTime;

                // 限制左邊界
                if (nextX < minX)
                    nextX = minX;

                transform.position = new Vector3(nextX, transform.position.y, transform.position.z);
            }
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

        if (playerObj != null)
        {
            playerObj.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    // 玩家碰到怪物 Collider 時觸發
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 如果正在攻擊 → 不用直接殺（避免干擾）
            if (isAttacking) return;

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.InstantDie();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerObj = collision.gameObject; //記錄玩家
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
