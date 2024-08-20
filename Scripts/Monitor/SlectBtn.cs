using UnityEngine;

public class SlectBtn : MonoBehaviour
{
    public LevelLoader LevelLoader;

    public TCPClient client;
    public void TabSelect(int index)
    {
        TabStore.SelectedTab = index;
        client.SendMsg($"cityIndex:{index}");
        LevelLoader.LoadNewScene("EnterMonitorScene", false); 
    }
}
