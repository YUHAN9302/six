using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class 登入 : MonoBehaviour
{
    public GameObject START;
    public GameObject Quit;
    Setting[] SettingObjects;
    SetAndGetSaveData[] LoadObjects;

    // Start is called before the first frame update
    void Start()
    {
        SettingObjects = GameObject.FindObjectsOfType<Setting>(true);
        SettingObjects[0].gameObject.SetActive(true);
        SettingObjects[0].gameObject.SetActive(false);

        LoadObjects = GameObject.FindObjectsOfType<SetAndGetSaveData>(true);
        LoadObjects[0].gameObject.SetActive(true);
        LoadObjects[0].gameObject.SetActive(false);
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
        FindObjectOfType<SaveManager>().PlayerPos = new Vector2(-7.47f, -1.08f);
        SceneManager.LoadScene(1);
    }
    public void SettingObj() {
        SettingObjects[0].gameObject.SetActive(true);
    }
    public void LoadObj()
    {
        LoadObjects[0].gameObject.SetActive(true);
    }
}
