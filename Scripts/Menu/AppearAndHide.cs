using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppearAndHide : MonoBehaviour
{
    public Image image; // 需要调整透明度的图片
    public List<TextMeshProUGUI> texts;
    public float duration = 0.5f; // 渐变时间
    public Image CloseImg;
    public Image OpenImg;

    private bool IsShow = false;
    private bool IsFinsh = true;
    public void TogglePopoActive()
    {
        if (!IsFinsh) return;
        IsFinsh = false;
        if (IsShow)
        {
            EndColosePopo();

        }
        else
        {
            SetActive(!IsShow);
            StartShowPopo();
            IsShow = true;
        }
    }


    //点击按钮开始显现弹窗
    private void StartShowPopo()
    {
        ToggelBtnImg(true);
        StartCoroutine(FadeInImage());
        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeInText(text, duration));
        }
    }


    //控制自己本身是否激活
    private void SetActive(bool IsActive)
    {
        gameObject.SetActive(IsActive);
    }

    //点击按钮关闭弹窗框
    private void EndColosePopo()
    {
        ToggelBtnImg(false);
        StartCoroutine(FadeOutImage());
        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeOutText(text, duration));
        }
    }

    //切换按钮状态
    public void ToggelBtnImg(bool IsOpen)
    {
        CloseImg.enabled = IsOpen;
        OpenImg.enabled = !IsOpen;
    }



    IEnumerator FadeInImage()
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration); // 计算alpha值
            color.a = alpha;
            image.color = color;
            yield return null;
        }
        // 确保最终alpha为1（255）
        color.a = 1f;
        image.color = color;
        IsFinsh = true;
    }


    IEnumerator FadeInText(TextMeshProUGUI text, float duration)
    {
        // 初始颜色，确保alpha为0
        Color color = text.color;
        color.a = 0;
        text.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration); // 计算alpha值
            color.a = alpha; // 设置新的alpha值
            text.color = color; // 应用到文字上
            yield return null; // 等待下一帧
        }
        // 确保最终alpha为1（255）
        color.a = 1f;
        text.color = color;
    }



    IEnumerator FadeOutImage()
    {
        Color originalColor = image.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 确保最终透明度为0
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        IsFinsh = true;
    }
    IEnumerator FadeOutText(TextMeshProUGUI text, float duration)
    {
        // 初始颜色，确保alpha为0
        Color color = text.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        // 确保最终透明度为0
        text.color = new Color(color.r, color.g, color.b, 0f);
        IsShow = false;
        SetActive(IsShow);
    }
}
