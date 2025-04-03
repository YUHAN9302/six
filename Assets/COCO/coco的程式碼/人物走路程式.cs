using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 人物走路程式 : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb; // 2D 物理剛體
    public float moveSpeed = 8f; // 移動速度

    public bool isClothed = false;
    public bool canMove = true; // 是否允許移動
    private bool isWalking = false;
    private bool isRight = true;

    public float minX = -12f; // 左邊界
    public float maxX = 13f;  // 右邊界

    public Clothed ClothedScript;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        animator.SetBool("isClothed", false);
    }

    // Update is called once per frame
    void Update()
    {
        /*  if (!canMove)
          {
              rb.velocity = new Vector2(0, rb.velocity.y); // 停止移動
              rb.bodyType = RigidbodyType2D.Static; // **讓 Rigidbody2D 停止接受物理影響**
              anim.SetBool("isWalking", false); // 確保動畫回到待機狀態
              return; // 直接跳出 Update，避免處理移動邏輯
          }
          else
          {
             // rb.bodyType = RigidbodyType2D.Dynamic; // **恢復 Rigidbody2D**
          }
        */


        /*if (Input.GetKeyDown(KeyCode.C))
        {
            isClothed = true;
            anim.SetBool("isClothed", true);

            if (ClothedScript != null)
            {
                ClothedScript.enabled = true; // 啟用 go 腳本
                Debug.Log("Clothed 腳本已啟用！");
            }
            else
            {
                Debug.LogError("ClothedScript 為 null，無法啟用！");
            }
            this.enabled = false; // 關閉自己（人物走路程式）
            return;
        }*/ /*試人物是否可以換裝用*/



        float moveX = 0f;

        // 檢測 A 或 ← 鍵移動左，D 或 → 鍵移動右
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isWalking = true;
            isRight = false;
            moveX = moveSpeed;

            // transform.rotation = Quaternion.Euler(0, 0, 0); // 角色翻轉

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isWalking = true;
            isRight = true;
            moveX = -moveSpeed;


            // transform.rotation = Quaternion.Euler(0, 180, 0); // 角色朝右
        }
        else {
           
            isWalking = false;
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRight", isRight);

        // 移動角色
        //rb.velocity = new Vector2(moveX, rb.velocity.y);
        transform.Translate(moveX* Time.deltaTime,0,0);
        // 設定 Animator 參數

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

    }
    public void ChangeClothes()
    {
        isClothed = true;
        animator.SetBool("isClothed", true);

        if (ClothedScript != null)
        {
            ClothedScript.enabled = true;  // 啟用 `go` 腳本
            Debug.Log("Clothed 腳本已成功啟用！");
        }
        else
        {
            Debug.LogError("ClothedScript 仍然為 null，請確認是否手動綁定！");
        }

        this.enabled = false; // 關閉 `人物走路程式`
    }
   
}
