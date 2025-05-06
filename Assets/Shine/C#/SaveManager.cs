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
    public void SavePlayerPosition(int saveSlot, Vector2 playerPosition, List<string> items)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("�s�ɽs�����~�I�Шϥ� 1~5");
            return;
        }
        if (items == null || items.Count != 5)
        {
            Debug.LogWarning("�д��� 5 �ӹD���T�I");
            return;
        }
        string filePath = Path.Combine(saveDirectory, $"save{saveSlot}.txt");

        // ���o��e�ɶ�
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // �X�ָ�Ƭ��r��Gx,y,time,item1,item2,...
        string data = $"{playerPosition.x},{playerPosition.y},{saveTime},{string.Join(",", items)}";


        File.WriteAllText(filePath, data);
        Debug.Log($"���a��m�w�x�s�� {filePath}�A�ɶ��G{saveTime}");
    }

    public (Vector2, string, List<string>) LoadPlayerPosition(int saveSlot)
    {
        if (saveSlot < 1 || saveSlot > 5)
        {
            Debug.LogWarning("�s�ɽs�����~�I�Шϥ� 1~5");
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

                Debug.Log($"���J�s�� {saveSlot}�G({x}, {y})�A�ɶ��G{saveTime}�A�D��G{string.Join(", ", items)}");
                return (new Vector2(x, y), saveTime, items);
            }
        }

        Debug.LogWarning($"�s�� {saveSlot} ���s�b�I");
        return (Vector2.zero, "No Data", new List<string>());
    }

}
