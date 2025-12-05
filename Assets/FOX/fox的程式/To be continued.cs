using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobecontinued : MonoBehaviour
{
    
    private Animator animator;
    public GameObject menuUI;
    public GameObject HomeButton;
    public float delayTime = 9f; // 畫面停留時間（秒）

    void Start()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(false);
        }

        StartCoroutine(ShowMenuAfterDelay());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator ShowMenuAfterDelay()
    {
        // 等待指定時間
        yield return new WaitForSeconds(delayTime);

        // 顯示菜單畫面
        if (menuUI != null)
        {
            menuUI.SetActive(true);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();   // 自動抓同物件的 Animator
    }

    public void StopAnimationAtLastFrame()
    {
        animator.speed = 0;
    }

    public void home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}