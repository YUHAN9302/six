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


        if (SetAndGetSaveData.SelectID == 0)
        {
            ToggleAllSlots(false);
            return;
        }
        var saveMgr = FindObjectOfType<SaveManager>();
        if (saveMgr == null)
        {
            ToggleAllSlots(false);
            Debug.LogWarning("[OpenItemBox] 找不到 SaveManager。");
            return;
        }

        var (_, _, _, items, _) = saveMgr.LoadPlayerState(SetAndGetSaveData.SelectID);
        if (items == null)
            items = new List<string>();

        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        for (int i = 0; i < itemImages.Length; i++)
        {
            var slot = itemImages[i];
            if (slot == null) continue;

            string itemName = (i < items.Count) ? items[i] : "";
            bool hasItem = !string.IsNullOrEmpty(itemName);

            if (slot.transform.childCount > 0)
            {
                var icon = slot.transform.GetChild(0).gameObject;
                if (icon != null)
                {
                    icon.SetActive(hasItem);

                    if (hasItem)
                    {
                        var image = icon.GetComponent<Image>();
                        if (image != null)
                        {
                            Sprite itemSprite = LoadIconSpriteForItem(itemName);
                            if (itemSprite != null)
                            {
                                image.sprite = itemSprite;
                            }
                            else
                            {
                                Debug.LogWarning($"找不到對應圖片：{itemName}");
                            }
                        }
                    }
                }
            }
        }

        Debug.Log("[OpenItemBox] 目前道具：" + string.Join(",", items));
    }

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

    /// <summary>
    /// 根據道具名稱載入對應圖示（需放在 Resources/Icons 目錄）
    /// </summary>
    private Sprite LoadIconSpriteForItem(string itemName)
    {
        // 對應道具名稱與圖片檔名
        switch (itemName)
        {
            case "書本":
                return Resources.Load<Sprite>("Icons/book");
            case "粉糖果":
                return Resources.Load<Sprite>("Icons/truecandy");
            case "玩偶":
                return Resources.Load<Sprite>("Icons/doll");
            // 可繼續擴充
            default:
                return null;
        }
    }
}
