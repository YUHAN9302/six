using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetItem : MonoBehaviour
{
    public Image dollImage;
    public Sprite state0, state1, state2, finalState;
    public RectTransform needleThreadTargetPos;
    private Vector2 needleThreadStartPos;


    public GameObject needle;       // 針
    public GameObject thread;       // 線
    public GameObject needleThread; // 針線（初始隱藏）
    public GameObject button;       // 鈕扣
    public RectTransform buttonTargetPos; // 鈕扣固定位置
    public GameObject cloth;        // 布料

    [HideInInspector] public bool fixedEyes = false;
    [HideInInspector] public bool changedClothes = false;
    [HideInInspector] public bool buttonPlaced = false;

    void Start()
    {
        dollImage.sprite = state0;
        needleThread.SetActive(false);

        // 記錄針線初始位置
        RectTransform rt = needleThread.GetComponent<RectTransform>();
        needleThreadStartPos = rt.anchoredPosition;

    }

    public void OnToolDropped(ToolDrag tool)
    {
        string name = tool.toolName;
        bool success = false;

        // -------------------
        // 針 + 線 → 針線
        // -------------------
        if ((name == "針" && thread.activeSelf) || (name == "線" && needle.activeSelf))
        {
            needle.SetActive(false);
            thread.SetActive(false);

            needleThread.SetActive(true);

            // 不改變父物件
            RectTransform rt = needleThread.GetComponent<RectTransform>();
            rt.anchoredPosition = needleThreadStartPos; // 針線固定位置
            rt.localScale = Vector3.one;
            rt.localRotation = Quaternion.identity;

            success = true;
        }
        // -------------------
        // 鈕扣放娃娃固定位置（不消失）
        // -------------------
        else if (name == "鈕扣")
        {
            // 檢查是否拖到娃娃範圍內
            RectTransform dollRT = dollImage.GetComponent<RectTransform>();
            RectTransform buttonRT = tool.GetRectTransform();

            // 判斷是否在娃娃範圍內
            if (RectTransformUtility.RectangleContainsScreenPoint(dollRT, buttonRT.position))
            {
                // 成功放置 → 固定位置
                buttonPlaced = true;
                buttonRT.anchoredPosition = buttonTargetPos.anchoredPosition;
                success = true;
            }
            else
            {
                // 拖錯位置 → 回原位
                tool.ResetPosition();
                success = false;
            }
        }
        // -------------------
        // 針線 + 娃娃 + 已放置鈕扣 → 修眼睛
        // -------------------
        else if (name == "針線")
        {
            if (buttonPlaced)
            {
                // 成功互動
                needleThread.SetActive(false);
                button.SetActive(false);
                fixedEyes = true;
                dollImage.sprite = changedClothes ? finalState : state1;
                success = true;
            }
            else
            {
                // 未放鈕扣 → 針線回原位，不消失
                tool.ResetPosition();

                // 針線自身回到原始位置
                RectTransform rt = needleThread.GetComponent<RectTransform>();
                rt.anchoredPosition = needleThreadStartPos;
                rt.localScale = Vector3.one;
                rt.localRotation = Quaternion.identity;

                success = false;
            }
        }
        // -------------------
        // 布料 + 娃娃 → 換衣服
        // -------------------
        else if (name == "布料" && !changedClothes)
        {
            changedClothes = true;
            dollImage.sprite = fixedEyes ? finalState : state2;
            cloth.SetActive(false);
            success = true;
        }

        if (!success)
        {
            tool.ResetPosition(); // 互動失敗回原位
        }
    }
}
