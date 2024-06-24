using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrVideoPrefab : MonoBehaviour
{
    public GameObject Prefab;

    public Transform parentTransform; // 指定父对象
    public Vector3 localPosition = new Vector3(0,0,0); // 在父对象的本地坐标中的位置

    void Start()
    {
        StartCoroutine(LoadPrefab());
    }

    IEnumerator LoadPrefab()
    {
        // 模拟一些延迟，例如等待某些资源加载完成
        yield return new WaitForSeconds(0.5f);

        // 在指定父对象下实例化预制体
        GameObject instance = Instantiate(Prefab, parentTransform);
        // 设置预制体在父对象中的本地位置和旋转
        instance.transform.localPosition = localPosition;
    }
}
