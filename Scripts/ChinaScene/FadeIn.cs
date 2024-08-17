using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class FadeIn : MonoBehaviour
{
    public Image image; // 需要调整透明度的图片
    public List<TextMeshProUGUI> texts;
    public float duration = 0.5f; // 渐变时间

    void Start()
    {
        // 开始渐变协程
        StartCoroutine(FadeInImage());
        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeInText(text, duration));
        }
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
}
