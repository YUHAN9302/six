using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [Header("玩家進門位置")]
    public Transform playerSpawnPoint;  // 玩家進門時的門前位置

    [Header("選項")]
    public string doorID = "";          // 若留空會自動生成「場景名稱_物件名稱」
    public GameObject nextDoorToShow;   // 回房間時要出現的新門（可選）
    public GameObject dialogueToShow;   // 回房間時自動開啟的對話物件（可選）

    [Header("轉場等待")]
    public float transitionDuration = 1.5f; // 等待轉場動畫播完的秒數

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (!collision.CompareTag("Player")) return;

        triggered = true;

        //自動生成 doorID
        string id = string.IsNullOrEmpty(doorID)
            ? SceneManager.GetActiveScene().name + "_" + gameObject.name
            : doorID;

        // 存門前位置
        if (playerSpawnPoint != null)
            位置紀錄.SetDoorEntryPosition(id, playerSpawnPoint.position);


        // UI用（永遠更新）
        位置紀錄.LastDoorID = id;

        // 關鍵：只在「第一次進入路徑」記錄入口
        if (string.IsNullOrEmpty(位置紀錄.ReturnDoorID))
        {
            位置紀錄.ReturnDoorID = id;
            Debug.Log("記錄入口門：" + id);
        }

        Debug.Log("最後經過門：" + id);
    }
}
