using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CtrVideo : MonoBehaviour
{
    public RenderTexture RenderTexture;
    public VideoPlayer videoPlayer;
    public string nextSceneName;
    public GameObject GameObject;

    void Start()
    {
        //释放RenderTexture视频素材
        RenderTexture.Release();

        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // 注册loopPointReached事件的监听器是否播放完成
        videoPlayer.loopPointReached += OnVideoFinished;

    }
      //播放视频
      public void OnLodeScene() => GameObject.SetActive(true);   
    // 播放完成切换到下一个场景
    void OnVideoFinished(VideoPlayer vp) => SceneManager.LoadScene(nextSceneName);
    
    private void OnDisable() => RenderTexture.Release();
}
