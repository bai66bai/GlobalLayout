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
        //�ͷ�RenderTexture��Ƶ�ز�
        RenderTexture.Release();

        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // ע��loopPointReached�¼��ļ������Ƿ񲥷����
        videoPlayer.loopPointReached += OnVideoFinished;

    }
      //������Ƶ
      public void OnLodeScene() => GameObject.SetActive(true);   
    // ��������л�����һ������
    void OnVideoFinished(VideoPlayer vp) => SceneManager.LoadScene(nextSceneName);
    
    private void OnDisable() => RenderTexture.Release();
}
