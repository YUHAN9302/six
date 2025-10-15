using System.Collections;
using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("書本動畫物件")]
    public GameObject bookAnimation;

    [Header("動畫持續秒數 (0 表示永久顯示)")]
    public float animationDuration = 2f;

    [Header("道具名稱（用於存檔）")]
    public string bookName = "書本";

    private bool isCollected = false;

    void Start()
    {
        if (bookAnimation != null)
            bookAnimation.SetActive(false);
    }

    public void OnMouseUp()
    {
        if (isCollected) return;
        isCollected = true;

        // 顯示動畫
        if (bookAnimation != null)
        {
            bookAnimation.SetActive(true);
            if (animationDuration > 0)
                StartCoroutine(HideAfterSeconds(animationDuration));
        }

        // 自動儲存：玩家位置 + 道具
        int saveID;
        if (SetAndGetSaveData.SelectID != 0)
        {
             saveID = SetAndGetSaveData.SelectID;
        }
        else {
            SetAndGetSaveData.SelectID = 1;
             saveID = SetAndGetSaveData.SelectID;

        }
        if (saveID > 0)
        {
            var saveSystem = FindObjectOfType<SetAndGetSaveData>();
            if (saveSystem != null)
            {
                saveSystem.SaveDataPos(saveID);               // 儲存位置
                saveSystem.SaveDataItem(saveID, bookName);    // 儲存道具
                Debug.Log($"📘 書本《{bookName}》已自動儲存到存檔 {saveID}");
            }
        }
        else
        {
            Debug.LogWarning("❌ 尚未選擇存檔，無法自動儲存書本");
        }

        // 隱藏書本物件本體
        gameObject.SetActive(false);
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (bookAnimation != null)
            bookAnimation.SetActive(false);
    }
}
