using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 登入 : MonoBehaviour
{
    public GameObject START;
    public GameObject Quit;
    Setting[] SettingObjects;
    SetAndGetSaveData[] LoadObjects;

    public GameObject transitionObject; // 轉場動畫的 GameObject（帶有 Animator）
    public float animationDuration = 0.2f;  // 動畫的持續時間（設為動畫長度）


    // Start is called before the first frame update
    void Start()
    {
        SettingObjects = GameObject.FindObjectsOfType<Setting>(true);
        SettingObjects[0].gameObject.SetActive(true);
        SettingObjects[0].gameObject.SetActive(false);

        LoadObjects = GameObject.FindObjectsOfType<SetAndGetSaveData>(true);
        LoadObjects[0].gameObject.SetActive(true);
        LoadObjects[0].gameObject.SetActive(false);

       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void second()
    {
        PlayerController.direction = -1;
        PlayerController.isClothed = false;

        SetAndGetSaveData.SelectID = 0;
        StartCoroutine(PlayAnimationAndLoadScene());

    }

    private IEnumerator PlayAnimationAndLoadScene()
    {
        // 播放動畫
        transitionObject.SetActive(true);

        // 等待動畫播放完成，假設動畫長度為1秒
        yield return new WaitForSeconds(animationDuration);

        // 設置玩家位置（這部分不變）
        FindObjectOfType<SaveManager>().PlayerPos = new Vector2(-7.47f, -1.08f);

        // 切換到場景1
        SceneManager.LoadScene(1);
    }

    public void SettingObj() {
        SettingObjects[0].gameObject.SetActive(true);
    }
    public void LoadObj()
    {
        LoadObjects[0].gameObject.SetActive(true);
    }
}
