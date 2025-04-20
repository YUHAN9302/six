using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject windowAnimation;  // 書本動畫的 GameObject
    private bool animationPlayed = false;  // 動畫是否播放過
    // Start is called before the first frame update
    void Start()
    {
        if (windowAnimation != null)
        {
            windowAnimation.SetActive(false); // 確保動畫物件一開始是隱藏的
        }
    }

    public void OnMouseUp()
    {
        if (!windowAnimation.activeSelf)
        {
            windowAnimation.SetActive(true);  // 顯示書本動畫

            Destroy(this);
        }

    }
   
    // Update is called once per frame
    void Update()
    {

    }
}
