using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 位置紀錄 : MonoBehaviour
{
    // Start is called before the first frame update
    public static 位置紀錄 Instance;

    public static Vector3? LastPosition = null;
    public static string LastAnimState = null; // 新增：最後播放的動畫狀態名稱


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
}
