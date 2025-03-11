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
        // �]�w�s�ɸ�Ƨ�
        saveDirectory = Path.Combine(Application.streamingAssetsPath, "SaveFiles");

        // �p�G��Ƨ����s�b�A�h�إ�
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    // �x�s���a��m����w���s�� (1~5)
    public void SavePlayerPosition(int saveSlot, Vector2 playerPosition)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("�s�ɽs�����~�I�Шϥ� 1~5");
            return;
        }

        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");

        // ���o��e�ɶ�
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // �榡�Gx,y,�ɶ�
        string data = $"{playerPosition.x},{playerPosition.y},{saveTime}";

        File.WriteAllText(filePath, data);
        Debug.Log($"���a��m�w�x�s�� {filePath}�A�ɶ��G{saveTime}");
    }

    // Ū���s�ɪ����a��m�P�ɶ�
    public (Vector2, string) LoadPlayerPosition(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("�s�ɽs�����~�I�Шϥ� 1~5");
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

                Debug.Log($"���J�s�� {saveSlot}�G({x}, {y})�A�ɶ��G{saveTime}");
                return (new Vector2(x, y), saveTime);
            }
        }

        Debug.LogWarning($"�s�� {saveSlot} ���s�b�I");
        return (Vector2.zero, "No Data");
    }
}
