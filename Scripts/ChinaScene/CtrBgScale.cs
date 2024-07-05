using System.Collections;
using UnityEngine;

public class CtrBgScale : MonoBehaviour
{

    public Vector3 targetLocalPosition = new(-765f, 430f,0);
    public Vector3 targetScale = new(1.8f, 1.8f,1f);


    //´æ´¢³õÊ¼×´Ì¬
    private Vector3 initialPosition;
    private Vector3 initialScale;

    public float duration = 2.0f;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialScale = transform.localScale;
    }


    public void ClickBtnSCale()
    {
        StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, targetScale, duration));
    }

    public void ClickBtnEndSCale()
    {
        StartCoroutine(MoveAndScaleOverTime(initialPosition, initialScale, duration));
    }

    private IEnumerator MoveAndScaleOverTime(Vector3 newLocalPosition, Vector3 newScale, float time)
    {

        Vector3 inPos = transform.localPosition;
        Vector3 inScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetPostionX = Mathf.SmoothStep(inPos.x, newLocalPosition.x, t);
            float targetPostionY = Mathf.SmoothStep(inPos.y, newLocalPosition.y, t);
            transform.localPosition = new Vector3(targetPostionX, targetPostionY, 0);
            float targetScaleX = Mathf.SmoothStep(inScale.x, newScale.x, t);
            float targetScaleY = Mathf.SmoothStep(inScale.y, newScale.y, t);
            transform.localScale = new Vector3(targetScaleX, targetScaleY,1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = newLocalPosition;
        transform.localScale = newScale;
    }


}
