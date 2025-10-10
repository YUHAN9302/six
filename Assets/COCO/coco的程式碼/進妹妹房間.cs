using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 進妹妹房間 : MonoBehaviour
{
    [Header("音效與動畫")]
    public GameObject openDoorSoundObject;       // 開門音效物件
    public GameObject closeEyesAnimationObject;  // 關眼動畫物件

    [Header("劇情UI")]
    public GameObject doorDialogueUI;            // 點門後的劇情UI（例如對話框）

    [Header("場景設定")]
    public string sceneName = "妹妹房間";       // 要切換的場景名稱

    private bool isClicked = false;
    private bool isDialogueFinished = false;     // 確保只切換一次場景

    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 儲存玩家位置
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            位置紀錄.SetPosition(player.transform.position);
        }

        StartCoroutine(HandleDoorEvent());
    }

    IEnumerator HandleDoorEvent()
    {
        // 同時播放開門音效與關眼動畫
        if (openDoorSoundObject != null)
        {
            openDoorSoundObject.SetActive(true);
            AudioSource audioSource = openDoorSoundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play(); // 播放後不等待
            }
        }

        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CloseEyes");
            }
        }

        // 開啟劇情UI（例如對話）
        if (doorDialogueUI != null)
        {
            doorDialogueUI.SetActive(true);
        }

        StartCoroutine(WaitForDialogueToClose());

        yield return null;

    }

    IEnumerator WaitForDialogueToClose()
    {
        // 等待直到劇情UI關閉
        while (doorDialogueUI != null && doorDialogueUI.activeSelf)
        {
            yield return null; // 每幀檢查一次
        }

        if (!isDialogueFinished)
        {
            isDialogueFinished = true;
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("妹妹房間");
    }

}
