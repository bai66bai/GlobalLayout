using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CtrBtn : MonoBehaviour
{
    public List<TextMeshProUGUI> textMeshProUGUIs;

    public List<Image> imageList;

    public List<GameObject> Btns;

    public List<GameObject> CtrBackBtn;

    public float duration = 2.0f;

    public TCPClient client;

    private Color baseColor = new(1f, 1f, 1f, 0.5f);

    private Color baseTextColor = new(8 / 255f, 64 / 255f, 248 / 255f, 0.5f);

    private Color selectedColor = new(8 / 255f, 64 / 255f, 248 / 255f, 1f);

    private Color unSelectedColor = new(1f, 1f, 1f, 1f);
    public void OnClickBtn(string name)
    {
        Btns.ForEach(b =>
        {
            int index = Btns.IndexOf(b);
            if (b.name == name)
            {
                client.SendMsg($"btnName:{name}");
                CtrBackBtnEnable();
                StartCoroutine(ChangeColorOverTime(textMeshProUGUIs[index], imageList[index], unSelectedColor, selectedColor, duration));
            }
            else
            {
                StartCoroutine(ChangeColorOverTime(textMeshProUGUIs[index], imageList[index], baseTextColor, baseColor, duration));
            }
            b.GetComponent<EventTrigger>().enabled = false;
        });
    }


    IEnumerator ChangeColorOverTime(TextMeshProUGUI text, Image btn, Color textColor, Color btnColor, float duration)
    {
        var a = text.text;
        float elapsed = 0.0f;

        Color textStartColor = text.color;
        Color btnStartColor = btn.color;


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            float tcolorR = Mathf.SmoothStep(btnStartColor.r, btnColor.r, t);
            float tcolorG = Mathf.SmoothStep(btnStartColor.g, btnColor.g, t);
            float tcolorB = Mathf.SmoothStep(btnStartColor.b, btnColor.b, t);
            float tcolorA = Mathf.SmoothStep(btnStartColor.a, btnColor.a, t);
            btn.color = new(tcolorR, tcolorG, tcolorB, tcolorA);
            float icolorR = Mathf.SmoothStep(textStartColor.r, textColor.r, t);
            float icolorG = Mathf.SmoothStep(textStartColor.g, textColor.g, t);
            float icolorB = Mathf.SmoothStep(textStartColor.b, textColor.b, t);
            float icolorA = Mathf.SmoothStep(textStartColor.a, textColor.a, t);
            text.color = new(icolorR, icolorG, icolorB, icolorA);

            yield return null;
        }
        btn.color = btnColor;
        text.color = textColor;
        yield return new WaitForSeconds(4f);
        HideBtn();
    }

    /// <summary>
    /// 控制按钮到达时间消失
    /// </summary>
    public void HideBtn()
    {
        imageList.ForEach(i =>
        {
            StartCoroutine(ChangeBtnDisapper(i, duration));
        });
        textMeshProUGUIs.ForEach(t =>
        {
            StartCoroutine(ChangeTextDisapper(t, duration));
        });
    }
    IEnumerator ChangeTextDisapper(TextMeshProUGUI text, float duration)
    {
        float elapsed = 0.0f;
        Color textStartColor = text.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float icolorA = Mathf.SmoothStep(textStartColor.a, 0, t);
            text.color = new(textStartColor.r, textStartColor.g, textStartColor.b, icolorA);
            yield return null;
        }

    }

    IEnumerator ChangeBtnDisapper(Image btn, float duration)
    {
        float elapsed = 0.0f;

        Color btnStartColor = btn.color;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float tcolorA = Mathf.SmoothStep(btnStartColor.a, 0, t);
            btn.color = new(btnStartColor.r, btnStartColor.g, btnStartColor.b, tcolorA);
            yield return null;
        }

    }

    /// <summary>
    /// 禁用返回按钮
    /// </summary>
    public void CtrBackBtnEnable()
    {
        CtrBackBtn.ForEach(b =>
        {
            b.GetComponent<Button>().interactable = false;
        });
    }
}


