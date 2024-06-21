using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrPopWindow : MonoBehaviour
{
    public float duration = 2.0f;
    private Vector3 targetLocalPosition = new(0, 0, 0);

    private Vector3 initialLocalPosition;

    private bool isRemove = false;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
    }
    public void StartRemove()
    {
        StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, duration));
        isRemove = true;
    }

    public void ReturnMove()
    {
        if (isRemove)
        {
            StartCoroutine(MoveAndScaleOverTime(initialLocalPosition, duration));
        }
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
