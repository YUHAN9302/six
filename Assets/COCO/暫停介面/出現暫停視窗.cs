using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 出現暫停視窗 : MonoBehaviour
{
    public GameObject 暫停;
    public GameObject 濾鏡;
    // Start is called before the first frame update
    void Start()
    {
        if (暫停 != null)
            暫停.SetActive(false); // 開始時隱藏 UI
        if (濾鏡 != null)
            濾鏡.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // 按下 ESC 鍵
        {
            if (暫停 != null)
                暫停.SetActive(!暫停.activeSelf);
            if (濾鏡 != null)
                濾鏡.SetActive(!濾鏡.activeSelf);
        }
    }
}
