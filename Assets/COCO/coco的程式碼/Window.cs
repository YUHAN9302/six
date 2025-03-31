using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject windowAnimation;  // �ѥ��ʵe�� GameObject
    private bool animationPlayed = false;  // �ʵe�O�_����L
    // Start is called before the first frame update
    void Start()
    {
        if (windowAnimation != null)
        {
            windowAnimation.SetActive(false); // �T�O�ʵe����@�}�l�O���ê�
        }
    }

    public void OnMouseUp()
    {
        if (!windowAnimation.activeSelf)
        {
            windowAnimation.SetActive(true);  // ��ܮѥ��ʵe

        }

    }
    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);  // ���ݫ��w�ɶ�
        windowAnimation.SetActive(false);  // ���ðʵe����

    }
    // Update is called once per frame
    void Update()
    {

    }
}
