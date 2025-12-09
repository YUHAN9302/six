using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 判斷待續 : MonoBehaviour
{
    [Header("好結局 UI")]
    public GameObject goodUI;

    [Header("壞結局 UI")]
    public GameObject badUI;

    [Header("跳轉場景")]
    public string sceneName = "To be continued";

    [Header("轉場動畫物件（要有 Animator）")]
    public GameObject closeEyesAnimationObject;

    private bool goodOpened = false;
    private bool badOpened = false;
    private bool isTransitionStarted = false;


    void Update()
    {
        // --- 好結局 UI ---
        if (goodUI != null)
        {
            if (!goodOpened && goodUI.activeSelf)
                goodOpened = true;

            if (goodOpened && !goodUI.activeSelf && !isTransitionStarted)
                StartCoroutine(PlayTransitionAndChangeScene());
        }

        // --- 壞結局 UI ---
        if (badUI != null)
        {
            if (!badOpened && badUI.activeSelf)
                badOpened = true;

            if (badOpened && !badUI.activeSelf && !isTransitionStarted)
                StartCoroutine(PlayTransitionAndChangeScene());
        }
    }

    IEnumerator PlayTransitionAndChangeScene()
    {
        isTransitionStarted = true;

        // 播放轉場動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);

            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("CloseEyes");  // 你的動畫 Trigger
        }

        // 等待動畫播放完成（你原本是 1.5 秒）
        yield return new WaitForSeconds(1.5f);

        // 切場景
        SceneManager.LoadScene(sceneName);
    }
}
