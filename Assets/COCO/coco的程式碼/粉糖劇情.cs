using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 粉糖劇情 : MonoBehaviour
{
    [Header("劇情 UI（預設關閉）")]
    public GameObject dialogueUI;

    [Header("轉場動畫物件（進入客廳時會顯示）")]
    public GameObject transitionObject;

    [Header("如果轉場物件沒關閉，最多等多久（保險）")]
    public float maxWaitTime = 3f;

    void Start()
    {
        StartCoroutine(ShowStoryAfterTransition());
    }

    IEnumerator ShowStoryAfterTransition()
    {
        // 1️⃣ 必須已經點過糖果
        if (!儲存管理.Instance.hasClickedCandy)
            yield break;

        // 2️⃣ 如果劇情已經播過 → 不重複
        if (儲存管理.Instance.hasPlayedCandyReturnDialogue)
            yield break;

        // 3️⃣ 等待轉場動畫結束
        float timer = 0f;
        if (transitionObject != null)
        {
            while (transitionObject.activeSelf && timer < maxWaitTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        // 4️⃣ 顯示劇情 UI
        if (dialogueUI != null)
            dialogueUI.SetActive(true);

        Debug.Log("顯示糖果劇情（轉場結束後）");

        // 5️⃣ 標記已播放，避免重複
        儲存管理.Instance.hasPlayedCandyReturnDialogue = true;
    }
}
