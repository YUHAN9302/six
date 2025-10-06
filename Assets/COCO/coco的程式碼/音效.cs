using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 音效 : MonoBehaviour
{
    public GameObject dialogueObject;  // 對話框 Object
    public GameObject soundObject;     // 音效 Object
    private bool soundPlayed = false;

    // Start is called before the first frame update
    void Update()
    {
        // 檢查對話框是否被打開
        if (dialogueObject.activeSelf && !soundPlayed)
        {
            soundPlayed = true; // 避免重複觸發
            StartCoroutine(PlaySoundOnce());
        }

        // 若對話框關閉，重置狀態
        if (!dialogueObject.activeSelf)
        {
            soundPlayed = false;
        }
    }
    IEnumerator PlaySoundOnce()
    {
        soundObject.SetActive(true);   // 開啟音效物件
        yield return new WaitForSeconds(2f); // 等待1秒
        soundObject.SetActive(false);  // 關閉音效物件
    }
}
