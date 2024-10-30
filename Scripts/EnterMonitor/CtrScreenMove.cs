using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CtrScreenMove : MonoBehaviour
{
    public List<GameObject> Screens;

    public float duration = 2.0f;

    public int VId;

    private TCPClient client;

    private bool isOutOver = true;

    private void Start()
    {
        GameObject LevelLoader = GameObject.Find("LevelLoader");
        client = LevelLoader.GetComponent<TCPClient>();
        Screens.Sort((a,b) =>a.GetComponent<ScreenIndex>().Index.CompareTo(b.GetComponent<ScreenIndex>().Index));
    }

    /// <summary>
    /// ��ȡ����ǰ����Ļ����index �Լ�����λ������
    /// </summary>
    /// <param name="name">�������Ļ�����</param>
    public void OnClickScreen(string name)
    {
        if (isOutOver)
        {
            Screens.ForEach(t =>
            {
                if (t.name == name)
                {

                    var targetTextMesh = t.GetComponentInChildren<TextMeshProUGUI>();
                    var currentTextMesh = Screens[0].GetComponentInChildren<TextMeshProUGUI>();
                    (currentTextMesh.text, targetTextMesh.text) = (targetTextMesh.text, currentTextMesh.text);

                    var targetScreenIndex = t.GetComponent<ScreenIndex>();
                    var currentScreenIndex = Screens[0].GetComponent<ScreenIndex>();
                    (currentScreenIndex.Name, targetScreenIndex.Name) = (targetScreenIndex.Name, currentScreenIndex.Name);

                    client.SendMsg($"screenName:{VId}-{currentScreenIndex.Name}");

                    t.GetComponent<ScreenIndex>().Index = 0;
                }
            });
        }
    }

}
