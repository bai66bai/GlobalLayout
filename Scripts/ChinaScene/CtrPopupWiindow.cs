using System.Collections;
using UnityEngine;

public class CtrPopupWiindow : MonoBehaviour
{
    private Vector3 targetLocalPosition = new(0 , 0, 0);
    public float duration = 2.0f;


    public void StartScale()
    {
        StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, duration));
    }

    private IEnumerator MoveAndScaleOverTime(Vector3 newLocalPosition, float time)
    {
        Vector3 initialPosition = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetPostionX = Mathf.SmoothStep(initialPosition.x, newLocalPosition.x, t);
            transform.localPosition = new Vector3(targetPostionX, 0, 0);
           
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = newLocalPosition;
    }
}
