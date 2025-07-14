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
    string newItem;
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

    public void SaveDataPos(int iD)
    {
        SelectID = iD;

        SavePlayerPosition(iD);
        Texts[iD - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public void SaveDataIteam(int ID, string newItem)
    {
        SaveItemData(ID, newItem);
        Texts[ID - 1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    }

    private void SavePlayerPosition(int ID)
    {
        Vector2 playerPosition = GameObject.Find("Player").transform.position;
        var saveManager = FindObjectOfType<SaveManager>();
        var (_, _, oldItems) = saveManager.LoadPlayerPosition(ID);
        saveManager.SavePlayerPosition(ID, playerPosition, oldItems);
    }

    private void SaveItemData(int ID, string newItem)
    {
        var saveManager = FindObjectOfType<SaveManager>();

        // 取得原始資料
        var (playerPosition, _, oldItems) = saveManager.LoadPlayerPosition(ID);

        // 確保道具欄位長度為 5
        while (oldItems.Count < 5)
            oldItems.Add("");

        // 新增道具
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

        // 如果都滿了，就覆蓋最後一格
        if (!added)
            oldItems[oldItems.Count - 1] = newItem;

        // 儲存時保留原本位置、不更新位置
        saveManager.SavePlayerPosition(ID, playerPosition, oldItems);
    }
}
