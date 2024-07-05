using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrAllBtn : MonoBehaviour
{
    public List<Transform> BtnTransforms;
    public Transform PopupWindow;
    public List<GameObject> RegionalInformations;
    public Transform BgPanel;
    public TCPClient client;
    //控制第一次点击按钮缓慢缩放
    private bool isFrist = true;

    public GameObject CtrlImage;


    public void ClickBtn(string name)
    {
        CtrPopupWIndow(true);
        BtnTransforms.ForEach(u =>
        {
            CtrBtnChange ctrBtnChange = u.GetComponent<CtrBtnChange>();

            if(name == u.name)
            {
                CtrlImage.SetActive(true);
                client.SendMsg($"btnName:{name}");
                int index = BtnTransforms.IndexOf(u);
                ShowInFormations(index);
                ctrBtnChange.SelectBtn();
            }
            else
            {
                ctrBtnChange.UnSelectedBtn(isFrist);
            }
        });
        isFrist = false;
    }


    /// <summary>
    /// 控制国家布局返未选中状态
    /// </summary>
    public void CtrlreductionInterface()
    {
        CtrlImage.SetActive(false);
        CtrPopupWIndow(false);
       // isFrist = true;
    }


    /// <summary>
    /// 控制弹出窗的显示,以及背景图的缩放
    /// </summary>
    private void CtrPopupWIndow(bool IsStart)
    {
        if (IsStart)
        {
            PopupWindow.GetComponent<CtrPopupWiindow>().StartMove();
            BgPanel.GetComponent<CtrBgScale>().ClickBtnSCale();
        }
        else
        {
            PopupWindow.GetComponent<CtrPopupWiindow>().EndMove();
            BgPanel.GetComponent<CtrBgScale>().ClickBtnEndSCale();
        }
    }




    public void ShowInFormations(int index)
    {
        RegionalInformations.ForEach(u =>
        {
            int sindex = RegionalInformations.IndexOf(u);
            u.SetActive(index == sindex);
        });
    }
}
