using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;      // ���w�A�� VideoPlayer
    public string nextSceneName;         // �U�@�ӭn���J�������W��

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
