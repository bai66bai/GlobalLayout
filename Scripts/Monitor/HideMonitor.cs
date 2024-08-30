using UnityEngine;

public class HideMonitor : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(MonitorStore.MonitorState == "visible");
    }

}
