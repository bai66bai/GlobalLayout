using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrContentActive : MonoBehaviour
{
    public List<Transform> Contents;

    public float interval = 0.15f; //物体移动时间间隔

    public float moveDuration = 0.1f;// 移动持续时间

    private Vector3 StartPosition = new Vector3(388f, 0f, 0f);

    private Vector3 EndPosition = new Vector3(0f, 0f, 0f);

    public bool IsMoveEnd = false;

    public CtrImgScale ctrImgScale;

    void OnEnable()
    {
        StartMove();
        IsMoveEnd = false;
        CtrPopupWiindow.IsFinish = false;
    }

    public void StartMove()
    {
        // 启动协程
        StartCoroutine(ExecuteRepeatedly());
    }


    // 定义协程间隔
    IEnumerator ExecuteRepeatedly()
    {
        float actionTime = 0f;

        for (int i = 0; i < Contents.Count; i++)
        {
            actionTime += Time.deltaTime;
            float t = actionTime / interval;
            float actionPosition = StartPosition.x;
            StartCoroutine(MoveObject(Contents[i], EndPosition));
            yield return null;
        }
    }

    //控制内容移动
    IEnumerator MoveObject(Transform obj, Vector3 targetPos)
    {
        Vector3 startPos = obj.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;

            float nextX = Mathf.SmoothStep(startPos.x, targetPos.x, t);
            obj.localPosition = new Vector3(nextX, startPos.y, startPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.localPosition = targetPos; // 确保最后位置精确
        if(Contents.IndexOf(obj) == Contents.Count - 1)
        {
            IsMoveEnd = true;
        }
    }


    private void OnDisable()
    {
        if (!ctrImgScale.isZoomed)
        {
            Contents.ForEach(u =>
        {
            u.localPosition = StartPosition;
        });
        }     
    }

}
