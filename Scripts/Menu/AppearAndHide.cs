using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppearAndHide : MonoBehaviour
{
    public Image image; // ��Ҫ����͸���ȵ�ͼƬ
    public List<TextMeshProUGUI> texts;
    public float duration = 0.5f; // ����ʱ��
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


    //�����ť��ʼ���ֵ���
    private void StartShowPopo()
    {
        ToggelBtnImg(true);
        StartCoroutine(FadeInImage());
        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeInText(text, duration));
        }
    }


    //�����Լ������Ƿ񼤻�
    private void SetActive(bool IsActive)
    {
        gameObject.SetActive(IsActive);
    }

    //�����ť�رյ�����
    private void EndColosePopo()
    {
        ToggelBtnImg(false);
        StartCoroutine(FadeOutImage());
        foreach (TextMeshProUGUI text in texts)
        {
            StartCoroutine(FadeOutText(text, duration));
        }
    }

    //�л���ť״̬
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
            float alpha = Mathf.Clamp01(elapsedTime / duration); // ����alphaֵ
            color.a = alpha;
            image.color = color;
            yield return null;
        }
        // ȷ������alphaΪ1��255��
        color.a = 1f;
        image.color = color;
        IsFinsh = true;
    }


    IEnumerator FadeInText(TextMeshProUGUI text, float duration)
    {
        // ��ʼ��ɫ��ȷ��alphaΪ0
        Color color = text.color;
        color.a = 0;
        text.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration); // ����alphaֵ
            color.a = alpha; // �����µ�alphaֵ
            text.color = color; // Ӧ�õ�������
            yield return null; // �ȴ���һ֡
        }
        // ȷ������alphaΪ1��255��
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

        // ȷ������͸����Ϊ0
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        IsFinsh = true;
    }
    IEnumerator FadeOutText(TextMeshProUGUI text, float duration)
    {
        // ��ʼ��ɫ��ȷ��alphaΪ0
        Color color = text.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        // ȷ������͸����Ϊ0
        text.color = new Color(color.r, color.g, color.b, 0f);
        IsShow = false;
        SetActive(IsShow);
    }
}
