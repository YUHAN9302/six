using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SetAndGetSaveData : MonoBehaviour
{
    public TextMeshProUGUI[] Texts;
    static public bool isClickLaodPage;
    static public int SelectID;
    private void Awake()
    {
        if (Application.loadedLevelName == "開始介面")
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    private void OnEnable()
    {
        isClickLaodPage = true;

    }
    private void OnDisable()
    {
        isClickLaodPage = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            var (loadedPosition, saveTime, _) = FindObjectOfType<SaveManager>().LoadPlayerPosition(i + 1);
            Texts[i].text = saveTime;

            if (Application.loadedLevelName == "開始介面")
            {
                bool hasData = saveTime != "No Data";
                Texts[i].GetComponentInParent<Button>().interactable = hasData;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadData(int ID)
    {
        SelectID = ID;
        Application.LoadLevel("0228");
        var (loadedPosition, saveTime, _) = FindObjectOfType<SaveManager>().LoadPlayerPosition(ID);
        FindObjectOfType<SaveManager>().PlayerPos = loadedPosition;
        Debug.Log($"玩家位置載入成功：{loadedPosition}，存檔時間：{saveTime}");
        gameObject.SetActive(false);
    }

    public void SaveData(int ID, string newItem)
    {
        // 取得目前玩家位置
        Vector2 playerPosition = GameObject.Find("Player").transform.position;

        // 先讀取舊的資料
        var (_, _, oldItems) = FindObjectOfType<SaveManager>().LoadPlayerPosition(ID);

        // 確保 list 長度為 5（不足補空字串）
        while (oldItems.Count < 5)
        {
            oldItems.Add("");
        }

        // 將新的道具放到第一個空位（若滿了你可以選擇替換最後一個）
        for (int i = 0; i < oldItems.Count; i++)
        {
            if (string.IsNullOrEmpty(oldItems[i]))
            {
                oldItems[i] = newItem;
                break;
            }
        }

        // 儲存新資料
        FindObjectOfType<SaveManager>().SavePlayerPosition(ID, playerPosition, oldItems);

        // 更新 UI 時間
        Texts[ID - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }


}
