using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
{
    public GameObject bigPhoto;


    void OnMouseUp()
    {
        bigPhoto.SetActive(true); // 顯示相框特寫
    }

    // Start is called before the first frame update

}
