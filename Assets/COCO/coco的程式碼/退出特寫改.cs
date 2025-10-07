using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 退出特寫改 : MonoBehaviour
{
    [Header("客廳")]
    public string sceneName = "客廳";

    [Header("關眼動畫物件")]
    public GameObject closeEyesAnimationObject;

    private bool isClicked = false;
    public void OnClickChangeScene()
    {
        if (isClicked) return;
        isClicked = true;

        // 儲存主角位置（假設主角有 "Player" 標籤）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            位置紀錄.SetPosition(player.transform.position);
        }

        StartCoroutine(PlayCloseEyesAndLoadScene());
    }
    IEnumerator PlayCloseEyesAndLoadScene()
    {
        // 播放關眼動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CloseEyes");
                yield return new WaitForSeconds(2f); // 根據實際動畫長度調整
            }
        }

        // 切換場景
        SceneManager.LoadScene("客廳");
    }
}
