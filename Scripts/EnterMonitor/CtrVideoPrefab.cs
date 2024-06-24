using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrVideoPrefab : MonoBehaviour
{
    public GameObject Prefab;

    public Transform parentTransform; // ָ��������
    public Vector3 localPosition = new Vector3(0,0,0); // �ڸ�����ı��������е�λ��

    void Start()
    {
        StartCoroutine(LoadPrefab());
    }

    IEnumerator LoadPrefab()
    {
        // ģ��һЩ�ӳ٣�����ȴ�ĳЩ��Դ�������
        yield return new WaitForSeconds(0.5f);

        // ��ָ����������ʵ����Ԥ����
        GameObject instance = Instantiate(Prefab, parentTransform);
        // ����Ԥ�����ڸ������еı���λ�ú���ת
        instance.transform.localPosition = localPosition;
    }
}
