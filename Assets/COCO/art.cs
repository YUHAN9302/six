using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art : MonoBehaviour
{
    public GameObject tears;  // 眼淚動畫的 GameObject
    public GameObject bookObject; // 書本物件


    public void OnViewButtonClicked()
    {
        if (tears != null)
        {
            tears.SetActive(true);  // 顯示眼淚動畫
            StartCoroutine(HideTearsAndShowBook());
        }
    }

    private IEnumerator HideTearsAndShowBook()
    {
        yield return new WaitForSeconds(2f); // 等待 2 秒

        if (tears != null)
        {
            tears.SetActive(false); // 隱藏眼淚動畫
        }

        if (bookObject != null)
        {
            bookObject.SetActive(true); // 顯示書本
        }
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
