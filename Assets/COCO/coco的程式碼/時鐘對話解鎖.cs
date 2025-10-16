using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 時鐘對話解鎖 : MonoBehaviour
{
    public GameObject dialogueUI;       // 拖入8:20對話UI
    public int triggerHour = 8;
    public int triggerMinute = 20;
    private bool hasTriggered = false;

    [Header("要隱藏的按鈕")]
    public GameObject[] buttonsToHide;  // ⭐ 拖入你要隱藏的兩個按鈕


    // Update is called once per frame
    void Update()
    {
        if (!hasTriggered && ClockController.hours == triggerHour && ClockController.minutes == triggerMinute)
        {
            hasTriggered = true;
            ShowDialogue();
        }
    }
    void ShowDialogue()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(true);
        }
        foreach (var btn in buttonsToHide)
        {
            if (btn != null)
                btn.SetActive(false);
        }
    }

}
