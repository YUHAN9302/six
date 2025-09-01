using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 進時鐘特寫 : MonoBehaviour
{
    public AudioSource clickSound;
    public string sceneName = "時鐘特寫";

    private bool isClicked = false;


    public GameObject closeEyesAnimationObject;

    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        // 儲存主角位置（假設主角有 "Player" 標籤）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            位置紀錄.SetPosition(player.transform.position);
        }

        if (clickSound != null)
        {
            clickSound.Play();
        }

        StartCoroutine(PlayCloseEyesAndLoadScene());
    }

    private IEnumerator PlayCloseEyesAndLoadScene()
    {
        // 播放動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CloseEyes");

                // 假設動畫長度為 2 秒，可根據實際動畫長度調整
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }

        // 等待音效（如果有）結束
        if (clickSound != null)
        {
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        SceneManager.LoadScene("時鐘特寫");
    }
}
