using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 重來 : MonoBehaviour
{
    public GameObject transitionObject; // 轉場動畫物件
    public float transitionTime = 0.4f; // 動畫播放時間
    public void RestartGame()
    {
        StartCoroutine(RestartWithTransition());
    }

    IEnumerator RestartWithTransition()
    {
        // 播放轉場動畫
        if (transitionObject != null)
        {
            transitionObject.SetActive(true);

            Animator anim = transitionObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play(0, 0, 0f); // 從頭播放
            }
        }

        // 等待動畫播放（用Realtime，因為之前Time.timeScale=0）
        yield return new WaitForSecondsRealtime(transitionTime);

        // 恢復時間
        Time.timeScale = 1f;

        // 重新載入場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
