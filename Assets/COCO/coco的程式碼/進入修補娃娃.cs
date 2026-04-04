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
    public string sceneName = "修補娃娃";  // 要切換的場景名稱

    private bool isClicked = false;
    private bool isDialogueFinished = false;     // 確保只切換一次場景

    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 儲存進入前位置 + 動畫
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var posScript = player.GetComponent<人物位置>();
            if (posScript != null)
                posScript.SaveCurrentTransform(); // ✅ 這裡儲存進入前位置
        }

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
        // ✅ 先儲存玩家位置 + 動畫
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var posScript = player.GetComponent<人物位置>();
            if (posScript != null)
                posScript.SaveCurrentTransform(); // 先記錄位置 + 動畫
        }

        // ✅ 手動設定 ReturnDoorID，模擬「從哪扇門進來」
        DoorTrigger door = GetComponent<DoorTrigger>();
        if (door != null)
            位置紀錄.ReturnDoorID = door.doorID;

        // 播動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator anim = closeEyesAnimationObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("CloseEyes");
                yield return new WaitForSeconds(1f); // 動畫播放
            }
        }

        yield return new WaitForSeconds(0.4f); // 原本等待的額外延遲

        // 切換場景
        SceneManager.LoadScene(sceneName);
    }
}
