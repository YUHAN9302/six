using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 離開追逐 : MonoBehaviour
{
    [Header("音效與動畫")]
    public GameObject soundObject;              // 音效物件
    public GameObject closeEyesAnimationObject; // 關眼動畫物件

    [Header("劇情UI (可選)")]
    public GameObject dialogueUI;               // 可選對話框

    [Header("場景設定")]
    public string sceneName = "走廊2F";         // 要切換的場景名稱

    private bool isClicked = false;
    private bool isSceneChanged = false;

    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 如果有 UI，先開啟 UI
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(true);
            StartCoroutine(WaitForDialogueEnd());
        }
        else
        {
            // 沒有 UI 就直接播放音效+動畫→切場景
            StartCoroutine(PlaySoundAndChangeScene());
        }
    }

    // 等待玩家關閉對話框
    IEnumerator WaitForDialogueEnd()
    {
        while (dialogueUI != null && dialogueUI.activeSelf)
        {
            yield return null; // 每幀檢查一次
        }

        if (!isSceneChanged)
        {
            isSceneChanged = true;
            StartCoroutine(PlaySoundAndChangeScene());
        }
    }

    IEnumerator PlaySoundAndChangeScene()
    {
        // 播放音效
        if (soundObject != null)
            soundObject.SetActive(true);

        // 播放關眼動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("CloseEyes");
        }

        // 等待 2 秒（保留原本動畫時間）
        yield return new WaitForSeconds(2f);

        // 儲存主角位置（保留原本功能）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            位置紀錄.SetPosition(player.transform.position);
        }

        // 關閉音效物件
        if (soundObject != null)
            soundObject.SetActive(false);

        // 切換場景
        SceneManager.LoadScene(sceneName);
    }
}
