using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 翻頁動畫 : MonoBehaviour
{
    public Animator bookAnimator;  // 書的 Animator
    public int totalPages = 6;     // 翻頁總數
    private int currentPage = 0;   // 目前翻頁數
    private bool isAnimating = false; // 防止連點

    private bool isClosed = false; // 標記是否已關書

    void Start()
    {
        // 初始打開日記動畫

    }

    public void OnBookClicked()
    {
        if (isAnimating) return;

        isAnimating = true;

        if (isClosed)
        {
            // 關書後下一次點擊 → 播打開日記動畫
            bookAnimator.ResetTrigger("打開日記");
            bookAnimator.SetTrigger("打開日記");
            isClosed = false;  // 重置關書狀態
            currentPage = 0;   // 從第一頁開始翻
        }

        else if (currentPage < totalPages)
        {
            // 翻頁 Trigger
            bookAnimator.ResetTrigger("內頁翻"); // 確保前一次 Trigger 不影響
            bookAnimator.SetTrigger("內頁翻");
            currentPage++;
        }
        else
        {
            // 第6頁 → 播關書動畫
            bookAnimator.ResetTrigger("關書");
            bookAnimator.SetTrigger("關書");
            isClosed = true; // 標記已關書
        }

        Invoke("AllowClick", 0.8f);

    }

    private void AllowClick()
    {
        isAnimating = false;
    }
}

