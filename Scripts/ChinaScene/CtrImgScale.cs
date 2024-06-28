using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CtrImgScale : MonoBehaviour
{


    private bool isZoomed = false;  

    public float duration = 0.5f;

    public float fadeDuration = 0.3f;

    public TCPClient client;

    private Vector2 targetSize = new(1066f, 600f);

    private Vector2 targetPosition;

    private RectTransform rectTransform;

    private Canvas canvas;

    private Vector2 originalPosition; //初始位置

    private Vector2 originalSize; //初始尺寸

    private Image Back;
    private Image Menu;

    private Button BackBtn;
    private Button MenuBtn;

    private string cityName;

    private void Start()
    {
        cityName = transform.parent.parent.name;
        rectTransform = GetComponent<RectTransform>();

        // 获取当前对象的宽度和高度
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        originalSize = new Vector2(width, height);
       
        // 获取Canvas
        canvas = GetComponentInParent<Canvas>();
        targetPosition = canvas.transform.position;
        Back = GameObject.Find("Back").GetComponent<Image>();
        Menu = GameObject.Find("Menu").GetComponent<Image>();
        BackBtn = GameObject.Find("Back").GetComponent<Button>();
        MenuBtn = GameObject.Find("Menu").GetComponent<Button>();
    }

    public void OnClick()
    {
        DisableBtn(isZoomed);
        if (isZoomed)
        {
            client.SendMsg($"small:{cityName}");
            StartCoroutine(AnimateZoom(originalPosition, originalSize));
        }
        else
        {
            client.SendMsg($"big:{cityName}");
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

    private void DisableBtn(bool isActive)
    {
        BackBtn.interactable = isActive;
        MenuBtn.interactable = isActive;
        StartCoroutine(FadeOutCoroutine(isActive));
    }

   
        
  


    IEnumerator FadeOutCoroutine(bool isShow)
    {
        // 获取初始颜色
        Color originalColor = Back.color;
        float startAlpha = originalColor.a;
        float endAlpha = isShow ? 1.0f : 0.0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            // 使用SmoothStep进行平滑过渡
            float smoothStep = Mathf.SmoothStep(startAlpha, endAlpha, t);
            Back.color = new Color(originalColor.r, originalColor.g, originalColor.b, smoothStep);
            Menu.color = new Color(originalColor.r, originalColor.g, originalColor.b, smoothStep);
            yield return null;
        }
        // 确保最终透明度为0
        Back.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
        Menu.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
    }
}
