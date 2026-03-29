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

        // 標記這扇門已開
        位置紀錄.AddInteraction(id);

        Debug.Log($"記錄門前位置: {id} -> {playerSpawnPoint.position}");
    }

    private void Start()
    {
        string id = string.IsNullOrEmpty(doorID)
            ? SceneManager.GetActiveScene().name + "_" + gameObject.name
            : doorID;

        // 回房間時顯示新門
        if (nextDoorToShow != null)
        {
            nextDoorToShow.SetActive(位置紀錄.HasInteracted(id));
        }

        // 回房間時顯示對話物件（等待轉場動畫播完）
        if (dialogueToShow != null && 位置紀錄.HasInteracted(id))
        {
            StartCoroutine(ShowDialogueAfterTransition());
        }
    }
    private IEnumerator ShowDialogueAfterTransition()
    {
        // 等待轉場動畫時間
        yield return new WaitForSecondsRealtime(transitionDuration);

        // 顯示對話
        dialogueToShow.SetActive(true);
    }
}
