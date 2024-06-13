using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrText : MonoBehaviour
{
    private Quaternion initialRotation;
    private Transform parentTransform;

    void Start()
    {
        // 记录文字对象的初始旋转
        initialRotation = transform.rotation;
        // 获取父对象的Transform
        parentTransform = transform.parent;
    }

    void LateUpdate()
    {
        // 保持文字对象的旋转相对于世界坐标系不变
        transform.rotation = initialRotation;
    }
}
