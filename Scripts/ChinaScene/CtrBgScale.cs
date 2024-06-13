using System.Collections;
using UnityEngine;

public class CtrBgScale : MonoBehaviour
{

    private Vector3 targetLocalPosition = new(-765f, 430f,0);
    private Vector3 targetScale = new(1.8f, 1.8f,1f);

    public float duration = 2.0f;

    public void ClickBtnSCale()
    {
        StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, targetScale, duration));
    }

    private IEnumerator MoveAndScaleOverTime(Vector3 newLocalPosition, Vector3 newScale, float time)
    {
        Vector3 initialPosition = transform.localPosition;
        Vector3 initialScale = transform.localScale;

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetPostionX = Mathf.SmoothStep(initialPosition.x, newLocalPosition.x, t);
            float targetPostionY = Mathf.SmoothStep(initialPosition.y, newLocalPosition.y, t);
            transform.localPosition = new Vector3(targetPostionX, targetPostionY, 0);
            float targetScaleX = Mathf.SmoothStep(initialScale.x, newScale.x, t);
            float targetScaleY = Mathf.SmoothStep(initialScale.y, newScale.y, t);
            transform.localScale = new Vector3(targetScaleX, targetScaleY,1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = newLocalPosition;
        transform.localScale = newScale;
    }


}
