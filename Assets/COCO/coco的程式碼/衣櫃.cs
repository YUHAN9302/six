using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 衣櫃 : MonoBehaviour
{
    public GameObject player; // 角色物件
    private 人物走路程式 playerScript; // 角色的腳本
    public GameObject closeEyeAnimation; // 閉眼動畫物件
    public float closeEyeDuration = 1.5f; // 閉眼動畫總時長
    private bool hasChangedClothes = false; // 是否已經換過裝

    public GameObject changeClothesSoundObject; // 換衣音效物件


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

        if (closeEyeAnimation != null)
        {
            closeEyeAnimation.SetActive(false); // 開始時先隱藏閉眼動畫
        }
        else
        {
            Debug.LogError("請在 Inspector 指定閉眼動畫的物件！");
        }
    }

    private void OnMouseDown()
    {
        if (hasChangedClothes) return; // 如果已經換過裝，就不再觸發

        if (playerScript != null)
        {
            StartCoroutine(PlayCloseEyeAnimation()); // 播放閉眼動畫並等待
            Debug.Log("點擊衣櫃，開始換裝動畫！");
        }
        else
        {
            Debug.LogError("未找到 人物走路程式，請確認角色身上有該腳本！");
        }
    }

    IEnumerator PlayCloseEyeAnimation()
    {
        if (closeEyeAnimation != null)
        {
            closeEyeAnimation.SetActive(true); // 顯示閉眼動畫
        }

        if (changeClothesSoundObject != null)
        {
            changeClothesSoundObject.SetActive(true); // 顯示音效物件
            AudioSource audioSource = changeClothesSoundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play(); // 播放換衣音效
            }
        }

        float halfDuration = closeEyeDuration / 2f; // 計算動畫的一半時間
        yield return new WaitForSeconds(halfDuration); // 先等待一半時間

        playerScript.ChangeClothes(); // 在動畫播放一半時換裝
        hasChangedClothes = true; // 設定為已換裝，之後不能再觸發
        Debug.Log("換裝完成（動畫進行一半）！");

        yield return new WaitForSeconds(halfDuration); // 再等待剩餘時間

        if (closeEyeAnimation != null)
        {
            closeEyeAnimation.SetActive(false); // 隱藏閉眼動畫
        }

        Debug.Log("閉眼動畫結束，角色睜眼！");

        if (changeClothesSoundObject != null)
        {
            // 等待音效播放完成後銷毀物件
            AudioSource audioSource = changeClothesSoundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                yield return new WaitForSeconds(audioSource.clip.length); // 根據音效長度等待
                Destroy(changeClothesSoundObject); // 銷毀音效物件
            }
        }
        Destroy(this);

    }
}