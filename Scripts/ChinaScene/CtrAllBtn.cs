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
    private bool IsStart = false;
    //控制第一次点击按钮缓慢缩放
    private bool isFrist = true;


    public void ClickBtn(string name)
    {
        if (!IsStart)
        {
            PopupWindow.GetComponent<CtrPopupWiindow>().StartScale();
            BgPanel.GetComponent<CtrBgScale>().ClickBtnSCale();
            IsStart = true;
        }
        BtnTransforms.ForEach(u =>
        {
            CtrBtnChange ctrBtnChange = u.GetComponent<CtrBtnChange>();

            if(name == u.name)
            {
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



    public void ShowInFormations(int index)
    {
        RegionalInformations.ForEach(u =>
        {
            int sindex = RegionalInformations.IndexOf(u);
            u.SetActive(index == sindex);
        });
    }
}
