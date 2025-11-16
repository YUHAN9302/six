using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truecandy : MonoBehaviour
{
    [Header("道具名稱（用於標誌）")]
    public string itemName = "糖果";

    private bool isCollected = false;

    private void OnMouseUp()
    {
        if (isCollected) return;
        isCollected = true;

        //  設 PlayerPrefs 標誌，表示玩家擁有糖果
        PlayerPrefs.SetInt("HasCandy", 1);
        PlayerPrefs.Save();

        // 隱藏糖果物件
        gameObject.SetActive(false);

        Debug.Log("玩家已取得糖果（{itemName}）");
    }
}
