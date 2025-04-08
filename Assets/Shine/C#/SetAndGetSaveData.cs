using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SetAndGetSaveData : MonoBehaviour
{
    public TextMeshProUGUI[] Texts;
    private void Awake()
    {
        if(Application.loadedLevelName== "開始介面")
        DontDestroyOnLoad(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 5; i++) {
            var (loadedPosition, saveTime) = FindObjectOfType<SaveManager>().LoadPlayerPosition(i+1);
            Texts[i].text = saveTime;
            if (Application.loadedLevelName == "開始介面")
            {
                if (Texts[i].text == "No Data")
                {
                    Texts[i].GetComponentInParent<Button>().interactable = false;
                }
                else
                {
                    Texts[i].GetComponentInParent<Button>().interactable = true;

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadData(int ID) {
        Application.LoadLevel("0228");
        var (loadedPosition, saveTime) = FindObjectOfType<SaveManager>().LoadPlayerPosition(ID);
        FindObjectOfType<SaveManager>().PlayerPos = loadedPosition;
        Debug.Log($"玩家位置載入成功：{loadedPosition}，存檔時間：{saveTime}");
        gameObject.SetActive(false);
    }
    public void SaveData(int ID)
    {
        Vector2 playerPosition = GameObject.Find("Player").transform.position;
        FindObjectOfType<SaveManager>().SavePlayerPosition(ID, playerPosition);
        Texts[ID-1].text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
