using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    public GameObject bookAnimation; // �ѥ��ʵe�� GameObject
    private bool isPlaying = false;  // �O���ʵe�O�_���b���
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
        // �p�G�I�����O�ѥ�����
        if (!isPlaying && bookAnimation != null)
        {
            bookAnimation.SetActive(true); // ��ܨü���ʵe
            isPlaying = true;

            // ���ݰʵe���񧹦�
            StartCoroutine(WaitForAnimation());
        }
        // �p�G�I�����O�ʵe����A�åB�ʵe�w�g����
        else if (isPlaying && bookAnimation != null)
        {
            bookAnimation.SetActive(false); // ���ðʵe����
            isPlaying = false;
        }
    }
    private IEnumerator WaitForAnimation()
    {
        Animator animator = bookAnimation.GetComponent<Animator>();
        if (animator != null)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // ���ݰʵe����
        }
        isPlaying = false; // ���\�A���I���������ʵe
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
