using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openbook : MonoBehaviour
{
    [Header("�ĤG�i������")]
    public GameObject objectImage;

    [Header("�Ĥ@�i�����ϡ]�ۤv�^")]
    public GameObject currentImage;

    public void OpenObjectImage()
    {
        if (objectImage != null)
            objectImage.SetActive(true);   // ��ܲĤG�i

        if (currentImage != null)
            currentImage.SetActive(false); // �����Ĥ@�i
    }
}
