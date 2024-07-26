
using System.Collections.Generic;
using UnityEngine;

public class Fllow : MonoBehaviour
{
    public List<GameObject> Regions;

    //���ְ�ťԭ��С
    void Update()
    {
        var parentScale = transform.parent.localScale;
        var targetScale = new Vector3(1 / parentScale.x,1/parentScale.y,1/parentScale.z);
        Regions.ForEach(e =>
        {
            e.transform.localScale = targetScale;
        });
    }


}
