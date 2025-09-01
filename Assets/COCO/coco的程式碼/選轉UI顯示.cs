using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 選轉UI顯示 : MonoBehaviour
{
    public GameObject clockStoryObject;
    public GameObject uiPanel;

    private void Start()
    {
        if (uiPanel != null)
            uiPanel.SetActive(false);

        StartCoroutine(CheckClockStory());
    }

    private System.Collections.IEnumerator CheckClockStory()
    {
        while (true)
        {
            if (clockStoryObject == null || !clockStoryObject.activeSelf)
            {
                if (uiPanel != null)
                    uiPanel.SetActive(true);

                Debug.Log("UI 已顯示，劇情物件已消失");

                yield break; // 停止 Coroutine
            }

            yield return null; // 每一幀檢查一次
        }
    }
}
