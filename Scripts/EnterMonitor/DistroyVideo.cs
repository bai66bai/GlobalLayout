using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyVideo : MonoBehaviour
{
    public List<GameObject> VideoScreens;
    private bool isExist = true;

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
        isExist = false;
    }


    private void Update()
    {
        if(!isExist)
        {
            foreach (var item in VideoScreens)
                Debug.Log(item);
        }
    }
}
