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
    //���Ƶ�һ�ε����ť��������
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

        //CtrPopupWiindow.IsFinish �жϵ������ݽ���Э���Ƿ�ִ�����
        //CtrBgScale.ScaleFinish �жϱ����Ŵ�Э��ִ�����
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
    /// ���ƻ�ԭ��ǰ���ҽ���
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
    /// ������Ļ�����Լ��������ĳ���
    /// </summary>
    /// <param name="IsStart">�ж��ǵ������ǻָ�</param>
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
    /// �жϵ�ǰ��ʾ����ʲô����
    /// </summary>
    /// <param name="index">���ݶ�Ӧ�İ�ť����</param>
    public void ShowInFormations(int index)
    {
        RegionalInformations.ForEach(u =>
        {
            int sindex = RegionalInformations.IndexOf(u);
            u.SetActive(index == sindex);
        });
    }
}