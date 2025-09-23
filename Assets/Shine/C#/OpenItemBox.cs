using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItemBox : MonoBehaviour
{
    [Tooltip("5 個圖片欄位（每格的第 0 個子物件會被顯示/隱藏）")]
    public GameObject[] itemImages;   // 5 格 UI 容器
    [Tooltip("對應每格的資訊視窗/說明面板")]
    public GameObject[] itemInfo;     // 5 個資訊面板

    private const int SLOT_COUNT = 5;

    private void OnEnable()
    {
        // 基本防呆
        if (itemImages == null || itemImages.Length == 0)
            return;

        // 未選擇存檔：全部關閉
        if (SetAndGetSaveData.SelectID == 0)
        {
            ToggleAllSlots(false);
            return;
        }

        // 讀取對應存檔的道具
        var saveMgr = FindObjectOfType<SaveManager>();
        if (saveMgr == null)
        {
            ToggleAllSlots(false);
            Debug.LogWarning("[OpenItemBox] 找不到 SaveManager。");
            return;
        }

        // 新版 API：位置、角度、時間、道具
        var (_, _, _, items) = saveMgr.LoadPlayerState(SetAndGetSaveData.SelectID);

        if (items == null)
            items = new List<string>();

        // 確保長度至少 5（不足補空字串）
        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        // 逐格更新顯示
        for (int i = 0; i < itemImages.Length; i++)
        {
            // 如果 UI 陣列比 5 多或少，也不會爆
            bool hasItem = (i < items.Count) && !string.IsNullOrEmpty(items[i]);

            var slot = itemImages[i];
            if (slot == null) continue;

            // 第 0 個子物件視為「物品圖示」
            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null)
                    icon.SetActive(hasItem);
            }
        }
    }

    /// <summary>
    /// 點擊某格，若該格有物品則打開對應的資訊面板
    /// </summary>
    public void OpenItemInfo(int id)
    {
        if (itemImages == null || itemInfo == null) return;
        if (id < 0 || id >= itemImages.Length || id >= itemInfo.Length) return;

        var slot = itemImages[id];
        if (slot == null) return;

        // 檢查第 0 個子物件（物品圖示）目前是否顯示
        if (slot.transform.childCount > 0)
        {
            var icon = slot.transform.GetChild(0).gameObject;
            if (icon != null && icon.activeSelf)
            {
                if (itemInfo[id] != null)
                    itemInfo[id].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 把所有格子的圖示關閉（或打開）
    /// </summary>
    private void ToggleAllSlots(bool enable)
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null) icon.SetActive(enable);
            }
        }
    }
}
