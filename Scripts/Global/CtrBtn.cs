using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CtrBtn : MonoBehaviour
{
    public List<TextMeshProUGUI> textMeshProUGUIs;

    public List<Image> imageList;

    public List<GameObject> Btns;

    public float duration = 2.0f;

    private Color unSelectedColor = new(1f, 1f, 1f, 1f);

    private Color selectedColor = new(8 / 255f, 64 / 255f, 248 / 255f, 1f);

    public void OnClickBtn(GameObject btn)
    {
        Btns.ForEach(b =>
        {
            int index = Btns.IndexOf(b);

            if (b.name == btn.name)
            {
                StartCoroutine(ChangeColorOverTime(textMeshProUGUIs[index], imageList[index], unSelectedColor, selectedColor, duration));
            }
            else
            {
                StartCoroutine(ChangeColorOverTime(textMeshProUGUIs[index], imageList[index], selectedColor, unSelectedColor, duration));
            }
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
            btn.color = new(tcolorR, tcolorG, tcolorB, 1f);
            float icolorR = Mathf.SmoothStep(textStartColor.r, textColor.r, t);
            float icolorG = Mathf.SmoothStep(textStartColor.g, textColor.g, t);
            float icolorB = Mathf.SmoothStep(textStartColor.b, textColor.b, t);
            text.color = new(icolorR, icolorG, icolorB, 1f);

            yield return null;
        }
        btn.color = btnColor;
        text.color = textColor;
        yield return new WaitForSeconds(4f);
        HideBtn();
    }


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
}


