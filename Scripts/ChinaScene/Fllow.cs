
using System.Collections.Generic;
using UnityEngine;

public class Fllow : MonoBehaviour
{
    public Transform beijingTransform;
    public Transform hongkongTransform;
    public Transform shanghaiTransform;
    public Transform suzhouTransfrom;
    public Transform hangzhouTransfrom;

    public List<GameObject> Regions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var parentScale = transform.parent.localScale;
        var targetScale = new Vector3(1 / parentScale.x,1/parentScale.y,1/parentScale.z);
        Regions.ForEach(e =>
        {
            e.transform.localScale = targetScale;
        });
        //beijingTransform.localScale = targetScale;
        //hongkongTransform.localScale = targetScale;
        //shanghaiTransform.localScale = targetScale;
        //suzhouTransfrom.localScale = targetScale;
        //hangzhouTransfrom.localScale = targetScale;
       
    }
}
