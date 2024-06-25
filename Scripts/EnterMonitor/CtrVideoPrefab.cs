using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CtrVideoPrefab : MonoBehaviour
{
    public GameObject Prefab;

    public Transform parentTransform; // 指定父对象
    public Vector3 localPosition = new Vector3(0,0,0); // 在父对象的本地坐标中的位置

    public void LoadPrefabSync() => StartCoroutine(LoadPrefab());

    IEnumerator LoadPrefab()
    {
        GameObject instance = Instantiate(Prefab, parentTransform);
        instance.transform.localPosition = localPosition;
        yield return null;
    }
}
