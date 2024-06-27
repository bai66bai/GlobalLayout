
using UnityEngine;
using UnityEngine.Video;

public class CtrVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer; // ��Ƶ���������
    public GameObject controlButton;    // ���Ʋ�����ͣ�İ�ť

    public TCPClient client;

    private bool isPlaying = false; // ��ǰ����״̬

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // ȷ����Ƶһ��ʼ����ͣ��
        videoPlayer.Pause();
        isPlaying = false;
    }

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
    }
}
