using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TargetItem : MonoBehaviour
{
    public Image dollImage;
    public Sprite state0, state1, state2, finalState, cutMouthState; // ✅ 新增 cutMouthState

    public RectTransform needleThreadTargetPos;
    private Vector2 needleThreadStartPos;

    [Header("工具與固定位置")]
    public GameObject needle;       // 針
    public GameObject thread;       // 線
    public GameObject needleThread; // 針線（初始隱藏）
    public GameObject button;       // 鈕扣
    public RectTransform buttonTargetPos; // 鈕扣固定位置
    public GameObject cloth;        // 布料

    public GameObject scissor;      // 剪刀（初始隱藏）
    public GameObject dialoguePanel; // ✅ 對話框（初始關閉）
    public GameObject closeEyesAnimationObject;

    [Header("互動音效物件")]
    public GameObject needleSoundObj;
    public GameObject threadSoundObj;
    public GameObject needleThreadSoundObj;
    public GameObject buttonSoundObj;
    public GameObject clothSoundObj;
    public GameObject scissorSoundObj;

    [HideInInspector] public bool fixedEyes = false;
    [HideInInspector] public bool changedClothes = false;
    [HideInInspector] public bool buttonPlaced = false;
    [HideInInspector] public bool canUseScissor = false; // 是否可以使用剪刀



    private bool dialogueShown = false; // ✅ 確保對話框只顯示一次


    void Start()
    {
        dollImage.sprite = state0;
        needleThread.SetActive(false);

        if (scissor != null) scissor.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);


        // 記錄針線初始位置
        RectTransform rt = needleThread.GetComponent<RectTransform>();
        needleThreadStartPos = rt.anchoredPosition;

    }

    public void OnToolDropped(ToolDrag tool)
    {
        string name = tool.toolName;
        bool success = false;

        // -------------------
        // 剪刀先判斷
        // -------------------
        if (name == "剪刀")
        {
            if (canUseScissor)
            {
                PlaySoundObject(scissorSoundObj);
                dollImage.sprite = cutMouthState;
                tool.gameObject.SetActive(false);
                StartCoroutine(BackToRoomAfterDelay(2f));
                success = true;
            }
            else
            {
                tool.ResetPosition();
                success = false;
            }
        }

        // -------------------
        // 針 + 線 → 針線
        // -------------------
        else if((name == "針" && thread.activeSelf) || (name == "線" && needle.activeSelf))
        {
            PlaySoundObject(needleThreadSoundObj);
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
                PlaySoundObject(buttonSoundObj);
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
                PlaySoundObject(needleThreadSoundObj);
                needleThread.SetActive(false);
                button.SetActive(false);
                fixedEyes = true;
                dollImage.sprite = changedClothes ? finalState : state1;
                success = true;

                // ✅ 若修眼睛也完成 → 最終型態
                if (fixedEyes && changedClothes)
                    if (dialoguePanel != null && !dialoguePanel.activeSelf)
                        dialoguePanel.SetActive(true);

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
            PlaySoundObject(clothSoundObj);
            changedClothes = true;
            dollImage.sprite = fixedEyes ? finalState : state2;
            cloth.SetActive(false);
            success = true;

            // ✅ 若修眼睛也完成 → 最終型態
            if (fixedEyes && changedClothes)
                if (dialoguePanel != null && !dialoguePanel.activeSelf)
                    dialoguePanel.SetActive(true);

        }

        if (!success)
        {
            tool.ResetPosition(); // 互動失敗回原位
        }
       

        if (!success)
        {
            tool.ResetPosition();
        }
    }

    // -------------------
    // 對話結束時呼叫
    // -------------------
    public void OnDialogueEnd()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // 顯示剪刀
        if (scissor != null)
        {
            scissor.SetActive(true);
            canUseScissor = true;
        }
    }
    private void PlaySoundObject(GameObject soundObj)
    {
        if (soundObj != null)
            StartCoroutine(PlaySoundCoroutine(soundObj));
    }

    private IEnumerator PlaySoundCoroutine(GameObject soundObj)
    {
        soundObj.SetActive(true);
        yield return new WaitForSeconds(2f); // 播放音效持續時間，可自行調整
        soundObj.SetActive(false);
    }


    // ✅ 剪嘴後延遲切換場景
    IEnumerator BackToRoomAfterDelay(float delay)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            位置紀錄.SetPosition(player.transform.position);

        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator anim = closeEyesAnimationObject.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("CloseEyes");
                yield return new WaitForSeconds(1f);
            }
        }

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("妹妹房間");
    }
}

