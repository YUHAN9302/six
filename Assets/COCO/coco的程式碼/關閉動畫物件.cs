using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 關閉動畫物件 : MonoBehaviour
{
    [Header("自動關閉時間 (秒)")]
    public float delay = 2f;

    private void OnEnable()
    {
        // 先取消舊的 Invoke，避免重複計時或卡住
        CancelInvoke("DisableSelf");
        Invoke("DisableSelf", delay);
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        // 如果物件提前被關閉，取消 Invoke 避免報錯
        CancelInvoke();
    }
}
