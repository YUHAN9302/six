using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // 轉場遮罩 (UI Image)
    public float fadeDuration = 1f; // 轉場持續時間


    // Update is called once per frame
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    // 淡出效果並切換場景
    public void FadeOutAndChangeScene(int sceneIndex)
    {
        StartCoroutine(FadeOutCoroutine(sceneIndex));
    }

    private IEnumerator FadeInCoroutine()
    {
        float alpha = 1;
        fadeImage.gameObject.SetActive(true); // 顯示遮罩
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);  // 變更顏色透明度
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);  // 隱藏遮罩
    }

    private IEnumerator FadeOutCoroutine(int sceneIndex)
    {
        float alpha = 0;
        fadeImage.gameObject.SetActive(true);  // 顯示遮罩

        // 讓遮罩逐漸變為不透明
        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 載入新場景
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;  // 不立即激活場景

        // 等待場景加載完成
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true; // 啟動場景
            }
            yield return null;
        }

        // 等待場景載入後進行淡入
        yield return new WaitForSeconds(0.5f);

        // 淡入效果
        alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);  // 隱藏遮罩
    }
    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);  // 開始時隱藏轉場遮罩
        }
    }
}
