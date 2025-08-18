using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI顯示 : MonoBehaviour
{
    public GameObject targetObjectToShow; // 要顯示的 UI 物件

    void Start()
    {
      if (targetObjectToShow != null)
            targetObjectToShow.SetActive(false); // 一開始隱藏

        StartCoroutine(WaitForTableEvent());
    }
    private IEnumerator WaitForTableEvent()
    {
        // 等待用餐劇情腳本完成事件（假設有 TableEventPlayed 旗標）
        while (!判定餐桌動畫.TableAnimPlayed)
        {
            yield return null; // 每幀檢查
        }

        // 事件完成後顯示 UI
        if (targetObjectToShow != null)
            targetObjectToShow.SetActive(true);

        Debug.Log("用餐劇情完成，UI顯示");
    }
}