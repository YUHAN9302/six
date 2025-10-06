using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 下樓 : MonoBehaviour
{
    public GameObject soundObject;
    public string sceneName = "走廊1F";

    private bool isClicked = false;


    public GameObject closeEyesAnimationObject;

    // Start is called before the first frame update
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



        StartCoroutine(PlaySoundAndChangeScene());
    }

    // Update is called once per frame
    IEnumerator PlaySoundAndChangeScene()
    {
        // 播放音效物件
        if (soundObject != null)
        {
            soundObject.SetActive(true);
        }

        // 播放關眼動畫
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CloseEyes");
            }
        }

        // 等待 1 秒
        yield return new WaitForSeconds(2f);

        // 關閉音效物件
        if (soundObject != null)
        {
            soundObject.SetActive(false);
        }

        SceneManager.LoadScene("走廊1F");
    }
}
