using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrRotation : MonoBehaviour
{
    // 旋转速度（度/秒）
    public float rotationSpeed = 45.0f;

    // 控制正反方向
    public bool rotateClockwise = true;

    void Update()
    {
        // 根据 rotateClockwise 决定旋转方向
        float direction = rotateClockwise ? 1 : -1;

        // 每帧绕Z轴旋转
        transform.Rotate(0, 0, direction * rotationSpeed * Time.deltaTime);
    }
}
