using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    public GameObject bookAnimation; // 書本動畫的 GameObject
    private bool isPlaying = false;  // 記錄動畫是否正在顯示
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (bookAnimation != null)
        {
            animator = bookAnimation.GetComponent<Animator>();
        }
    }

    void OnMouseDown()
    {
        // 如果點擊的是書本物件
        if (!isPlaying && bookAnimation != null)
        {
            bookAnimation.SetActive(true); // 顯示並播放動畫
            isPlaying = true;

            // 等待動畫播放完成
            StartCoroutine(WaitForAnimation());
        }
        // 如果點擊的是動畫物件，並且動畫已經播放完
        else if (isPlaying && bookAnimation != null)
        {
            bookAnimation.SetActive(false); // 隱藏動畫物件
            isPlaying = false;
        }
    }
    private IEnumerator WaitForAnimation()
    {
        Animator animator = bookAnimation.GetComponent<Animator>();
        if (animator != null)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // 等待動畫播完
        }
        isPlaying = false; // 允許再次點擊來關閉動畫
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
