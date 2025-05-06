using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject bookAnimation;  // �ѥ��ʵe�� GameObject
    private bool animationPlayed = false;  // �ʵe�O�_����L



    // Start is called before the first frame update
    void Start()
    {
        if (bookAnimation != null)
        {
            bookAnimation.SetActive(false); // �T�O�ʵe����@�}�l�O���ê�
        }
       FindObjectOfType<SetAndGetSaveData>().SaveData(1, "�ѥ�");

    }

    public void OnMouseUp()
    {
        if (!bookAnimation.activeSelf)
        {
            bookAnimation.SetActive(true);  // ��ܮѥ��ʵe
            gameObject.SetActive(false);     // ���îѥ�����

        }

    }


    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);  // ���ݫ��w�ɶ�
        bookAnimation.SetActive(true);  // ���ðʵe����
        gameObject.SetActive(false);     // ���îѥ�����
    }

   


    // Update is called once per frame
    void Update()
    {

    }
}