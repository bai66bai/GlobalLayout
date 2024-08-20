using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextVlc : MonoBehaviour
{
   public VLCPlayerExample VLCPlayerExample;

    private int count = 0;

    private void Update()
    {
        count++;
        if(count > 500)
        {
            VLCPlayerExample.mediaPlayer.Media = new LibVLCSharp.Media(new Uri("https://sf1-cdn-tos.huoshanstatic.com/obj/media-fe/xgplayer_doc_video/hls/xgplayer-demo.m3u8"));
            VLCPlayerExample.mediaPlayer.Play();
        }
    }
}
