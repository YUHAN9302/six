using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 轉場_長 : MonoBehaviour
{
    public GameObject eyeCloseObject; // 閉眼動畫的 Animator
    public float animationDuration = 2.4f; // 動畫長度（秒）

    // Start is called before the first frame update
    void Start()
    {
        StartSequence();
    }
    private void StartSequence()
    {
        if (eyeCloseObject != null)
        {
            eyeCloseObject.SetActive(true); // 開啟動畫物件
            StartCoroutine(HideAfterAnimation());
        }

        // 可選：禁用此腳本避免後續執行
        enabled = false;
    }
    private IEnumerator HideAfterAnimation()
    {
        // 等待動畫播放完畢
        yield return new WaitForSeconds(2.4f); // 這裡填寫你的動畫時間 (秒)

        eyeCloseObject.SetActive(false); // 隱藏動畫物件
    }

    // Update is called once per frame
    void Update()
    {

    }
}
