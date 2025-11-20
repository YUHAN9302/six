using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenItemBox : MonoBehaviour
{
    [Tooltip("5 個圖片欄位（每格的第 0 個子物件會被顯示/隱藏）")]
    public GameObject[] itemImages;

    [Tooltip("對應每格的資訊視窗/說明面板")]
    public GameObject[] itemInfo;

    private const int SLOT_COUNT = 5;

    private void OnEnable()
    {
        Debug.Log($"[OpenItemBox] 當前 SelectID = {SetAndGetSaveData.SelectID}");

        // 如果還沒選擇存檔，就把所有欄位關閉並清空圖示
        if (SetAndGetSaveData.SelectID == 0)
        {
            ToggleAllSlots(false, true);
            return;
        }

        var saveMgr = FindObjectOfType<SaveManager>();
        if (saveMgr == null)
        {
            ToggleAllSlots(false, true);
            Debug.LogWarning("[OpenItemBox] 找不到 SaveManager。");
            return;
        }

        // 讀取玩家狀態：只關心 items 這個 List
        var (_, _, _, items, _) = saveMgr.LoadPlayerState(SetAndGetSaveData.SelectID);
        if (items == null)
            items = new List<string>();

        // 確保有固定 5 格，沒有的用空字串補
        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        // 逐格更新背包顯示
        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            string rawName = (i < items.Count) ? items[i] : "";
            string itemName = rawName == null ? "" : rawName.Trim(); // 去掉前後空白
            bool hasItem = !string.IsNullOrWhiteSpace(itemName);

            // Debug 每一格實際拿到的名稱與長度
            Debug.Log($"[OpenItemBox] slot {i} raw='{rawName}' | trim='{itemName}' | length={itemName.Length}");

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null)
                {
                    icon.SetActive(hasItem);

                    var image = icon.GetComponent<Image>();
                    if (image != null)
                    {
                        if (hasItem)
                        {
                            // 根據名稱載入圖示
                            Sprite itemSprite = LoadIconSpriteForItem(itemName);

                            // 無論有沒有找到，都重新指定 sprite，避免殘留舊圖片
                            image.sprite = itemSprite;

                            if (itemSprite == null)
                            {
                                Debug.LogWarning($"[OpenItemBox] 找不到對應圖片：'{itemName}'，已清除此格圖示");
                            }
                            else
                            {
                                Debug.Log($"[OpenItemBox] slot {i} 成功載入圖示：'{itemName}'");
                            }
                        }
                        else
                        {
                            // 沒物品時，強制清空 sprite，避免出現之前的圖片
                            image.sprite = null;
                        }
                    }
                }
            }
        }

        Debug.Log("[OpenItemBox] 目前道具：" + string.Join(",", items));
    }

    /// <summary>
    /// 點擊某一格背包 icon，開啟對應的資訊視窗
    /// </summary>
    public void OpenItemInfo(int id)
    {
        if (itemImages == null || itemInfo == null) return;
        if (id < 0 || id >= itemImages.Length || id >= itemInfo.Length) return;

        var slot = itemImages[id];
        if (slot == null) return;

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
    /// 開或關全部欄位，同時可選擇是否清空 sprite
    /// </summary>
    private void ToggleAllSlots(bool enable, bool clearSprite = false)
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null)
                {
                    icon.SetActive(enable);

                    if (clearSprite)
                    {
                        var image = icon.GetComponent<Image>();
                        if (image != null)
                            image.sprite = null;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 根據道具名稱載入對應圖示（需放在 Resources/Icons 目錄）
    /// 檔案路徑範例：Assets/Resources/Icons/book.png
    /// </summary>
    private Sprite LoadIconSpriteForItem(string itemName)
    {
        // 先做一次標準化
        string key = itemName.Trim();

        // 嚴格對照（完全一樣）
        switch (key)
        {
            case "書本":
                return Resources.Load<Sprite>("Icons/book");
            case "糖果":
                return Resources.Load<Sprite>("Icons/truecandy");
            case "玩偶":
                return Resources.Load<Sprite>("Icons/doll");
        }

        // 如果嚴格比對失敗，再做「模糊比對」，避免名稱有多加文字
        if (key.Contains("書"))
        {
            Debug.Log($"[OpenItemBox] 使用模糊比對，'{itemName}' 視為 書本");
            return Resources.Load<Sprite>("Icons/book");
        }

        if (key.Contains("糖"))
        {
            Debug.Log($"[OpenItemBox] 使用模糊比對，'{itemName}' 視為 糖果");
            return Resources.Load<Sprite>("Icons/truecandy");
        }

        if (key.Contains("偶"))
        {
            Debug.Log($"[OpenItemBox] 使用模糊比對，'{itemName}' 視為 玩偶");
            return Resources.Load<Sprite>("Icons/doll");
        }

        Debug.LogWarning($"[OpenItemBox] 無法識別的道具名稱：'{itemName}'");
        return null;
    }
}
