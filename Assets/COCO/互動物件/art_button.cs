using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art_button : MonoBehaviour
{
    public GameObject artButton;
    public Animator animator;
    public GameObject bookObject;


    // Start is called before the first frame update
    void Start()
    {
        if (artButton != null)
        {
            artButton.SetActive(false);
        }
        if (bookObject != null)
        {
            bookObject.SetActive(false); // �@�}�l���îѥ�
        }
    }

    void OnMouseEnter()  // ��ƹ��i�J�����
    {
        if (artButton != null)
        {
            artButton.SetActive(true);  // ��ܫ��s
        }
    }
    void OnMouseExit()  // ��ƹ����}�����
    {
        if (artButton != null)
        {
            artButton.SetActive(false);  // ���ë��s
        }
    }
    public void OnViewButtonClicked()  // ����s�Q�I����
    {
        Debug.Log("�d�ݵe�@�I");
        // �A�i�H�b�o��Ĳ�o�@���޿�A�Ҧp��ܵe�@���ԲӫH���C
        if (animator != null)
        {
            animator.SetTrigger("�e���H���\");  // ����ʵeĲ�o��
        }
        if (bookObject != null)
        {
            bookObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
