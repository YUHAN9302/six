using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOX : MonoBehaviour
{
    public Animator boxAnimator;
    public Button boxButton;

    private bool isBoxOpen = false;
    private bool isAnimating = false;
    public GameObject ItemSlots;
    public GameObject boxSoundObject;

    // Start is called before the first frame update
    private void Start()
    {
        boxButton.onClick.AddListener(OnBoxButtonClick);
    }

    void OnBoxButtonClick()
    {
        if (isAnimating) return; // 防止動畫中重複觸發

        Debug.Log("按到了！");

        if (!isBoxOpen)
        {
            boxAnimator.ResetTrigger("CloseBox");
            boxAnimator.SetTrigger("OpenBox");
            StartCoroutine(WaitForAnimation("OpenBox"));

            StartCoroutine(PlaySoundAndHide(boxSoundObject));
        }
        else
        {
            boxAnimator.ResetTrigger("OpenBox");
            boxAnimator.SetTrigger("CloseBox");
            StartCoroutine(WaitForAnimation("CloseBox"));
        }

        isBoxOpen = !isBoxOpen;
    }

    private System.Collections.IEnumerator WaitForAnimation(string stateName)
    {
        isAnimating = true;

        AnimatorStateInfo stateInfo = boxAnimator.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName(stateName))
        {
            yield return null;
            stateInfo = boxAnimator.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(stateInfo.length);
        if (stateName == "OpenBox") {
            ItemSlots.SetActive(true);
        }
        else {
            ItemSlots.SetActive(false);

        }
        isAnimating = false;
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
