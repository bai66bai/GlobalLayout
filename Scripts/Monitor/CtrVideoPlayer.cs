
using UnityEngine;
using UnityEngine.Video;

public class CtrVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 视频播放器组件
    public GameObject controlButton;    // 控制播放暂停的按钮

    public TCPClient client;

    private bool isPlaying = false; // 当前播放状态

    private GameObject pause;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        pause = GameObject.Find("Pause");
        videoPlayer.loopPointReached += (vp) =>
        {
            videoPlayer.time = 0;
            PauseBtn(true);
            isPlaying = false;
        };
        // 确保视频一开始是暂停的
        videoPlayer.Pause();
        isPlaying = false;

    }
    /// <summary>
    /// 控制视频暂停播放
    /// </summary>
    public void TogglePlayPause()
    {
        if (isPlaying)
        {
            client.SendMsg($"touchScreen:togglePlay");
            videoPlayer.Pause();
        }
        else
        {
            client.SendMsg($"touchScreen:togglePlay");
            videoPlayer.Play();
        }
        isPlaying = !isPlaying;

        PauseBtn(!isPlaying);
    }

    //控制暂停图标显示
    private void PauseBtn(bool state)
    {
        pause.SetActive(state);
    }
}
