using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 儲存管理 : MonoBehaviour
{
    public static 儲存管理 Instance;

    // 是否點擊過糖果 (重開遊戲會重置)
    public bool hasClickedCandy = false;

    // 是否已播放糖果回客廳劇情
    public bool hasPlayedCandyReturnDialogue = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨場景不會消失
        }
        else
        {
            Destroy(gameObject); // 避免重複
        }
    }
}
