using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CtrPopupWiindow : MonoBehaviour
{
    private Vector3 targetLocalPosition = new(0, 0, 0);

    //��¼��ʼλ��
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
    /// �����еĵ��������ڸ�ԭʱ������ʾ
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
    /// ��Э��ִ�й����н�ͼƬ���¼�ע����
    /// </summary>
    /// <param name="IsMove">�ж��Ƿ����ƶ�</param>
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
