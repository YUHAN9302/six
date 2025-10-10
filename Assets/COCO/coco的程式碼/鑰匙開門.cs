using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 鑰匙開門 : MonoBehaviour
{
    public string keyName; // 這扇門需要哪把鑰匙
                           // 把你的兩個劇情程式直接拖進這裡
    [Header("物件控制")]
    public GameObject lockedDoor;   // A：鎖住版本（顯示在沒鑰匙時）
    public GameObject unlockedDoor; // B：開啟版本（顯示在有鑰匙時）

    private void Start()
    {
        UpdateDoorState(); // 一開始根據鑰匙狀態設定顯示
    }



    private void OnMouseDown() // 滑鼠點擊互動（也可改E鍵互動）
    {
        if (鑰匙偵測.Instance.HasKey(keyName))
        {
            Debug.Log($"使用 {keyName} 開門！");
        }
        else
        {
            Debug.Log("門鎖著，沒有鑰匙。");
        }

        UpdateDoorState(); // 每次互動都重新檢查顯示
    }
    private void UpdateDoorState()
    {
        bool hasKey = 鑰匙偵測.Instance.HasKey(keyName);

        if (lockedDoor != null)
            lockedDoor.SetActive(!hasKey); // 沒鑰匙顯示鎖門
        if (unlockedDoor != null)
            unlockedDoor.SetActive(hasKey); // 有鑰匙顯示開門
    }
}
