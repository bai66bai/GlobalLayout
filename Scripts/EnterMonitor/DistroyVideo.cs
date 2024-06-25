using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyVideo : MonoBehaviour
{
    public List<GameObject> VideoScreens;

    public void DestroyList()
    {
        foreach (var item in VideoScreens)
        {
            VLCPlayerExample[] VLCPlayerExamples = item.GetComponentsInChildren<VLCPlayerExample>();
            foreach (var player in VLCPlayerExamples)
            {
                player.DestoryVLCPlayer();
            }
        }
    }


}
