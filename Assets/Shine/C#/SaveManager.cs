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
    public float PlayerMoveSpeed; // ⭐ 新增：紀錄移動速度

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
    public void SavePlayerState(int saveSlot, Vector2 playerPosition, float moveSpeed, List<string> items)
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

        // 新格式：x,y,moveSpeed,time,item1~5 → 共 9 欄
        string data =
            string.Format(F, "{0},{1},{2},{3},{4}",
                playerPosition.x.ToString(F),
                playerPosition.y.ToString(F),
                moveSpeed.ToString(F),
                saveTime,
                string.Join(",", items)
            );

        File.WriteAllText(filePath, data);
        Debug.Log($"✅ 玩家狀態已儲存到 {filePath}，時間：{saveTime}，速度：{moveSpeed}");
    }

    // === 讀檔：回傳 (位置, 速度, 時間字串, 道具清單) ===
    public (Vector2 pos, float moveSpeed, string time, List<string> items) LoadPlayerState(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return (Vector2.zero, 3f, "No Data", new List<string>()); // 預設速度給 3
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"存檔 {saveSlot} 不存在！");
            return (Vector2.zero, 3f, "No Data", new List<string>());
        }

        string data = File.ReadAllText(filePath);
        string[] values = data.Split(',');

        // 新格式：x,y,moveSpeed,time,item1~5 → 9 欄
        // 舊格式：x,y,time,item1~5        → 8 欄（沒有速度）
        if (values.Length == 9 || values.Length == 8)
        {
            float x = float.Parse(values[0], F);
            float y = float.Parse(values[1], F);

            float moveSpeed = 3f; // 預設速度
            string saveTime;
            int itemStartIndex;

            if (values.Length == 9)
            {
                moveSpeed = float.Parse(values[2], F);
                saveTime = values[3];
                itemStartIndex = 4;
            }
            else
            {
                saveTime = values[2];
                itemStartIndex = 3;
            }

            var items = new List<string>();
            for (int i = itemStartIndex; i < values.Length; i++)
                items.Add(values[i]);

            var pos = new Vector2(x, y);
            PlayerPos = pos;
            PlayerMoveSpeed = moveSpeed;

            Debug.Log($"📂 載入存檔 {saveSlot}：({x}, {y})，速度：{moveSpeed}，時間：{saveTime}，道具：{string.Join(", ", items)}");
            return (pos, moveSpeed, saveTime, items);
        }

        Debug.LogWarning($"存檔 {saveSlot} 欄位數不正確（{values.Length}）。");
        return (Vector2.zero, 3f, "No Data", new List<string>());
    }
}
