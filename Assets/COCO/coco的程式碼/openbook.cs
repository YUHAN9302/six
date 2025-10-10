using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openbook : MonoBehaviour
{
    [Header("第二張說明圖")]
    public GameObject objectImage;

    [Header("第一張說明圖（自己）")]
    public GameObject currentImage;

    public void OpenObjectImage()
    {
        if (objectImage != null)
            objectImage.SetActive(true);   // 顯示第二張

        if (currentImage != null)
            currentImage.SetActive(false); // 關閉第一張
    }
}
