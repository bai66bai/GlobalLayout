using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CtrImgScale : MonoBehaviour
{


    public bool isZoomed = false;  

    public float duration = 0.5f;

    public float fadeDuration = 0.3f;

    public TCPClient client;

    public EventTrigger CtrlImage;

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
    private bool isCoroutineRunning = false;
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
        if (!isCoroutineRunning)
        {
            isCoroutineRunning = true;
            DisableBtn(isZoomed);
            if (isZoomed)
            {
                     client.SendMsg($"small:{cityName}");
                Debug.Log(originalPosition);
                    StartCoroutine(AnimateZoom(originalPosition, originalSize));
                    CtrlImage.enabled = true;
            }
            else
            {
                    client.SendMsg($"big:{cityName}");
                originalPosition = rectTransform.position; //保存初始位置
                StartCoroutine(AnimateZoom(targetPosition, targetSize));
                    CtrlImage.enabled = false;

            }
            // 切换状态
            isZoomed = !isZoomed;
            }
    }

    private IEnumerator AnimateZoom( Vector2 targetPos, Vector2 tarSize)
    {
        CtrBtnChange.IsFinish = false;
        Vector2 startPosition = rectTransform.position;
        Vector2 startSize = rectTransform.sizeDelta;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;  
            rectTransform.position = Vector2.Lerp(startPosition, targetPos, t);
            rectTransform.sizeDelta = Vector2.Lerp(startSize, tarSize, t);
            elapsedTime += Time.smoothDeltaTime;
            yield return null; 
        }
        // 确保最终值
        rectTransform.position = targetPos;
        rectTransform.sizeDelta = tarSize;
        isCoroutineRunning = false;
        CtrBtnChange.IsFinish = true;
    }
    /// <summary>
    /// 控制按钮的显示隐藏
    /// </summary>
    /// <param name="isActive">显示或隐藏</param>
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
