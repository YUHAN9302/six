using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art : MonoBehaviour
{
    public GameObject tears;  // ���\�ʵe�� GameObject
    public GameObject bookObject; // �ѥ�����

    public GameObject �S�g�®�;

    public GameObject bookSoundObject; // �]�t�ѥ��������Ī� GameObject
    public float soundDuration = 2f;  // �]�w���ļ���ɪ�


    public void OnMouseDown()
    {
        if (tears != null)
        {
            tears.SetActive(true);  // ��ܲ��\�ʵe
            StartCoroutine(HideTearsAndShowBook());
            �S�g�®�.SetActive(true);

        }
    }

    private IEnumerator HideTearsAndShowBook()
    {
        yield return new WaitForSeconds(2f); // ���� 2 ��

        if (tears != null)
        {
            tears.SetActive(false); // ���ò��\�ʵe
            �S�g�®�.SetActive(false);
        }

        if (bookObject != null)
        {
            bookObject.SetActive(true); // ��ܮѥ�
        }
        //�����e�ت��I���� �L�k�A���I��

        if (bookSoundObject != null)
        {
            bookSoundObject.SetActive(true); // ��ܭ��Ī���
            AudioSource audioSource = bookSoundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();  // ���񭵮�
            }

            // ���ݭ��ļ��񧹦���P�����Ī���
            yield return new WaitForSeconds(soundDuration); // ���ݭ��ļ��񧹦�
            Destroy(bookSoundObject);  // �P�����Ī���
        }

        GetComponent<Collider2D>().enabled = false;
        Destroy(this);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
