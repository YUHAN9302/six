using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM不中斷 : MonoBehaviour
{
    private static BGM不中斷 instance;
    private AudioSource audioSource;

    public string[] scenesToMute;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 如果新場景在要關閉 BGM 的列表中，靜音
        foreach (string sceneName in scenesToMute)
        {
            if (scene.name == sceneName)
            {
                audioSource.Pause();
                return;
            }
        }

        // 如果不是要靜音的場景，恢復播放
        if (!audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
    }
}
