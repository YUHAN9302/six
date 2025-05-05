using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject windowAnimation;  // �ѥ��ʵe�� GameObject
    private bool animationPlayed = false;  // �ʵe�O�_����L
    public GameObject windowSoundObject;

    // Start is called before the first frame update
    void Start()
    {
        if (windowAnimation != null)
        {
            windowAnimation.SetActive(false); // �T�O�ʵe����@�}�l�O���ê�
        }
    }

    public void OnMouseUp()
    {
        if (!windowAnimation.activeSelf)
        {
            windowAnimation.SetActive(true);  // ��ܮѥ��ʵe

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

            // ���ݭ��ļ��񧹦�
            yield return new WaitForSeconds(audio.clip.length);
        }

        soundObject.SetActive(false); // ���񧹲�������
    }

    // Update is called once per frame
    void Update()
    {

    }
}
