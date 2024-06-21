using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetection : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform swipeArea; // �趨���������RectTransform
    public float duration = 0.5f;
    public int MovingDistance = 1920;

    public Turnthepage Turnthepage;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float minSwipeDistance = 50f; // ������С��������

    private bool isFinish = true;
    [HideInInspector] public int bigIndex; //��󻬶�����

    [HideInInspector] public int nowIndex;

    void Start()
    {
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        float width = swipeArea.rect.width;
        bigIndex = (int)width / 1920;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ����Ƿ��ڻ��������ڿ�ʼ����
        if (RectTransformUtility.RectangleContainsScreenPoint(swipeArea, eventData.position, eventData.pressEventCamera))
        {
            startPosition = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���»���λ��
        if (RectTransformUtility.RectangleContainsScreenPoint(swipeArea, eventData.position, eventData.pressEventCamera))
        {
            endPosition = eventData.position;
            // �����ﴦ�����߼�������ѡ����ʵʱ������
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ��������
        if (RectTransformUtility.RectangleContainsScreenPoint(swipeArea, eventData.position, eventData.pressEventCamera))
        {
            endPosition = eventData.position;
            DetectSwipeDirection();
        }
    }

    private void DetectSwipeDirection()
    {
        float swipeDistance = (endPosition - startPosition).magnitude;

        if (swipeDistance >= minSwipeDistance)
        {
            float swipeDirectionX = endPosition.x - startPosition.x;

            if (Mathf.Abs(swipeDirectionX) > Mathf.Abs(endPosition.y - startPosition.y))
            {
                if (swipeDirectionX > 0)
                {
                    ToRight();
                    Debug.Log("���һ���");
                }
                else
                {
                    ToLeft();
                    Debug.Log("���󻬶�");
                }
            }
        }
    }


    public void ToRight()
    {
        if (nowIndex > 0 && isFinish)
        {
            Vector3 localPostion = transform.localPosition;
            Vector3 targetLocalPosition = new Vector3(localPostion.x + MovingDistance, localPostion.y, localPostion.z);
            StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, duration));
            --nowIndex;
            Turnthepage.ChangeBtnColor(nowIndex);
            isFinish = false;
        }
    }


    public void ToLeft()
    {
        if (nowIndex < bigIndex - 1 && isFinish)
        {
            Vector3 localPostion = transform.localPosition;
            Vector3 targetLocalPosition = new Vector3(localPostion.x - MovingDistance, localPostion.y, localPostion.z);
            StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, duration));
            ++nowIndex;
            Turnthepage.ChangeBtnColor(nowIndex);
            isFinish = false;
        }
    }

    private IEnumerator MoveAndScaleOverTime(Vector3 newLocalPosition, float time)
    {
        Vector3 initialPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetPostionX = Mathf.SmoothStep(initialPosition.x, newLocalPosition.x, t);
            transform.localPosition = new Vector3(targetPostionX, newLocalPosition.y, newLocalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = newLocalPosition;
        isFinish = true;
    }

}