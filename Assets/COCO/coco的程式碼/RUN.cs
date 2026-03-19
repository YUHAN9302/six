using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RUN : MonoBehaviour
{
    [Header("跑步設定")]
    public float runMultiplier = 2f;    // 跑步倍率
    public bool canRun = true;           // 是否允許跑（場景控制用）

    private PlayerController playerController;
    private Animator animator;
    private float defaultSpeed;          // 儲存原本走路速度

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        if (playerController != null)
            defaultSpeed = playerController.moveSpeed; // 記住原始走路速度
    }

    void Update()
    {
        if (!canRun || playerController == null || !playerController.canMove) return;

        // 判斷是否按下 Shift 並且角色正在走路
        bool isRunning = Input.GetKey(KeyCode.LeftShift) &&
                         (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                          Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));

        // 設定 Animator
        animator.SetBool("isRunning", isRunning);

        // 調整 PlayerController 速度
        if (isRunning)
            playerController.moveSpeed = defaultSpeed * runMultiplier;
        else
            playerController.moveSpeed = defaultSpeed; // 恢復走路速度
    }
}
