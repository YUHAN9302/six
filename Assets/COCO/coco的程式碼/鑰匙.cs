using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 鑰匙 : MonoBehaviour
{
    public string keyName; // 在 Inspector 輸入鑰匙名稱（例如 "RoomKey"）

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            鑰匙偵測.Instance.ObtainKey(keyName);
            Debug.Log($"玩家撿到 {keyName}");
        }
    }
}
