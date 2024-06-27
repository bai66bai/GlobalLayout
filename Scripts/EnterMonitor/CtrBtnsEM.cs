using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Drawing;

public class CtrBtnsEM : MonoBehaviour
{
    public List<GameObject> Btns;
    public List<GameObject> Contents;

    public GameObject Suzhou;
    public GameObject Hangzhou;

    public TCPClient client;

    [HideInInspector]
    public bool IsReleasingSuZhou = false;
    [HideInInspector]
    public bool IsReleasingHangZhou = false;

    private BtnActive[] btnActivesScripts;

    private void Start()
    {
        changeStyle(TabStore.SelectedTab);
        changeContent(TabStore.SelectedTab);
        btnActivesScripts = FindObjectsOfType<BtnActive>();
        ForbiddenBtn();
        EnableBtn();
    }

    public void OnClickBtn(string name)
    {
        ForbiddenBtn();
        EnableBtn();
        foreach (var btn in btnActivesScripts)
        {
            btn.DisableBtn();
            btn.EnableBtnInSeconds(3);
        }
        Btns.ForEach(b =>
        {           
            int index = Btns.IndexOf(b);
            if (b.name == name)
            {
                client.SendMsg($"btnName:{name}");
                changeStyle(index);
                changeContent(index);
            }
        });
    }


    /// <summary>
    /// 控制按钮的样式
    /// </summary>
    /// <param name="index"></param>
    private void changeStyle(int index)
    {
        Btns.ForEach(b =>
        {
            int bindex = Btns.IndexOf(b);
            if (bindex == index)
            {
                TextMeshProUGUI text = b.GetComponentInChildren<TextMeshProUGUI>();
                text.color = UnityEngine.Color.white;
                Image[] images = b.GetComponentsInChildren<Image>();
                images[0].enabled = true;
                images[1].enabled = false;
            }
            else
            {
                TextMeshProUGUI text = b.GetComponentInChildren<TextMeshProUGUI>();
                text.color = new UnityEngine.Color(47 / 255f, 92 / 255f, 197 / 255f, 1f);
                Image[] images = b.GetComponentsInChildren<Image>();
                images[1].enabled = true;
                images[0].enabled = false;
            }
        });
    }


    void Update()
    {
 
        if (IsReleasingHangZhou || IsReleasingSuZhou)
        {
            bool hasAllVlcReleased = true;
            foreach (Transform child in IsReleasingSuZhou ? Suzhou.transform : Hangzhou.transform)
            {
                VLCPlayerExample[] vLCPlayerExamples = child.gameObject.GetComponentsInChildren<VLCPlayerExample>();
                foreach (var item in vLCPlayerExamples)
                {
                    if(!item.HasDestroyed)
                    {
                        hasAllVlcReleased = false;
                        break;
                    }
                }                

                if(hasAllVlcReleased)
                {
                    Destroy(child.gameObject);
                    IsReleasingHangZhou = false;
                    IsReleasingSuZhou = false;
                }
            }


        }
    }

    private void changeContent(int index)
    {
        if (index == 0)
        {
            VLCPlayerExample[] vLCPlayer1Examples = Hangzhou.GetComponentsInChildren<VLCPlayerExample>();
            foreach (var item in vLCPlayer1Examples)
            {
                item.DestoryVLCPlayer();
            }
            Contents[0].SetActive(true);
            Suzhou.GetComponent<CtrVideoPrefab>().LoadPrefabSync();
            IsReleasingHangZhou = true;
            IsReleasingSuZhou = false;
        }
        else
        {

            VLCPlayerExample[] vLCPlayer1Examples = Suzhou.GetComponentsInChildren<VLCPlayerExample>();
            foreach (var item in vLCPlayer1Examples)
            {
                item.DestoryVLCPlayer();
            }
            Contents[1].SetActive(true);
           Hangzhou.GetComponent<CtrVideoPrefab>().LoadPrefabSync();
            IsReleasingSuZhou = true;
            IsReleasingHangZhou = false;
        }
    }

    private void ForbiddenBtn()
    {
        Btns.ForEach(b =>
        {
            b.GetComponent<EventTrigger>().enabled = false;
            Image[] images = b.GetComponentsInChildren<Image>();
            images[0].color = new UnityEngine.Color(170 / 255f, 170 / 255f, 170 / 255f);
            images[1].color = new UnityEngine.Color(170 / 255f, 170 / 255f, 170 / 255f);
        });
    }

    private void EnableBtn()
    {
        StartCoroutine(ExecuteAfterTime(() => Btns.ForEach(b =>
        {
            b.GetComponent<EventTrigger>().enabled = true;
            Image[] images = b.GetComponentsInChildren<Image>();
            images[0].color = new UnityEngine.Color(1, 1, 1);
            images[1].color = new UnityEngine.Color(1, 1, 1);
        }), 3));
    }

    private IEnumerator ExecuteAfterTime(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
