using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHit = 3;
    private int currentHit = 0;

    [Header("黑屏動畫")]
    public GameObject blackScreenObject;      // 黑屏動畫物件
    public float blackScreenDuration = 1.5f;  // 黑屏動畫總長

    [Header("UI設定")]
    public GameObject gameOverImage;          // 死亡圖片
    public float imageShowTime = 1f;        // 黑屏動畫到幾秒顯示圖片
    public GameObject 重來;                   // 重來按鈕

    public void TakeDamage(int damage)
    {
        currentHit += damage;

        if (currentHit >= maxHit)
        {
            StartCoroutine(DieSequence());
        }
    }
    IEnumerator DieSequence()
    {
        // 暫停遊戲
        Time.timeScale = 0f;

        // 顯示黑屏動畫
        blackScreenObject.SetActive(true);
        Animator anim = blackScreenObject.GetComponent<Animator>();
        anim.Play(0, 0, 0f); // 從頭播放

        // 等到 UI 要出現的時間
        yield return new WaitForSecondsRealtime(imageShowTime);

        // 顯示圖片
        if (gameOverImage != null)
            gameOverImage.SetActive(true);

        // 等到黑屏動畫結束
        yield return new WaitForSecondsRealtime(blackScreenDuration - imageShowTime);

        // 隱藏黑屏動畫
        blackScreenObject.SetActive(false);

        // 顯示重來按鈕
        if (重來 != null)
            重來.SetActive(true);
    }
}
