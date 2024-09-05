
using UnityEngine;
using UnityEngine.Video;

public class CtrVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 视频播放器组件

    public TCPClient client;

    private bool isPlaying = false; // 当前播放状态

    public GameObject pause;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        videoPlayer.loopPointReached += (vp) =>
        {
            videoPlayer.time = 0;
            PauseBtn(true);
            isPlaying = true;
        };
    }
    /// <summary>
    /// 控制视频暂停播放
    /// </summary>
    public void TogglePlayPause()
    {
        if (!isPlaying)
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

        PauseBtn(isPlaying);
    }

    //控制暂停图标显示
    private void PauseBtn(bool state)
    {
        pause.SetActive(state);
    }

    public void CtrlStopVideo()
    {
        videoPlayer.Pause();
    }
}
