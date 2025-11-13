using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class 進入下個洞 : MonoBehaviour
{
    [Header("目前螞蟻洞")]
    public GameObject currentHole;

    [Header("下一個螞蟻洞")]
    public GameObject nextHole;

    [Header("轉場物件")]
    public GameObject transitionObject;

    [Header("轉場延遲時間 (秒)")]
    public float transitionDelay = 1f; // 可以調整延遲時間

    private bool isSwitching = false;

    private void OnMouseDown()
    {
        if (!isSwitching)
            StartCoroutine(SwitchHoleWithDelay());
    }

    private IEnumerator SwitchHoleWithDelay()
    {
        isSwitching = true;

        // 1️⃣ 立即顯示轉場
        if (transitionObject != null)
            transitionObject.SetActive(true);

        // 2️⃣ 等待短暫延遲
        yield return new WaitForSeconds(transitionDelay);

        // 3️⃣ 切換螞蟻洞
        if (currentHole != null)
            currentHole.SetActive(false);
        if (nextHole != null)
            nextHole.SetActive(true);

        // 4️⃣ 不自動隱藏轉場
        isSwitching = false;
    }
}
