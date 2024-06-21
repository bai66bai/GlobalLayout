
using UnityEngine;
using UnityEngine.Video;

public class CtrVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 视频播放器组件
    public GameObject controlButton;    // 控制播放暂停的按钮

    private bool isPlaying = false; // 当前播放状态

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // 确保视频一开始是暂停的
        videoPlayer.Pause();
        isPlaying = false;
    }

    public void TogglePlayPause()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
        isPlaying = !isPlaying;
    }
}
