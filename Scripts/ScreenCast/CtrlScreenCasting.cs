using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CtrlScreenCasting : MonoBehaviour
{
    private bool IsScreenCasting = true;
    public TCPClient client;
    public CtrScreenVideoPlayer player;

    public Image CloseImage;
    public Image StartImage;
    public void ChangeScreenCastingStatus(string VideoName)
    {
        if (IsScreenCasting)
        {
            ChangeImg();
            client.SendMsg($"play:{VideoName}");
            player.CtrlResetVideo();
            ScreenStore.IsBegin = true;
        }
        else
        {
            ChangeImg();
            client.SendMsg($"close:screenCasting");
            ScreenStore.IsBegin = false;
        }
        IsScreenCasting = !IsScreenCasting;
    }

    private void ChangeImg()
    {
        if(IsScreenCasting)
        {
            CloseImage.enabled = true;
            StartImage.enabled = false;
        }
        else
        {
            CloseImage.enabled = false;
            StartImage.enabled = true;
        }
    }
    
}
