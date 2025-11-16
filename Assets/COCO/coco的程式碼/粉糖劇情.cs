using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 粉糖劇情 : MonoBehaviour
{
    [Header("糖果返回劇情 UI")]
    public GameObject dialogueUI;

    void Start()
    {
        // 檢查玩家是否擁有糖果（用 PlayerPrefs 標誌）
        if (PlayerPrefs.GetInt("HasCandy", 0) == 1)
        {
            if (dialogueUI != null)
                dialogueUI.SetActive(true);

            // 如果希望只播放一次，可取消下一行註解：
            // PlayerPrefs.SetInt("HasCandy", 0);
            // PlayerPrefs.Save();
        }
    }
}
