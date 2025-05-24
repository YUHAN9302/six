using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 下樓 : MonoBehaviour
{
    public AudioSource clickSound;
    public string sceneName = "走廊1F";

    private bool isClicked = false;


    public GameObject closeEyesAnimationObject;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        if (clickSound != null)
        {
            clickSound.Play();
        }

        StartCoroutine(PlayCloseEyesAndLoadScene());
    }

    // Update is called once per frame
    IEnumerator PlayCloseEyesAndLoadScene()
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
                // 若沒找到 animator，直接進入場景
                yield return new WaitForSeconds(1f);
            }
        }

        // 等待音效（如果有）結束
        if (clickSound != null)
        {
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        SceneManager.LoadScene("走廊1F");
    }
}
