using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 衣櫃 : MonoBehaviour
{
    public GameObject player; // 角色物件
    private 人物走路程式 playerScript; // 角色的腳本

    private void Start()
    {
        if (player != null)
        {
            playerScript = player.GetComponent<人物走路程式>();
        }
        else
        {
            Debug.LogError("請在 Inspector 中指定 Player 角色物件！");
        }
    }

    private void OnMouseDown()
    {
        if (playerScript != null)
        {
            playerScript.ChangeClothes(); // 呼叫角色的換衣方法
            Debug.Log("點擊衣櫃，執行換裝！");
        }
        else
        {
            Debug.LogError("未找到 人物走路程式，請確認角色身上有該腳本！");
        }
    }
}
