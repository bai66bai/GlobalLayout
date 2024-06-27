using UnityEngine;

public class CtrLoadScene : MonoBehaviour
{
    public GameObject tempObject;
    public GameObject tempObject1;

    private string LoadScene;

    public LevelLoader LevelLoader;

    public TCPClient client;

    private bool isLoad = true;
    // Update is called once per frame
    void Update()
    {
        if (tempObject == null && tempObject1 == null)
        {
            if (isLoad)
            {
                LevelLoader.LoadNewScene(LoadScene,false);
                isLoad = false;
            }
        }
    }
    public void StartDestroy(string name)
    {
       client.SendMsg($"sceneName:{name}");
        VLCPlayerExample[] vLCPlayer1Examples = tempObject1.GetComponentsInChildren<VLCPlayerExample>();
        foreach (var item in vLCPlayer1Examples)
        {
            item.DestroyMediaPlayer();
        }
        VLCPlayerExample[] vLCPlayerExamples = tempObject.GetComponentsInChildren<VLCPlayerExample>();
        foreach (var item in vLCPlayerExamples)
        {
            item.DestroyMediaPlayer();
        }
        tempObject = null;
        tempObject1 = null;
        LoadScene = name;
    }

}
