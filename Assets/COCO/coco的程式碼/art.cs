using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art : MonoBehaviour
{
    public GameObject tears;  // 眼淚動畫的 GameObject
    public GameObject bookObject; // 書本物件

    public GameObject 特寫黑框;

    public GameObject bookSoundObject; // 包含書本掉落音效的 GameObject
    public float soundDuration = 2f;  // 設定音效播放時長


    public void OnMouseDown()
    {
        if (tears != null)
        {
            tears.SetActive(true);  // 顯示眼淚動畫
            StartCoroutine(HideTearsAndShowBook());
            特寫黑框.SetActive(true);

        }
    }

    private IEnumerator HideTearsAndShowBook()
    {
        yield return new WaitForSeconds(2f); // 等待 2 秒

        if (tears != null)
        {
            tears.SetActive(false); // 隱藏眼淚動畫
            特寫黑框.SetActive(false);
        }

        if (bookObject != null)
        {
            bookObject.SetActive(true); // 顯示書本
        }
        //關閉畫框的碰撞器 無法再次點選

        if (bookSoundObject != null)
        {
            bookSoundObject.SetActive(true); // 顯示音效物件
            AudioSource audioSource = bookSoundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();  // 播放音效
            }

            // 等待音效播放完成後銷毀音效物件
            yield return new WaitForSeconds(soundDuration); // 等待音效播放完成
            Destroy(bookSoundObject);  // 銷毀音效物件
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
