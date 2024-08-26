using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseVideo : MonoBehaviour
{
    public TCPClient client;
    public CtrVideoPlayer player;

    public void StopVideo()
    {
        client.SendMsg($"stopVideo:pause");
        player.CtrlStopVideo();
    }
}
