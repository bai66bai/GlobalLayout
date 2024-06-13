using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrScale : MonoBehaviour
{
    // 缩放范围
    public float minScale = 1.0f;
    public float maxScale = 1.25f;
    public float delaySeconds = 1.0f;

    // 缩放速度
    public float scaleSpeed = 1.0f;

    private RectTransform rectTransform;

    void Start()
    {
        // 获取 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Time.time < delaySeconds) return;
        // 计算缩放值
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong((Time.time - delaySeconds) * scaleSpeed, 1.0f));

        // 设置缩放
        rectTransform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
