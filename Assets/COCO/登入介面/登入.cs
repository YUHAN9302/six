using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 登入 : MonoBehaviour
{
    public GameObject START;
    public GameObject Quit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    public void second()
    {
        SceneManager.LoadScene(0);
    }
}
