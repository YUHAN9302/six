using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 互動物紀錄 : MonoBehaviour
{
    public string objectID; // 每個互動物件給個唯一名稱


    [Header("互動後的狀態設定")]
    public bool hasInteracted = false;      // 是否已互動
    public Animator animator;               // 若互動後有動畫
    public string interactedAnimName;       // 互動後要保持的動畫名
    public AudioSource interactSound;       // 若有音效
    public GameObject newObjectToEnable;         // ✅ 回房後要開啟的新物件

    public bool disableAfterInteract = true;     // ✅ 是否互動後隱藏自己

    void Start()
    {
        // ✅ 如果上次就已經互動過，維持狀態（不重置）
        if (位置紀錄.HasInteracted(objectID))
        {
            hasInteracted = true;

            // 如果有動畫，要讓它維持互動後狀態
            if (animator != null && !string.IsNullOrEmpty(interactedAnimName))
            {
                animator.Play(interactedAnimName);
            }
            // ✅ 如果有指定新的物件 → 開啟它
            if (newObjectToEnable != null)
                newObjectToEnable.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (hasInteracted) return; // 已互動過不再觸發

      
            hasInteracted = true;

            // ✅ 記錄互動狀態
            位置紀錄.AddInteraction(objectID);
            Debug.Log($"互動：{objectID}");

            // ✅ 播放音效
            if (interactSound != null)
                interactSound.Play();

            // ✅ 關閉自己（若設定）
            if (disableAfterInteract)
                gameObject.SetActive(false);

            // ✅ 若指定新的物件 → 立即開啟
            if (newObjectToEnable != null)
                newObjectToEnable.SetActive(true);
        
    }
}
