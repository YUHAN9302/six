using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject openDoorSoundObject; // �}�����Ī���
    public GameObject lockedSoundObject;   // ������Ī���
    private bool isOpen = false;

    public GameObject closeEyesAnimationObject;

    private void OnMouseDown()
    {
        if (isOpen) return;

        if (TriggerManager.Instance != null && TriggerManager.Instance.AreAllClicked())
        {
            OpenDoor();
        }
        else
        {
            PlayLockedSound();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        if (openDoorSoundObject != null)
        {
            StartCoroutine(PlaySoundAndHide(openDoorSoundObject)); // ����}������
        }

        if (closeEyesAnimationObject != null)
        {
            closeEyesAnimationObject.SetActive(true);
            Animator animator = closeEyesAnimationObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("CloseEyes");  // ���]�ʵe���@�ӦW�� "CloseEyes" ��Ĳ�o��
            }
        }
        Debug.Log("Door is now open!");
    }

    void PlayLockedSound()
    {
        if (lockedSoundObject != null)
        {
            StartCoroutine(PlaySoundAndHide(lockedSoundObject)); // �����������
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
            yield return new WaitForSeconds(audioSource.clip.length); // ���ݭ��ļ��񧹲�
        }

        soundObject.SetActive(false);
    }
}
