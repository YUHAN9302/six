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
    public bool PlayerIsClothed; // ⭐ 新增：紀錄穿著狀態

    private static readonly IFormatProvider F = CultureInfo.InvariantCulture;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        saveDirectory = Path.Combine(Application.streamingAssetsPath, "SaveFiles");
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    // === 存檔 ===
    // 新增 isClothed 參數
    public void SavePlayerState(int saveSlot, Vector2 playerPosition, float moveSpeed, List<string> items, bool isClothed)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return;
        }
        if (items == null || items.Count != 5)
        {
            Debug.LogWarning("請提供 5 個道具資訊！");
            return;
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 新格式：x,y,moveSpeed,isClothed,time,item1~5 → 共 10 欄
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
        Debug.Log($"✅ 玩家狀態已儲存到 {filePath}\n時間：{saveTime}，速度：{moveSpeed}，isClothed={isClothed}");
    }

    // === 讀檔：回傳 (位置, 速度, 時間字串, 道具清單, isClothed) ===
    public (Vector2 pos, float moveSpeed, string time, List<string> items, bool isClothed) LoadPlayerState(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return (Vector2.zero, 3f, "No Data", new List<string>(), false);
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"存檔 {saveSlot} 不存在！");
            return (Vector2.zero, 3f, "No Data", new List<string>(), false);
        }

        string data = File.ReadAllText(filePath);
        string[] values = data.Split(',');

        // 新格式：x,y,moveSpeed,isClothed,time,item1~5 → 10 欄
        // 舊格式：x,y,moveSpeed,time,item1~5 → 9 欄（沒有 isClothed）
        // 更舊：x,y,time,item1~5 → 8 欄（沒有 moveSpeed, isClothed）
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
                    // 最新格式
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
                    // 最舊格式
                    saveTime = values[2];
                    itemStartIndex = 3;
                }

                for (int i = itemStartIndex; i < values.Length; i++)
                    items.Add(values[i]);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ 載入存檔 {saveSlot} 失敗：{ex.Message}");
        }

        var pos = new Vector2(x, y);
        PlayerPos = pos;
        PlayerMoveSpeed = moveSpeed;
        PlayerIsClothed = isClothed;

        Debug.Log($"📂 載入存檔 {saveSlot}：({x}, {y})，速度：{moveSpeed}，isClothed={isClothed}，時間：{saveTime}");
        return (pos, moveSpeed, saveTime, items, isClothed);
    }
}
