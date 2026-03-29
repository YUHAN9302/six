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

    [Header("額外門或劇情物件")]
    public GameObject nextDoorOrObject; // 打開後出現的新門/物件

    private string doorID; // 用來唯一識別這個門


    private void Start()
    {
        // 生成門唯一ID
        doorID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_" + gameObject.name;

        // 更新門狀態
        UpdateDoorState();

        // ⭐ 如果門已開過，進房間時顯示新門
        if (位置紀錄.HasInteracted(doorID) && nextDoorOrObject != null)
        {
            nextDoorOrObject.SetActive(true);
        }
    }



    private void OnMouseDown()
    {
        if (鑰匙偵測.Instance.HasKey(keyName))
        {
            Debug.Log($"使用 {keyName} 開門！");

            // ⭐ 記錄這個門已開過
            位置紀錄.AddInteraction(doorID);

            // 如果有新門/物件需要出現
            if (nextDoorOrObject != null)
                nextDoorOrObject.SetActive(true);
        }
        else
        {
            Debug.Log("門鎖著，沒有鑰匙。");
        }

        UpdateDoorState();
    }
    private void UpdateDoorState()
    {
        bool hasKey = 鑰匙偵測.Instance.HasKey(keyName);
        bool alreadyOpened = 位置紀錄.HasInteracted(doorID);

        // 🔹 顯示鎖門或開門
        if (lockedDoor != null)
            lockedDoor.SetActive(!hasKey || alreadyOpened == false);
        if (unlockedDoor != null)
            unlockedDoor.SetActive(hasKey || alreadyOpened);

        // 🔹 如果已開過，下一個門/物件也顯示
        if (alreadyOpened && nextDoorOrObject != null)
            nextDoorOrObject.SetActive(true);
    }
}
