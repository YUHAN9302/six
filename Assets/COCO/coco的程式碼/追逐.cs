using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 追逐 : MonoBehaviour
{
    [Header("音效與動畫")]
    public GameObject openJarSoundObject;        // 開螞蟻罐音效
    public GameObject closeEyesAnimationObject;  // 關眼動畫（可用於轉場）

    [Header("劇情UI")]
    public GameObject jarDialogueUI;             // 螞蟻罐劇情 UI（對話框）

    [Header("場景設定")]
    public string sceneName = "To be continued";

    private bool isClicked = false;
    private bool isDialogueFinished = false;

    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 儲存玩家位置（如果需要）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            位置紀錄.SetPosition(player.transform.position);
        }

        StartCoroutine(HandleJarEvent());
    }

    IEnumerator HandleJarEvent()
    {
        // 1️⃣ 播放開罐音效
        if (openJarSoundObject != null)
        {
            openJarSoundObject.SetActive(true);
            AudioSource audioSource = openJarSoundObject.GetComponent<AudioSource>();
            if (audioSource != null)
                audioSource.Play();
        }

        // 2️⃣ 打開劇情 UI（先跑劇情！）
        if (jarDialogueUI != null)
            jarDialogueUI.SetActive(true);

        // 等劇情關閉
        StartCoroutine(WaitForDialogueToClose());

        yield return null;
    }

    IEnumerator WaitForDialogueToClose()
    {
        // 🔄 等待劇情 UI 關閉
        while (jarDialogueUI != null && jarDialogueUI.activeSelf)
            yield return null;

        if (!isDialogueFinished)
        {
            isDialogueFinished = true;
            StartCoroutine(PlayTransitionThenChangeScene());
        }
    }
    IEnumerator PlayTransitionThenChangeScene()
    {
        // 3️⃣ 劇情關閉後 → 才播放「關眼轉場」
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);

            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("CloseEyes");
        }

        // 4️⃣ 等轉場動畫播放（你可調整時間）
        yield return new WaitForSeconds(1.5f);

        // 5️⃣ 進入場景
        SceneManager.LoadScene(sceneName);
    }
}
