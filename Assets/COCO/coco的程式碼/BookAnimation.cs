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
            // ���îѥ�����M�ʵe����
            bookScript.gameObject.SetActive(false);  // ���îѥ�����
            bookScript.bookAnimation.SetActive(false);  // ���ðʵe����
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
