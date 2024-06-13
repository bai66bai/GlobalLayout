using System.Collections;
using UnityEngine;

public class CtrImgScale : MonoBehaviour
{


    private bool isZoomed = false;  

    public float duration = 0.5f;

    private Vector2 targetSize = new(1066f, 600f);

    private Vector2 targetPosition;

    private RectTransform rectTransform;

    private Canvas canvas;

    private Vector2 originalPosition; //初始位置

    private Vector2 originalSize; //初始尺寸

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // 获取当前对象的宽度和高度
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        originalSize = new Vector2(width, height);
       
        // 获取Canvas
        canvas = GetComponentInParent<Canvas>();
        targetPosition = canvas.transform.position;
    }

    public void OnClick()
    {
        if (isZoomed)
        {
            StartCoroutine(AnimateZoom(originalPosition, originalSize));
        }
        else
        {
            originalPosition = rectTransform.position; //保存初始位置
            StartCoroutine(AnimateZoom(targetPosition, targetSize));
        }

        // 切换状态
        isZoomed = !isZoomed;
    }


    private IEnumerator AnimateZoom( Vector2 targetPosition, Vector2 targetSize)
    {
      
        Vector2 startPosition = rectTransform.position;
        Vector2 startSize = rectTransform.sizeDelta;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;  
            rectTransform.position = Vector2.Lerp(startPosition, targetPosition, t);
            rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 确保最终值
        rectTransform.position = targetPosition;
        rectTransform.sizeDelta = targetSize;
    }

}
