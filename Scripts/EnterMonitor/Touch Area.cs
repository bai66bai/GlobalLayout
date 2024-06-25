using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 startPoint;
    private Vector2 endPoint;
    private bool isSwiping = false;
    public SwipeDetection swipeDetection;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPoint = eventData.position;
        isSwiping = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 这里可以根据需要添加拖动过程中显示拖动轨迹的代码
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endPoint = eventData.position;
        isSwiping = false;

        DetectSwipeDirection();
    }

    private void DetectSwipeDirection()
    {
        if (isSwiping)
        {
            return;
        }

        float deltaX = endPoint.x - startPoint.x;
        if (Mathf.Abs(deltaX) > 50)  // 设置一个阈值，避免微小的滑动也被检测到
        {
            if (deltaX > 0)
            {
                swipeDetection.ToRight();
            }
            else
            {
                swipeDetection.ToLeft();
            }
        }
    }
}
