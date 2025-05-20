using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public GameObject openDoorSoundObject; // �}�����Ī���
    public GameObject lockedSoundObject;   // ������Ī���
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

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("���Y2F"); // �T�O�o�ӳ����W�٥��T�B�w�[�J Build Settings
    }
    public void OnCloseOpenDoorDialogue()
    {
        if (OpenDoorDialogueUI != null)
        {
            OpenDoorDialogueUI.SetActive(false);
            StartCoroutine(LoadNextSceneAfterDelay(0.5f));
        }
    }
}
