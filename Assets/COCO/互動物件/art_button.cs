using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art_button : MonoBehaviour
{
    public GameObject artButton;
    public Animator animator;
    public GameObject bookObject;


    // Start is called before the first frame update
    void Start()
    {
        if (artButton != null)
        {
            artButton.SetActive(false);
        }
        if (bookObject != null)
        {
            bookObject.SetActive(false); // 一開始隱藏書本
        }
    }

    void OnMouseEnter()  // 當滑鼠進入物件時
    {
        if (artButton != null)
        {
            artButton.SetActive(true);  // 顯示按鈕
        }
    }
    void OnMouseExit()  // 當滑鼠離開物件時
    {
        if (artButton != null)
        {
            artButton.SetActive(false);  // 隱藏按鈕
        }
    }
    public void OnViewButtonClicked()  // 當按鈕被點擊時
    {
        Debug.Log("查看畫作！");
        // 你可以在這裡觸發一些邏輯，例如顯示畫作的詳細信息。
        if (animator != null)
        {
            animator.SetTrigger("畫中人掉淚");  // 播放動畫觸發器
        }
        if (bookObject != null)
        {
            bookObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
