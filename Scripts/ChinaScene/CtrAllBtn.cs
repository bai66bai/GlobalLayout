using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CtrAllBtn : MonoBehaviour
{
    public List<Transform> BtnTransforms;
    public Transform PopupWindow;
    public List<GameObject> RegionalInformations;
    public Transform BgPanel;
    public TCPClient client;
    public GameObject CtrlBackImage;

    public Button BackBtn;
    //控制第一次点击按钮缓慢缩放
    private bool isFrist = true;

    public void ClickBtn(string name)
    {
        if (!CtrBtnChange.IsFinish) return;
        CtrlScalseAndMove(true);
        BtnTransforms.ForEach(u =>
        {
            CtrBtnChange ctrBtnChange = u.GetComponent<CtrBtnChange>();

            if (name == u.name)
            {
                client.SendMsg($"btnName:{name}");
                int index = BtnTransforms.IndexOf(u);
                ShowInFormations(index);
                ctrBtnChange.SelectBtn(true);
            }
            else
            {
                    ctrBtnChange.UnSelectedBtn(isFrist);
            }
        });
        isFrist = false;
    }



    private void Update()
    {
        
        bool isAllBtnComplete = true;
        BtnTransforms.ForEach(u =>
        {
            if (u.gameObject.GetComponent<CtrBtnChange>().ChangeFinish == false)
            {
                isAllBtnComplete = false;
            }            
        });
        if (!isAllBtnComplete)
        {
            CtrlBackImage.SetActive(false);
        }

        //CtrPopupWiindow.IsFinish 判断弹窗内容交换协程是否执行完成
        //CtrBgScale.ScaleFinish 判断背景放大协程执行完毕
        if (isAllBtnComplete && CtrPopupWiindow.IsFinish && CtrBgScale.ScaleFinish)
        {
            if (CtrBgScale.ScaleExpand)
                CtrlBackImage.SetActive(true);
            else
            {
                CtrlBackImage.SetActive(false);
                BackBtn.enabled = true;
            }
        }
        else
        {
            CtrlBackImage.SetActive(false);
        }
    }


    /// <summary>
    /// 控制还原当前国家界面
    /// </summary>
    public void CtrlRecovery()
    {
        client.SendMsg($"operationName:click");
        CtrlScalseAndMove(false);
        BtnTransforms.ForEach(u =>
        {
            CtrBtnChange ctrBtnChange = u.GetComponent<CtrBtnChange>();
            int index = BtnTransforms.IndexOf(u);
            ctrBtnChange.SelectBtn(false);
        });
    }

    /// <summary>
    /// 控制屏幕缩放以及弹出窗的出现
    /// </summary>
    /// <param name="IsStart">判断是弹出还是恢复</param>
    private void CtrlScalseAndMove(bool IsStart)
    {
        if (IsStart)
        {
            PopupWindow.GetComponent<CtrPopupWiindow>().StartMove();
            BgPanel.GetComponent<CtrBgScale>().ClickBtnSCale();
        }
        else
        {
            PopupWindow.GetComponent<CtrPopupWiindow>().StartMoveEnd();
            BgPanel.GetComponent<CtrBgScale>().ClickBtnSCaleEnd();
        }
    }

    /// <summary>
    /// 判断当前显示的是什么内容
    /// </summary>
    /// <param name="index">根据对应的按钮索引</param>
    public void ShowInFormations(int index)
    {
        RegionalInformations.ForEach(u =>
        {
            int sindex = RegionalInformations.IndexOf(u);
            u.SetActive(index == sindex);
        });
    }
}