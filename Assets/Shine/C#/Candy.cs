using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public string Name = "糖果";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

        // 隱藏書本物件本體
        gameObject.SetActive(false);
    }
}
