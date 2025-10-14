using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class 進入修補娃娃 : MonoBehaviour
{
    [Header("音效與動畫")]
    public GameObject closeEyesAnimationObject;  // 關眼動畫物件

    [Header("劇情UI")]
    public GameObject dialogueUI;                // 修補包點擊後的對話框

    [Header("場景設定")]
    public string sceneName = "修補娃娃場景";  // 要切換的場景名稱

    private bool isClicked = false;
    private bool isDialogueFinished = false;     // 確保只切換一次場景

    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 開啟對話框
        if (dialogueUI != null)
            dialogueUI.SetActive(true);

        StartCoroutine(WaitForDialogueEnd());
    }

    // 等待玩家關閉對話框
    IEnumerator WaitForDialogueEnd()
    {
        while (dialogueUI != null && dialogueUI.activeSelf)
        {
            yield return null; // 每幀檢查一次
        }

        if (!isDialogueFinished)
        {
            isDialogueFinished = true;
            StartCoroutine(PlayCloseEyesAndChangeScene());
        }
    }

    IEnumerator PlayCloseEyesAndChangeScene()
    {
        // 播放閉眼動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("CloseEyes");
        }

        // 等待動畫時間（假設 1 秒，你可依動畫長度調整）
        yield return new WaitForSeconds(1f);

        // 切換場景
        SceneManager.LoadScene("修補娃娃");
    }
}
