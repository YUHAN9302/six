using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 客廳開頭 : MonoBehaviour
{
    [Header("UI 元件")]
    public GameObject eyeCloseObject; // 閉眼動畫物件
    public GameObject dialogueUI;     // 開場對話UI

    private bool dialogueStarted = false;
    public static bool alreadyDestroyed = false; // 靜態，跨場景保留

    //[Header("設定")]
    //public string destroyKey = "客廳開場已刪除"; // PlayerPrefs key


    private void Start()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        StartCoroutine(WaitForEyeClose());
    }

    private IEnumerator WaitForEyeClose()
    {
        if (eyeCloseObject != null)
        {
            while (eyeCloseObject.activeSelf)
            {
                yield return null;
            }
        }

        ShowDialogueUI();
    }

    private void ShowDialogueUI()
    {
        if (dialogueStarted) return;

        dialogueStarted = true;

        if (dialogueUI != null)
            dialogueUI.SetActive(true);

        StartCoroutine(MonitorDialogueUI());
    }

    private IEnumerator MonitorDialogueUI()
    {
        while (dialogueUI != null && dialogueUI.activeSelf)
            yield return null;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }
}
