using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class art : MonoBehaviour
{
    public GameObject tears;  // ���\�ʵe�� GameObject
    public GameObject bookObject; // �ѥ�����


    public void OnViewButtonClicked()
    {
        if (tears != null)
        {
            tears.SetActive(true);  // ��ܲ��\�ʵe
            StartCoroutine(HideTearsAndShowBook());
        }
    }

    private IEnumerator HideTearsAndShowBook()
    {
        yield return new WaitForSeconds(2f); // ���� 2 ��

        if (tears != null)
        {
            tears.SetActive(false); // ���ò��\�ʵe
        }

        if (bookObject != null)
        {
            bookObject.SetActive(true); // ��ܮѥ�
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
