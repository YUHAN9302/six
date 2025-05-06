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
    public void SavePlayerPosition(int saveSlot, Vector2 playerPosition, List<string> items)
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

        // 取得當前時間
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 合併資料為字串：x,y,time,item1,item2,...
        string data = $"{playerPosition.x},{playerPosition.y},{saveTime},{string.Join(",", items)}";


        File.WriteAllText(filePath, data);
        Debug.Log($"玩家位置已儲存到 {filePath}，時間：{saveTime}");
    }

    public (Vector2, string, List<string>) LoadPlayerPosition(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("存檔編號錯誤！請使用 1~5");
            return (Vector2.zero, "No Data", new List<string>());
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            string[] values = data.Split(',');

            if (values.Length == 8) // x, y, time, item1~5
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                string saveTime = values[2];

                List<string> items = new List<string>();
                for (int i = 3; i < 8; i++)
                {
                    items.Add(values[i]);
                }

                Debug.Log($"載入存檔 {saveSlot}：({x}, {y})，時間：{saveTime}，道具：{string.Join(", ", items)}");
                return (new Vector2(x, y), saveTime, items);
            }
        }

        Debug.LogWarning($"存檔 {saveSlot} 不存在！");
        return (Vector2.zero, "No Data", new List<string>());
    }

}
