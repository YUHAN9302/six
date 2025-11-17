using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truecandy : MonoBehaviour
{
    private bool isClicked = false;

    public void OnMouseUp()
    {
        Debug.Log(" 玩家點擊糖果");

        // 記錄點擊（跨場景有效）
        儲存管理.Instance.hasClickedCandy = true;

    }
}
