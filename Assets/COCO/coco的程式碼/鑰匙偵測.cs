using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 鑰匙偵測 : MonoBehaviour
{
    public static 鑰匙偵測 Instance;
    private HashSet<string> keys = new HashSet<string>(); // 用來儲存已獲得的鑰匙

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 拿到鑰匙
    public void ObtainKey(string keyName)
    {
        keys.Add(keyName);
        Debug.Log($"取得鑰匙：{keyName}");
    }

    // 是否擁有鑰匙
    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }
}
