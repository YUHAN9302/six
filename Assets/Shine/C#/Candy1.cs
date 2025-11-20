using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy1 : MonoBehaviour
{
     string Name = "金平糖";

    public void OnMouseUp()
    {

        // 自動儲存：玩家位置 + 道具
        int saveID;
        if (SetAndGetSaveData.SelectID != 0)
        {
            saveID = SetAndGetSaveData.SelectID;
        }
        else
        {
            SetAndGetSaveData.SelectID = 1;
            saveID = SetAndGetSaveData.SelectID;

        }
        if (saveID > 0)
        {
            var saveSystem = FindObjectOfType<SetAndGetSaveData>();
            if (saveSystem != null)
            {
                saveSystem.SaveDataPos(saveID);               // 儲存位置
                saveSystem.SaveDataItem(saveID, Name);    // 儲存道具
                Debug.Log($"《{Name}》已自動儲存到存檔 {saveID}");
            }
        }
        else
        {
            Debug.LogWarning("❌ 尚未選擇存檔，無法自動儲存");
        }

    }
}
