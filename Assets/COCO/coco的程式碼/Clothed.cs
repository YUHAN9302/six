using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothed : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private bool isWalking = false;// 初始為不走路
    private bool isRight = true;
    private float moveSpeed = 4f;

    public bool canMove = true;
    public float minX = -12f; // 左邊界
    public float maxX = 13f;  // 右邊界

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // 當按下 D 鍵，角色向右走
        {
            isWalking = true;
            isRight = true;
            moveX = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // 當按下 A 鍵，角色向左走
        {
            isWalking = true;
            isRight = false;
            moveX = -moveSpeed;
        }
        else
        {
            isWalking = false; // 沒有按下 A 或 D 鍵，角色停止走路
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;  // 角色面向右邊
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isRight = false; // 角色面向左邊
        }


        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRight", isRight);

        rb.velocity = new Vector2(moveX, rb.velocity.y); // 只改變 x 軸的速度，y 軸保持不變


        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, 0f);  // 保持 z 軸不變

        // 確保角色只會在 X 軸移動
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
