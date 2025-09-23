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

    private void OnEnable()
    {
        isClickLoadPage = true;
    }

    private void OnDisable()
    {
        isClickLoadPage = false;
    }

    void Start()
    {
        var saveManager = FindObjectOfType<SaveManager>();

        for (int i = 0; i < 5; i++)
        {
            var (_, _, saveTime, _) = saveManager.LoadPlayerState(i + 1);
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
        var (loadedPosition, moveSpeed, saveTime, _) = saveManager.LoadPlayerState(SelectID);

        // 更新 SaveManager 的快取
        saveManager.PlayerPos = loadedPosition;
        saveManager.PlayerMoveSpeed = moveSpeed;

        // 套用到 Player
        var player = GameObject.Find("Player")?.GetComponent<PlayerController>();
        if (player != null)
        {
            player.transform.position = loadedPosition;
            player.direction = moveSpeed;
            if (player.direction == -1)
            {
                player.GetComponent<Animator>().SetBool("isRight", true);

            }
            if (player.direction == 1)
            {
                player.GetComponent<Animator>().SetBool("isRight", false);

            }
            Debug.Log($"✅ 玩家載入成功：位置 {loadedPosition}，速度 {moveSpeed}，時間 {saveTime}");
        }
        else
        {
            Debug.LogWarning("❌ 場景中沒有找到 Player 或 PlayerController");
        }

        gameObject.SetActive(false);
    }

    // === 存檔：位置 + 速度 ===
    public void SaveDataPos(int ID)
    {
        SelectID = ID;
        SavePlayerPosition(ID);
        Texts[ID - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // === 存檔：道具 ===
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
        float moveSpeed = player.direction;

        var saveManager = FindObjectOfType<SaveManager>();
        var (_, _, _, oldItems) = saveManager.LoadPlayerState(ID);

        saveManager.SavePlayerState(ID, playerPosition, moveSpeed, oldItems);
    }

    private void SaveItemData(int ID, string newItem)
    {
        var saveManager = FindObjectOfType<SaveManager>();
        var (playerPosition, moveSpeed, _, oldItems) = saveManager.LoadPlayerState(ID);

        while (oldItems.Count < 5)
            oldItems.Add("");

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
        if (!added)
            oldItems[oldItems.Count - 1] = newItem;

        saveManager.SavePlayerState(ID, playerPosition, moveSpeed, oldItems);
    }
}
