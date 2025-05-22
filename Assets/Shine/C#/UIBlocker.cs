using UnityEngine;
using UnityEngine.EventSystems;

public class UIBlocker : MonoBehaviour
{
    //public GameObject blockPanel;         // 全畫面透明遮罩（UI用 Image + CanvasGroup）
    public MonoBehaviour[] scriptsToDisable; // 你要暫時禁用的腳本（如玩家控制器）

    void OnEnable()
    {
        // 啟用遮罩
       // if (blockPanel != null)
       //     blockPanel.SetActive(true);

        // 顯示滑鼠
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        // 禁用指定腳本
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = false;
        }

        //（可選）暫停時間
        // Time.timeScale = 0;
    }

    void OnDisable()
    {
        // 關閉遮罩
        //if (blockPanel != null)
         //   blockPanel.SetActive(false);

        // 隱藏滑鼠
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // 還原腳本
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = true;
        }

        //（可選）恢復時間
        // Time.timeScale = 1;
    }
}
