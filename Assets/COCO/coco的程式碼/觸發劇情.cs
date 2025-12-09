using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 觸發劇情 : MonoBehaviour
{
    [Header("指定要刪除的物件")]
    public GameObject targetObject; // 指向你要刪除的開場物件

    public string triggerKey; // 用來檢查是否已觸發，可用物件名
    private bool triggered = false;

    // 靜態變數紀錄已觸發的 triggerKey
    private static HashSet<string> triggeredKeys = new HashSet<string>();

    private void Start()
    {
        if (string.IsNullOrEmpty(triggerKey))
        {
            triggerKey = gameObject.name;
        }

        // 如果已經觸發過，直接刪除目標物件
        if (triggeredKeys.Contains(triggerKey) ||
            (TriggerManager.Instance != null && TriggerManager.Instance.IsObjectClicked(triggerKey)))
        {
            if (targetObject != null)
                 Destroy(targetObject);

            // 觸發物件本身可以選擇保留或刪除，這裡保留
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // 模擬 ClickableObject 點擊行為
            ClickableObject clickable = GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
            }

            // 記錄靜態變數，確保跨場景也不重複觸發
            triggeredKeys.Add(triggerKey);

            // 記錄 TriggerManager
            if (TriggerManager.Instance != null)
            {
                TriggerManager.Instance.RecordClick(triggerKey);
            }

            // ⭐ 刪除指定目標物件
            if (targetObject != null)
                Destroy(targetObject);

            // 刪除物件
            //Destroy(gameObject);
        }
    }
}
