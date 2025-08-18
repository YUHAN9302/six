using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 人物位置 : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Vector3? savedPos = 位置紀錄.GetPosition();

        if (savedPos.HasValue)
        {
            transform.position = savedPos.Value;
           
        }

        // 回復動畫狀態
        string lastAnim = 位置紀錄.GetAnimState();
        if (!string.IsNullOrEmpty(lastAnim) && animator != null)
        {
            animator.Play(lastAnim);
        }
        位置紀錄.ClearPosition(); // 清除以免重複用到
    }
    public void SaveCurrentTransform()
    {
        // 保存位置
        位置紀錄.SetPosition(transform.position);

        // 保存動畫狀態
        if (animator != null)
        {
            string currentState = animator.GetCurrentAnimatorStateInfo(0).shortNameHash.ToString();
            currentState = animator.GetCurrentAnimatorStateInfo(0).IsName("走路") ? "走路" : "待機";
            位置紀錄.SetAnimState(currentState);
        }
    }
    // Update is called once per frame
}
