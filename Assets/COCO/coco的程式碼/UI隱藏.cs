using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI隱藏 : MonoBehaviour
{
    public GameObject targetUI; // 要隱藏的 UI

    void Start()
    {
        StartCoroutine(CheckDollEvent());
    }

    IEnumerator CheckDollEvent()
    {
        // 等待直到娃娃被互動（也就是娃娃物件消失）
        while (!判定娃娃.DollInteracted)
        {
            yield return null;
        }

        // 閉UI
        targetUI.SetActive(false);
        Debug.Log("娃娃互動完成 → UI 隱藏");
    }
}
