using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOAD : MonoBehaviour
{
    public GameObject Load;
    public GameObject 暫停介面;

    private bool isLoadActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLoadActive)
        {
            
            Debug.Log("ESC Pressed - Close or Go Back to Game");
            // 這裡可以執行退出遊戲的代碼或其他操作
        }
        else
        {
            // 如果是 "存檔介面"，則返回 "Load 介面"
            ReturnToLoadUI();
        }


    }
    public void SwitchToSaveUI()
    {
        if (Load != null && 暫停介面 != null)
        {

            Load.SetActive(true);
            暫停介面.SetActive(false);
        }
    }
    public void ReturnToLoadUI()
    {
        if (Load != null && 暫停介面 != null)
        {
            Load.SetActive(false);  // Hide Save UI
            暫停介面.SetActive(true); // Show LOADs UI
        }
    }
}
