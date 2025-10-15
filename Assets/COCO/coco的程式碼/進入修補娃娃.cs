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
        // ...（原本動畫部分）

        yield return new WaitForSeconds(1.4f);

        // ✅ 儲存角色位置與動畫
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var posScript = player.GetComponent<人物位置>();
            if (posScript != null)
                posScript.SaveCurrentTransform(); // 記錄位置 + 動畫
        }

        SceneManager.LoadScene("修補娃娃");
    }
}
