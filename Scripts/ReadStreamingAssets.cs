using System.Collections;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ReadStreamingAssets : MonoBehaviour
{
    public VLCPlayerExample vLCPlayerExample;
    public string LocalPath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReadFile());
    }

    private void Update()
    {
        if (vLCPlayerExample != null
            && vLCPlayerExample.mediaPlayer != null
            && vLCPlayerExample.mediaPlayer.Time > vLCPlayerExample.mediaPlayer.Length - 1)
        {
            vLCPlayerExample.mediaPlayer.SetTime(0);
        }
    }

    IEnumerator ReadFile()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "a.mp4");

        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string data = www.downloadHandler.text;
            vLCPlayerExample.StrartVideo(filePath);
        }
        else
        {
            Debug.LogError("º”‘ÿ ß∞‹£∫ " + www.error);
        }
    }
}
