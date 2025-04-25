using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject openDoorSoundObject; // 開門音效物件
    public GameObject lockedSoundObject;   // 鎖門音效物件
    private bool isOpen = false;

    public GameObject closeEyesAnimationObject;
    public GameObject CloseDoorDialogueUI;
    public GameObject OpenDoorDialogueUI;

    private void OnMouseDown()
    {
        if (isOpen) return;

        if (TriggerManager.Instance != null && TriggerManager.Instance.AreAllClicked())
        {
            OpenDoor();
            OpenDoorDialogueUI.SetActive(true);

        }
        else
        {
            PlayLockedSound();
            CloseDoorDialogueUI.SetActive(true);

        }
    }

    void OpenDoor()
    {
        isOpen = true;
        if (openDoorSoundObject != null)
        {
            StartCoroutine(PlaySoundAndHide(openDoorSoundObject)); // 播放開門音效
        }

        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CloseEyes");  // 假設動畫有一個名為 "CloseEyes" 的觸發器
            }
        }
        Debug.Log("Door is now open!");
    }

    void PlayLockedSound()
    {
        if (lockedSoundObject != null)
        {
            StartCoroutine(PlaySoundAndHide(lockedSoundObject)); // 播放鎖門音效
        }
        Debug.Log("Door is locked!");
    }
    IEnumerator PlaySoundAndHide(GameObject soundObject)
    {
        soundObject.SetActive(true);

        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length); // 等待音效播放完畢
        }

        soundObject.SetActive(false);
    }
}
