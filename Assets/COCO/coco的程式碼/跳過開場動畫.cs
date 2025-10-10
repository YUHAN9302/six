using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 跳過開場動畫 : MonoBehaviour
{
    public string sceneName = "0228";

    public GameObject closeEyesAnimationObject; // 關眼動畫物件
    private bool isClicked = false;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        StartCoroutine(PlayAnimationAndChangeScene());
    }
    IEnumerator PlayAnimationAndChangeScene()
    {
        // 直接開啟動畫物件
        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
        }

        // 等待動畫物件播放（假設動畫長度約 2 秒）
        yield return new WaitForSeconds(1f);

        // 切換場景
        SceneManager.LoadScene(sceneName);
    }
}
