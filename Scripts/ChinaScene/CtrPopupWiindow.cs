using System.Collections;
using UnityEngine;

public class CtrPopupWiindow : MonoBehaviour
{
    private Vector3 targetLocalPosition = new(0 , 0, 0);
    public float duration = 2.0f;

    //¼ÇÂ¼³õÊ¼×´Ì¬
    private Vector3 initialPosition;

    public void StartMove()
    {
        StartCoroutine(MoveAndScaleOverTime(targetLocalPosition, duration));
    }

    private void Start()
    {
        initialPosition = transform.localPosition;
}

    public void EndMove()
    {
        StartCoroutine(MoveAndScaleOverTime(initialPosition, duration));
    }

    private IEnumerator MoveAndScaleOverTime(Vector3 newLocalPosition, float time)
    {
       Vector3 inpos = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;

            float targetPostionX = Mathf.SmoothStep(inpos.x, newLocalPosition.x, t);
            transform.localPosition = new Vector3(targetPostionX, 0, 0);
           
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = newLocalPosition;
    }
}
