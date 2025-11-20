using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;

public class SaveManager : MonoBehaviour
{
    private string saveDirectory;

    public Vector2 PlayerPos;
    public float PlayerMoveSpeed;
    public bool PlayerIsClothed; // 紀錄穿著狀態

    private static readonly IFormatProvider F = CultureInfo.InvariantCulture;

    private const int SLOT_COUNT = 5;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        saveDirectory = Path.Combine(Application.streamingAssetsPath, "SaveFiles");
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    // =============================
    // 存檔：完整覆寫指定存檔槽
    // =============================
    public void SavePlayerState(int saveSlot, Vector2 playerPosition, float moveSpeed, List<string> items, bool isClothed)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return;
        }

        if (items == null)
            items = new List<string>();

        // 確保剛好 5 格
        // 如果超過就只存前 5 個，如果不足就補空字串
        if (items.Count > SLOT_COUNT)
            items = items.GetRange(0, SLOT_COUNT);

        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 格式：x,y,moveSpeed,isClothed,time,item1,item2,item3,item4,item5 → 共 10 欄
        string data =
            string.Format(F, "{0},{1},{2},{3},{4},{5}",
                playerPosition.x.ToString(F),
                playerPosition.y.ToString(F),
                moveSpeed.ToString(F),
                isClothed ? "1" : "0",
                saveTime,
                string.Join(",", items)
            );

        File.WriteAllText(filePath, data);

        Debug.Log($"✅ 玩家狀態已儲存到 {filePath}\n時間：{saveTime}，速度：{moveSpeed}，isClothed={isClothed}\n道具：{string.Join(",", items)}");
    }

    // =============================
    // 讀檔：回傳 (位置, 速度, 時間字串, 道具清單, isClothed)
    // =============================
    public (Vector2 pos, float moveSpeed, string time, List<string> items, bool isClothed) LoadPlayerState(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return (Vector2.zero, 3f, "No Data", CreateEmptyItems(), false);
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"存檔 {saveSlot} 不存在！");
            return (Vector2.zero, 3f, "No Data", CreateEmptyItems(), false);
        }

        string data = File.ReadAllText(filePath);
        string[] values = data.Split(',');

        float x = 0f, y = 0f, moveSpeed = 3f;
        bool isClothed = false;
        string saveTime = "No Data";
        var items = new List<string>();
        int itemStartIndex = 0;

        try
        {
            if (values.Length >= 8)
            {
                x = float.Parse(values[0], F);
                y = float.Parse(values[1], F);

                if (values.Length >= 10)
                {
                    // 最新格式：x,y,moveSpeed,isClothed,time,item1~5
                    moveSpeed = float.Parse(values[2], F);
                    isClothed = values[3] == "1";
                    saveTime = values[4];
                    itemStartIndex = 5;
                }
                else if (values.Length == 9)
                {
                    // 只有 moveSpeed，沒有 isClothed
                    moveSpeed = float.Parse(values[2], F);
                    saveTime = values[3];
                    itemStartIndex = 4;
                }
                else if (values.Length == 8)
                {
                    // 最舊格式：x,y,time,item1~5
                    saveTime = values[2];
                    itemStartIndex = 3;
                }

                // 只讀取最多 5 個道具欄位，多的直接忽略
                for (int i = itemStartIndex; i < values.Length && items.Count < SLOT_COUNT; i++)
                {
                    items.Add(values[i]);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ 載入存檔 {saveSlot} 失敗：{ex.Message}");
        }

        // 確保剛好 5 格
        if (items.Count > SLOT_COUNT)
            items = items.GetRange(0, SLOT_COUNT);

        while (items.Count < SLOT_COUNT)
            items.Add(string.Empty);

        var pos = new Vector2(x, y);
        PlayerPos = pos;
        PlayerMoveSpeed = moveSpeed;
        PlayerIsClothed = isClothed;

        Debug.Log($"📂 載入存檔 {saveSlot}：({x}, {y})，速度：{moveSpeed}，isClothed={isClothed}，時間：{saveTime}\n道具：{string.Join(",", items)}");

        return (pos, moveSpeed, saveTime, items, isClothed);
    }

    // =============================
    // 建立「空背包」的工具函式
    // =============================
    private List<string> CreateEmptyItems()
    {
        var list = new List<string>();
        for (int i = 0; i < SLOT_COUNT; i++)
            list.Add(string.Empty);
        return list;
    }

    // =============================
    // 清空指定存檔槽的道具（位置與時間保留）
    // 在「新遊戲但沿用同一個存檔槽」時可使用
    // =============================
    public void ClearItemsInSave(int saveSlot)
    {
        var (pos, moveSpeed, time, items, isClothed) = LoadPlayerState(saveSlot);
        items = CreateEmptyItems(); // 全部清空
        SavePlayerState(saveSlot, pos, moveSpeed, items, isClothed);
        Debug.Log($"🧹 已清空存檔 {saveSlot} 的所有道具");
    }

    // =============================
    // 完全重置存檔槽：刪除檔案
    // 若是「真正的新遊戲」也可以用這個
    // =============================
    public void DeleteSaveSlot(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5) return;

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"🗑️ 已刪除存檔 {saveSlot}");
        }
    }

    // =============================
    // 建立一個「全新的遊戲存檔」
    // 開新遊戲時可呼叫這個，確保背包是空的
    // =============================
    public void CreateNewGameSave(int saveSlot, Vector2 startPos, float defaultMoveSpeed, bool defaultClothed = false)
    {
        var emptyItems = CreateEmptyItems();
        SavePlayerState(saveSlot, startPos, defaultMoveSpeed, emptyItems, defaultClothed);
        Debug.Log($"✨ 新遊戲存檔已建立：槽位 {saveSlot}，起始位置 {startPos}，速度 {defaultMoveSpeed}");
    }
}
