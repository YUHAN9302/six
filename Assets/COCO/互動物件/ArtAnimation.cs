using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtAnimation : MonoBehaviour
{
    public GameObject tears;  // 存放動畫的物件
    public Animator animator;
    public List<Button> buttonsToDisable;

    // Start is called before the first frame update
    void Start()
    {
        if (tears != null)
        {
            tears.SetActive(false);  // 隱藏
        }
    }
    public void OnViewButtonClicked()
    {
        // 顯示動畫物件
        SetButtonsActive(false);

        if (tears != null)
        {
            tears.SetActive(true);
        }

        // 播放動畫
        if (animator != null)
        {
            animator.SetTrigger("畫中人掉淚");  // 播放動畫的 Trigger
        }

        // 設置 Coroutine 來等待動畫結束後隱藏物件
        StartCoroutine(HideAfterAnimation());
    }

    private IEnumerator HideAfterAnimation()
    {
        // 等待動畫播放完（這裡假設動畫長度為 3 秒）
        // 你可以根據實際的動畫長度進行調整
        yield return new WaitForSeconds(2f);

        // 隱藏動畫物件
        if (tears != null)
        {
            tears.SetActive(false);
        }
        SetButtonsActive(true);
    }
    private void SetButtonsActive(bool state)
    {
        foreach (Button btn in buttonsToDisable)
        {
            if (btn != null)
            {
                btn.gameObject.SetActive(state);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
