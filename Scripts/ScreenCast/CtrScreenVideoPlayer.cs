using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CtrScreenVideoPlayer : MonoBehaviour
{
     public VideoPlayer videoPlayer; // ��Ƶ���������

    public TCPClient client;

    private bool isPlaying = false; // ��ǰ����״̬

    private GameObject pause;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        pause = GameObject.Find("Pause");
        videoPlayer.loopPointReached += OnVideoEnd;
        // ȷ����Ƶһ��ʼ����ͣ��
        videoPlayer.Pause();
        isPlaying = false;

    }


    void OnVideoEnd(VideoPlayer vp)
    {
        PauseBtn(true);
        isPlaying = false;
    }

    /// <summary>
    /// ������Ƶ��ͣ����
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

    //������ͣͼ����ʾ
    private void PauseBtn(bool state)
    {
        pause.SetActive(state);
    }

    public void CtrlResetVideo()
    {
        // ֹͣ��Ƶ����
        videoPlayer.Stop();

        // ����ʱ��Ϊ0�����ص���һ֡
        videoPlayer.time = 0;

        // ׼������Ƶ���ţ���������ͣ״̬
        videoPlayer.Prepare();

        // �ȴ���Ƶ׼���ú���ͣ
        videoPlayer.prepareCompleted += PauseAfterPrepare;
        // ��Ƶ׼���ú���ͣ
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
        // ȡ�������Ա����ڴ�й©
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
