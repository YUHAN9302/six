using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box出現 : MonoBehaviour
{
    public GameObject 開場對話; // 轉場動畫物件
    public GameObject boxButton;
    // Start is called before the first frame update
    private void Start()
    {
        boxButton.SetActive(false); // 確保一開始是隱藏的
        StartCoroutine(WaitForTransitionToEnd());
    }

    // Update is called once per frame
    private IEnumerator WaitForTransitionToEnd()
    {
        // 等待直到轉場物件被關閉
        Debug.Log("Coroutine started");

        while (開場對話.activeSelf)
        {
            yield return null; // 每幀檢查一次
        }
        Debug.Log("轉場物件已關閉，顯示按鈕");
        boxButton.SetActive(true); // 顯示按鈕
    }
}
