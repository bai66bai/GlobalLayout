using LibVLCSharp;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ReadStreamingAssets : MonoBehaviour
{
    public VLCPlayerExample vLCPlayerExample;
    public string VideoName;

    private bool isStop = false;
    void Start()
    {
        StartCoroutine(ReadFile());
    }

    private void Update()
    {
        if (vLCPlayerExample != null
            && vLCPlayerExample.mediaPlayer != null)
        {

            if (vLCPlayerExample.mediaPlayer.State == VLCState.Stopping && !isStop)
            {
                isStop = true;
                vLCPlayerExample.Resume();
            }
            else
            {
                isStop = false;
            }
        }
    }

    IEnumerator ReadFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, VideoName.Trim());

        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            vLCPlayerExample.path = filePath;
            vLCPlayerExample.StrartVideo(filePath);
        }
        else
        {
            Debug.LogError("º”‘ÿ ß∞‹£∫ " + www.error);
        }
    }
}
