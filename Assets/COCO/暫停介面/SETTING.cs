using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SETTING : MonoBehaviour
{
    public GameObject setting;
    public GameObject 暫停介面;

    private bool isSettingActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSettingActive)
        {
            
            Debug.Log("ESC Pressed - Close or Go Back to Game");
            // 這裡可以執行退出遊戲的代碼或其他操作
        }
        else
        {
            // 如果是 "存檔介面"，則返回 "SETTING 介面"
            ReturnToSettingUI();
        }
    }
    public void SwitchToSaveUI()
    {
        if (setting != null && 暫停介面 != null)
        {

            setting.SetActive(true);
            暫停介面.SetActive(false);
        }
    }
    public void ReturnToSettingUI()
    {
        if (setting != null && 暫停介面 != null)
        {
            setting.SetActive(false);  // Hide Save UI
            暫停介面.SetActive(true); // Show Settings UI
        }
    }
}
