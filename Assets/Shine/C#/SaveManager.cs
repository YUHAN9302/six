using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class SaveManager : MonoBehaviour
{
    private string saveDirectory;
    public Vector2 PlayerPos;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // 設定存檔資料夾
        saveDirectory = Path.Combine(Application.streamingAssetsPath, "SaveFiles");

        // 如果資料夾不存在，則建立
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    // 儲存玩家位置到指定的存檔 (1~5)
    public void SavePlayerPosition(int saveSlot, Vector2 playerPosition)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return;
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");

        // 取得當前時間
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 格式：x,y,時間
        string data = $"{playerPosition.x},{playerPosition.y},{saveTime}";

        File.WriteAllText(filePath, data);
        Debug.Log($"玩家位置已儲存到 {filePath}，時間：{saveTime}");
    }

    // 讀取存檔的玩家位置與時間
    public (Vector2, string) LoadPlayerPosition(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return (Vector2.zero, "No Data");
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            string[] values = data.Split(',');

            if (values.Length == 3)
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                string saveTime = values[2];

                Debug.Log($"載入存檔 {saveSlot}：({x}, {y})，時間：{saveTime}");
                return (new Vector2(x, y), saveTime);
            }
        }

        Debug.LogWarning($"存檔 {saveSlot} 不存在！");
        return (Vector2.zero, "No Data");
    }
}
