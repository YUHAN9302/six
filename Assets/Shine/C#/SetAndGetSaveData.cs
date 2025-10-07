using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SetAndGetSaveData : MonoBehaviour
{
    [Header("UI 顯示每個存檔時間")]
    public TextMeshProUGUI[] Texts;

    public static bool isClickLoadPage;
    public static int SelectID;

    private void Awake()
    {
        // 在開始介面保留此物件
        if (SceneManager.GetActiveScene().name == "開始介面")
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable() { isClickLoadPage = true; }
    private void OnDisable() { isClickLoadPage = false; }

    void Start()
    {
        var saveManager = FindObjectOfType<SaveManager>();

        for (int i = 0; i < 5; i++)
        {
            // 這裡只需要時間字串，不涉 isClothed
            var (_, _, saveTime, _, _) = saveManager.LoadPlayerState(i + 1);
            Texts[i].text = saveTime;

            if (SceneManager.GetActiveScene().name == "開始介面")
            {
                bool hasData = saveTime != "No Data";
                Texts[i].GetComponentInParent<Button>().interactable = hasData;
            }
        }
    }

    // === 讀檔並切場景 ===
    public void LoadData(int ID)
    {
        SelectID = ID;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("0228");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "0228") return;

        SceneManager.sceneLoaded -= OnSceneLoaded; // 只執行一次

        var saveManager = FindObjectOfType<SaveManager>();
        // 讀出 isClothed
        var (loadedPosition, moveSpeed, saveTime, _, isClothed) = saveManager.LoadPlayerState(SelectID);

        // 更新 SaveManager 的快取
        saveManager.PlayerPos = loadedPosition;
        saveManager.PlayerMoveSpeed = moveSpeed;

        // 套用到 Player
        var player = GameObject.Find("Player")?.GetComponent<PlayerController>();
        if (player != null)
        {
            player.transform.position = loadedPosition;
            PlayerController.direction = moveSpeed;

            // 方向動畫（原有邏輯）
            if (PlayerController.direction == -1)
                player.GetComponent<Animator>().SetBool("isRight", true);
            if (PlayerController.direction == 1)
                player.GetComponent<Animator>().SetBool("isRight", false);

            // 套用穿著狀態
            ApplyIsClothedToPlayer(player, isClothed);

            Debug.Log($"✅ 玩家載入成功：位置 {loadedPosition}，速度 {moveSpeed}，isClothed={isClothed}，時間 {saveTime}");
        }
        else
        {
            Debug.LogWarning("❌ 場景中沒有找到 Player 或 PlayerController");
        }

        gameObject.SetActive(false);
    }

    // === 存檔：位置 + 速度 + 穿著狀態 ===
    public void SaveDataPos(int ID)
    {
        SelectID = ID;
        SavePlayerPosition(ID);
        Texts[ID - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // === 存檔：道具（保留既有 isClothed）===
    public void SaveDataItem(int ID, string newItem)
    {
        SaveItemData(ID, newItem);
        Texts[ID - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private void SavePlayerPosition(int ID)
    {
        var player = GameObject.Find("Player")?.GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogWarning("❌ 找不到 Player 或 PlayerController，無法存檔");
            return;
        }

        Vector2 playerPosition = player.transform.position;
        float moveSpeed = PlayerController.direction;

        var saveManager = FindObjectOfType<SaveManager>();
        // 取出既有清單與 isClothed（避免覆蓋）
        var (_, _, _, oldItems, oldIsClothed) = saveManager.LoadPlayerState(ID);

        // 目前玩家的 isClothed 以場上為準
        bool isClothed = ReadIsClothedFromPlayer(player, oldIsClothed);

        // ⬇⬇ 這裡多傳 isClothed
        saveManager.SavePlayerState(ID, playerPosition, moveSpeed, oldItems, isClothed);
    }

    private void SaveItemData(int ID, string newItem)
    {
        var saveManager = FindObjectOfType<SaveManager>();
        // 讀出既有 isClothed，一起回存
        var (playerPosition, moveSpeed, _, oldItems, isClothed) = saveManager.LoadPlayerState(ID);

        while (oldItems.Count < 5) oldItems.Add("");

        bool added = false;
        for (int i = 0; i < oldItems.Count; i++)
        {
            if (string.IsNullOrEmpty(oldItems[i]))
            {
                oldItems[i] = newItem;
                added = true;
                break;
            }
        }
        if (!added) oldItems[oldItems.Count - 1] = newItem;

        // ⬇⬇ 保留 isClothed
        saveManager.SavePlayerState(ID, playerPosition, moveSpeed, oldItems, isClothed);
    }

    // ====== 小工具：讀取/套用 isClothed ======

    /// <summary>
    /// 從 Player 讀取 isClothed；若沒有可讀來源，回傳 fallback。
    /// </summary>
    private bool ReadIsClothedFromPlayer(PlayerController player, bool fallback)
    {
        // 1) 若 PlayerController 本身就有 public bool isClothed
        var pcType = player.GetType();
        var field = pcType.GetField("isClothed");
        if (field != null && field.FieldType == typeof(bool))
            return (bool)field.GetValue(player);

        // 2) 或者 Animator 參數有 "isClothed"
        var ani = player.GetComponent<Animator>();
        if (ani != null)
        {
            try
            {
                return ani.GetBool("isClothed");
            }
            catch { /* Animator 沒有該參數就走 fallback */ }
        }

        // 3) 都沒有 → 使用舊存檔值
        return fallback;
    }

    /// <summary>
    /// 把 isClothed 套用回玩家（優先寫入 Animator 的 "isClothed"，否則嘗試 PlayerController 的欄位）
    /// </summary>
    private void ApplyIsClothedToPlayer(PlayerController player, bool isClothed)
    {
        var ani = player.GetComponent<Animator>();
        if (ani != null)
        {
            try { ani.SetBool("isClothed", isClothed); return; } catch { }
        }

        var pcType = player.GetType();
        var field = pcType.GetField("isClothed");
        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(player, isClothed);
        }
    }
}
