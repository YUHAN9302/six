using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject windowAnimation;  // 書本動畫的 GameObject
    private bool animationPlayed = false;  // 動畫是否播放過
    public GameObject windowSoundObject;

    // Start is called before the first frame update
    void Start()
    {
        if (windowAnimation != null)
        {
            windowAnimation.SetActive(false); // 確保動畫物件一開始是隱藏的
        }
    }

    public void OnMouseUp()
    {
        if (!windowAnimation.activeSelf)
        {
            windowAnimation.SetActive(true);  // 顯示書本動畫

            if (windowSoundObject != null)
            {
                StartCoroutine(PlaySoundAndHide(windowSoundObject));
            }

            Destroy(this);
        }

    }

    IEnumerator PlaySoundAndHide(GameObject soundObject)
    {
        soundObject.SetActive(true);
        AudioSource audio = soundObject.GetComponent<AudioSource>();

        if (audio != null)
        {
            audio.Play();

            // 等待音效播放完成
            yield return new WaitForSeconds(audio.clip.length);
        }

        soundObject.SetActive(false); // 播放完畢後隱藏
    }

    // Update is called once per frame
    void Update()
    {

    }
}
