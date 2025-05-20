using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class 上樓 : MonoBehaviour
{

    public AudioSource clickSound;
    public string sceneName = "走廊2F";

    private bool isClicked = false;


    public GameObject closeEyesAnimationObject;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (isClicked) return;
        isClicked = true;

        if (clickSound != null)
        {
            clickSound.Play();
            Invoke(nameof(LoadScene), clickSound.clip.length);
        }
        else
        {
            LoadScene();
        }
    }

    // Update is called once per frame
    void LoadScene()
    {
        SceneManager.LoadScene("走廊2F");
    }
}
