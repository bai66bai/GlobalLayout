using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CtrPopupWiindow : MonoBehaviour
{
    private Vector3 targetLocalPosition = new(0, 0, 0);

    //记录初始位置
    private Vector3 initialPosition;
    public float duration = 2.0f;

    public List<EventTrigger> PopuImages;
    public static bool IsFinish = false;
    public List<GameObject> DetailList;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (!IsFinish)
        {
             DetailList.ForEach(e =>
        {
            if(e.activeSelf && e.GetComponent<CtrContentActive>().IsMoveEnd)
            {
                IsFinish = true;
                return;
            }
        });
        }
       
    }
    /// <summary>
    /// 让所有的弹窗内容在复原时调用显示
    /// </summary>
    public void AllShow()
    => DetailList.ForEach((e) => { e.SetActive(true); });

    public void StartMove()
    {
        StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, duration ,true));
    }

    public void StartMoveEnd()
    {
        StartCoroutine(MoveAndScaleOverTime(initialPosition, duration,false));
    }

    /// <summary>
    /// 在协程执行过程中将图片的事件注销掉
    /// </summary>
    /// <param name="IsMove">判断是否在移动</param>
    public void DisableEvent(bool IsMove)
    => PopuImages.ForEach((e) => { e.enabled = IsMove; });
    

    private IEnumerator MoveAndScaleOverTime(Vector3 newLocalPosition, float time , bool IsPopu)
    {
        DisableEvent(false);
        Vector3 initPos = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetPostionX = Mathf.SmoothStep(initPos.x, newLocalPosition.x, t);
            transform.localPosition = new Vector3(targetPostionX, 0, 0);

            elapsedTime += Time.smoothDeltaTime;
            yield return null;
        }
        transform.localPosition = newLocalPosition;
        DisableEvent(true);
        if (!IsPopu)
            AllShow();
    }
}
