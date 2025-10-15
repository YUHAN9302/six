using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 位置紀錄 : MonoBehaviour
{
    // Start is called before the first frame update
    public static 位置紀錄 Instance;

    public static Vector3? LastPosition = null;
    public static string LastAnimState = null; // 新增：最後播放的動畫狀態名稱

    // ✅ 新增：紀錄互動過的物件
    private static HashSet<string> interactedObjects = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // 保留跨場景
        }
        else
        {
            Destroy(gameObject); // 防止重複
        }
    }

    public static void SetPosition(Vector3 pos)
    {
        LastPosition = pos;
    }

    public static Vector3? GetPosition()
    {
        return LastPosition;
    }

    public static void ClearPosition()
    {
        LastPosition = null;
    }
    //記錄動畫
    public static void SetAnimState(string stateName)
    {
        LastAnimState = stateName;
    }

    public static string GetAnimState()
    {
        return LastAnimState;
    }
    // ---------- 互動物件 ----------
    public static void AddInteraction(string name)
    {
        if (!interactedObjects.Contains(name))
            interactedObjects.Add(name);
    }

    public static bool HasInteracted(string name)
    {
        return interactedObjects.Contains(name);
    }

    public static void ResetAll()
    {
        LastPosition = null;
        LastAnimState = null;
        interactedObjects.Clear();
    }
}
