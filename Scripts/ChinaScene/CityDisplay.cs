using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CityDisplay : MonoBehaviour
{
    public float TransformTime = 0.5f;

    public Transform ButtonTransform;
    public float StartBtnScale = 0.5f;

    public Transform VerticalLineTransform;


    public Image LabelImg;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BumpupButton(TransformTime));
        StartCoroutine(ExpandLine(TransformTime));
        StartCoroutine(ChangeAlpha(TransformTime));
    }

    private IEnumerator BumpupButton(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetBtnScale = Mathf.SmoothStep(StartBtnScale, 1, t);
            ButtonTransform.localScale = new Vector3(targetBtnScale, targetBtnScale, 1);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ButtonTransform.localScale = new Vector3(1, 1, 1);
    }


    private IEnumerator ExpandLine(float time)
    {
        float elapsedTime = 0f;
        float scaleX = VerticalLineTransform.localScale.x;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetLineScale = Mathf.SmoothStep(0, 1, t);
            VerticalLineTransform.localScale = new Vector3(scaleX, targetLineScale, 1);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        VerticalLineTransform.localScale = new Vector3(scaleX, 1, 1);
    }

    private IEnumerator ChangeAlpha(float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetAlpha = Mathf.SmoothStep(0, 1, t);
            LabelImg.color = new Color(1f, 1f, 1f, targetAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        LabelImg.color = new Color(1f, 1f, 1f, 1f);
    }
}
