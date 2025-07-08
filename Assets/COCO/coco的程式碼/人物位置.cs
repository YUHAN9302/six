using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 人物位置 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3? savedPos = 位置紀錄.GetPosition();
        if (savedPos.HasValue)
        {
            transform.position = savedPos.Value;
            位置紀錄.ClearPosition(); // 清除以免重複用到
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
