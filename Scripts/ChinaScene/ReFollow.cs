using UnityEngine;

public class ReFollow : MonoBehaviour
{
    public Transform Text;
    public Transform Text1;

    //控制文字不受按钮缩放的影响
    void Update()
    {
        var parentScale = transform.parent.localScale;
        var targetScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1);
        Text.localScale = targetScale;
        Text1.localScale = targetScale;
    }
}
