using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 觸發劇情 : MonoBehaviour
{
    public string triggerKey; // 用來檢查是否已觸發，可用物件名
    private bool triggered = false;


    // Start is called before the first frame update
    private void Start()
    {
        if (string.IsNullOrEmpty(triggerKey))
        {
            triggerKey = gameObject.name;
        }

        if (TriggerManager.Instance != null && TriggerManager.Instance.IsObjectClicked(triggerKey))
        {
            // 如果已經觸發過，就直接銷毀這個物件
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            // 呼叫 ClickableObject 的 OnMouseDown 行為（模擬點擊）
            ClickableObject clickable = GetComponent<ClickableObject>();
            if (clickable != null)
            {
                clickable.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
            }

            // 記錄觸發狀態
            if (TriggerManager.Instance != null)
            {
                TriggerManager.Instance.RecordClick(triggerKey);
            }

            // 銷毀物件
            Destroy(gameObject);
        }
    }
}
