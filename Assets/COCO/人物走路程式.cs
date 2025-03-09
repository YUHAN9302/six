using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 人物走路程式 : MonoBehaviour
{
    private Animator anim; // Animator 變數
    private Rigidbody2D rb; // 2D 物理剛體
    public float moveSpeed = 5f; // 移動速度

    public bool canMove = true; // 是否允許移動


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();  // 取得 Animator
        rb = GetComponent<Rigidbody2D>(); // 取得 Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // 停止移動
            rb.bodyType = RigidbodyType2D.Static; // **讓 Rigidbody2D 停止接受物理影響**
            anim.SetBool("isWalking", false); // 確保動畫回到待機狀態
            return; // 直接跳出 Update，避免處理移動邏輯
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // **恢復 Rigidbody2D**
        }


        float moveX = 0f;

        // 檢測 A 或 ← 鍵移動左，D 或 → 鍵移動右
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -moveSpeed;
            transform.rotation = Quaternion.Euler(0, 0, 0); // 角色翻轉
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = moveSpeed;
            transform.rotation = Quaternion.Euler(0, 180, 0); // 角色朝右
        }

        // 移動角色
        rb.velocity = new Vector2(moveX, rb.velocity.y);

        // 設定 Animator 參數
        anim.SetBool("isWalking", moveX != 0);
    }
}
