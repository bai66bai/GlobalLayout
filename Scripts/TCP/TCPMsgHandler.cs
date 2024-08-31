using UnityEngine;
using UnityEngine.SceneManagement;

public  class TCPMsgHandler : MonoBehaviour
{
    private LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = GetComponent<LevelLoader>();
    }

    public virtual void OnMsg(string msg) 
    {
        MonitorStore.MonitorState = msg;
        Debug.Log(msg);
        levelLoader.LoadNewScene("MenuScene");
    }
}

