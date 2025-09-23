using System.Collections;
using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("書本動畫物件")]
    public GameObject bookAnimation;

    [Header("動畫持續秒數 (0 表示永久顯示)")]
    public float animationDuration = 2f;

    private bool isCollected = false;

    void Start()
    {
        if (bookAnimation != null)
        {
            bookAnimation.SetActive(false); // 開始時隱藏動畫
        }
    }

    // 玩家點擊（或觸發）時撿起書本
    public void OnMouseUp()
    {
        if (isCollected) return; // 避免重複觸發
        isCollected = true;

        // 顯示動畫
        if (bookAnimation != null)
        {
            bookAnimation.SetActive(true);

            if (animationDuration > 0)
            {
                StartCoroutine(HideAfterSeconds(animationDuration));
            }
        }

        // 存到目前選擇的檔案
        int saveID = SetAndGetSaveData.SelectID;
        if (saveID > 0)
        {
            var saveSystem = FindObjectOfType<SetAndGetSaveData>();
            if (saveSystem != null)
            {
                saveSystem.SaveDataItem(saveID, "書本");
                Debug.Log($"📘 書本已加入存檔 {saveID}");
            }
        }
        else
        {
            Debug.LogWarning("❌ 沒有選擇有效存檔，書本不會被保存");
        }

        // 隱藏書本本體
        gameObject.SetActive(false);
    }

    // 動畫顯示一段時間後自動隱藏
    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (bookAnimation != null)
            bookAnimation.SetActive(false);
    }
}
