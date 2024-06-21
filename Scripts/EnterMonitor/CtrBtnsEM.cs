using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CtrBtnsEM : MonoBehaviour
{
    public List<GameObject> Btns;
    public List<GameObject> Contents;

    private void Start()
    {
            changeStyle(TabStore.SelectedTab);
            changeContent(TabStore.SelectedTab);
    }

    public void OnClickBtn(string name)
    {
        Btns.ForEach(b =>
        {
            int index = Btns.IndexOf(b);
            if (b.name == name)
            {
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
                text.color = Color.white;
                Image[] images = b.GetComponentsInChildren<Image>();
                images[0].enabled = true;
                images[1].enabled = false;
            }
            else
            {
                TextMeshProUGUI text = b.GetComponentInChildren<TextMeshProUGUI>();
                text.color = new Color(47 / 255f, 92 / 255f, 197 / 255f, 1f);
                Image[] images = b.GetComponentsInChildren<Image>();
                images[1].enabled = true;
                images[0].enabled = false;
            }
        });
    }

    private void changeContent(int index)
    {
        if(index == 0)
        {
            Contents[0].SetActive(true);
            Contents[1].SetActive(false);
        }
        else
        {
            Contents[0].SetActive(false);
            Contents[1].SetActive(true);

        }
    }
}
