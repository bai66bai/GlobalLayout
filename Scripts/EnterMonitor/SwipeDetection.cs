using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetection : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform swipeArea; // 设定滑动区域的RectTransform
    public float duration = 0.5f;
    public int MovingDistance = 1920;

    public Turnthepage Turnthepage;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float minSwipeDistance = 50f; // 设置最小滑动距离

    private bool isFinish = true;
    [HideInInspector] public int bigIndex; //最大滑动次数

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
        // 检查是否在滑动区域内开始滑动
        if (RectTransformUtility.RectangleContainsScreenPoint(swipeArea, eventData.position, eventData.pressEventCamera))
        {
            startPosition = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 更新滑动位置
        if (RectTransformUtility.RectangleContainsScreenPoint(swipeArea, eventData.position, eventData.pressEventCamera))
        {
            endPosition = eventData.position;
            // 在这里处理滑动逻辑（可以选择处理实时滑动）
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 结束滑动
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
                    Debug.Log("向右滑动");
                }
                else
                {
                    ToLeft();
                    Debug.Log("向左滑动");
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