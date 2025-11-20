using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 判定娃娃 : MonoBehaviour
{
    public static bool DollInteracted
    {
        get
        {
            return 位置紀錄.HasInteracted("doll"); // Doll 就是 objectID
        }
    }
}
