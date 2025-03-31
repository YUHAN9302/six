using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject bookAnimation;  // 書本動畫的 GameObject
    private bool animationPlayed = false;  // 動畫是否播放過



    // Start is called before the first frame update
    void Start()
    {
        if (bookAnimation != null)
        {
            bookAnimation.SetActive(false); // 確保動畫物件一開始是隱藏的
        }
    }

    public void OnMouseDown()
    {
        if (!bookAnimation.activeSelf)
        {
            bookAnimation.SetActive(true);  // 顯示書本動畫
            StartCoroutine(HideAfterSeconds(2f));  // 等待 2秒後隱藏
        }

    }


    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);  // 等待指定時間
        bookAnimation.SetActive(false);  // 隱藏動畫物件
        gameObject.SetActive(false);     // 隱藏書本物件
    }

   


    // Update is called once per frame
    void Update()
    {

    }
}