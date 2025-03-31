using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAnimation : MonoBehaviour
{
    public Book bookScript;

    public void OnMouseDown()
    {
        if (bookScript != null)
        {
            // 隱藏書本物件和動畫物件
            bookScript.gameObject.SetActive(false);  // 隱藏書本物件
            bookScript.bookAnimation.SetActive(false);  // 隱藏動畫物件
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
