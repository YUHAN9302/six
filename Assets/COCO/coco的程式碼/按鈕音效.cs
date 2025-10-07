using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 按鈕音效 : MonoBehaviour
{
    public AudioClip sound1; // 只放這個音效

    public void PlaySound()
    {
        if (sound1 != null)
        {
            AudioSource.PlayClipAtPoint(sound1, Camera.main.transform.position);
        }
    }
}
