using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 暫停 : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    private 人物走路程式 playerMovement;
    Setting[] SettingObjects;
    SetAndGetSaveData[] LoadObjects;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<人物走路程式>(); // 找到角色的移動腳本
        pauseMenu.SetActive(false);
        SettingObjects = GameObject.FindObjectsOfType<Setting>(true);
        LoadObjects = GameObject.FindObjectsOfType<SetAndGetSaveData>(true);
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()//後加的
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f; // 暫停遊戲
            if (playerMovement != null)
                playerMovement.canMove = false; // 停止角色移動
        }
        else
        {
            Time.timeScale = 1f; // 恢復遊戲
            if (playerMovement != null)
                playerMovement.canMove = true; // 恢復角色移動
        }
    }

    public void GameQuit()
    {
        SceneManager.LoadScene(1);
    }
    public void second()
    {
        Time.timeScale = 1f; // 確保時間恢復正常
        SceneManager.LoadScene(1);
    }
    public void SettingObj()
    {
        TogglePause();
        SettingObjects[0].gameObject.SetActive(true);
    }
    public void LoadObj()
    {
        TogglePause();
        LoadObjects[0].gameObject.SetActive(true);
    }
}
