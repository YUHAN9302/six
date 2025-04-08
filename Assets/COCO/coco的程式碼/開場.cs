using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 開場 : MonoBehaviour
{
    public GameObject blackScreen;  // 黑幕 (UI Image)
    public GameObject eyeCloseObject; // 閉眼動畫的 Animator
    public GameObject dialogBox;

    public GameObject blanketSoundObject; // 棉被音效物件
    private bool blanketSoundPlayed = false;
    private AudioSource blanketAudioSource; // 音效的 AudioSource


    private void Awake()
    {
        dialogBox.SetActive(true);
    }
    private void StartSequence()
    {
        blackScreen.SetActive(false); // 隱藏黑幕
        eyeCloseObject.SetActive(true); // 開啟動畫物件

        StartCoroutine(HideAfterAnimation());

        enabled = false; // 停止 Update，避免重複偵測
    }
    private IEnumerator HideAfterAnimation()
    {
        // 等待動畫播放完畢
        yield return new WaitForSeconds(0.4f); // 這裡填寫你的動畫時間 (秒)

        eyeCloseObject.SetActive(false); // 隱藏動畫物件
    }
    // Start is called before the first frame update
    void Start()
    {
        blackScreen.SetActive(true);

        if (blanketSoundObject != null)
        {
            blanketAudioSource = blanketSoundObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogBox.activeSelf && !blanketSoundPlayed)
        {
            StartCoroutine(PlayBlanketSoundAndStartSequence());
            blanketSoundPlayed = true;
        }
    }
    private IEnumerator PlayBlanketSoundAndStartSequence()
    {
        if (blanketSoundObject != null)
        {
            blanketSoundObject.SetActive(true);
            if (blanketAudioSource != null)
            {
                blanketAudioSource.Play();
                yield return new WaitForSeconds(blanketAudioSource.clip.length); // 等待音效播放完成
            }
            blanketSoundObject.SetActive(false);
        }

        StartSequence();
    }
}
