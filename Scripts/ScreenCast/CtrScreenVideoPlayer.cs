using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CtrScreenVideoPlayer : MonoBehaviour
{
     public VideoPlayer videoPlayer; // 视频播放器组件

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
        videoPlayer.loopPointReached += OnVideoEnd;
        // 确保视频一开始是暂停的
        videoPlayer.Pause();
        isPlaying = false;

    }


    void OnVideoEnd(VideoPlayer vp)
    {
        PauseBtn(true);
        isPlaying = false;
    }

    /// <summary>
    /// 控制视频暂停播放
    /// </summary>
    public void ToggleScreenPlayPause()
    {
        if (isPlaying)
        {
            SendClientMesg();
            videoPlayer.Pause();
        }
        else
        {
            SendClientMesg();
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

    public void CtrlResetVideo()
    {
        // 停止视频播放
        videoPlayer.Stop();

        // 设置时间为0，即回到第一帧
        videoPlayer.time = 0;

        // 准备好视频播放，但保持暂停状态
        videoPlayer.Prepare();

        // 等待视频准备好后暂停
        videoPlayer.prepareCompleted += PauseAfterPrepare;
        // 视频准备好后暂停
        isPlaying = false ;
        PauseBtn(true);
    }
    private void PauseAfterPrepare(VideoPlayer vp)
    {
        videoPlayer.Pause();
    }

    private void SendClientMesg()
    {
        if (ScreenStore.IsBegin)
        {
            client.SendMsg($"ScreenCast:togglePlay-{ScreenStore.VideoSceneName}");
        }
    }

    void OnDestroy()
    {
        // 取消订阅以避免内存泄漏
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
