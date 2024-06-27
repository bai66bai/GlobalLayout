using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Turnthepage : MonoBehaviour
{

    public List<GameObject> Btns;

    public SwipeDetection SwipeDetection;

    private Image[] Lefts;
    private Image[] Rights;
    private int endIndex;
    private void Start()
    {
        endIndex = (int) SwipeDetection.swipeArea.rect.width / SwipeDetection.MovingDistance;
        Lefts = Btns[0].GetComponentsInChildren<Image>();
        Rights = Btns[1].GetComponentsInChildren<Image>();
        changBtn();

    }
    public void Left()
    {
        SwipeDetection.ToRight();
    }

    public void Right()
    {
        SwipeDetection.ToLeft();
    }


    public void changBtn()
    {
        int index = SwipeDetection.nowIndex;
        ChangeBtnColor(index);

    }

    public void ChangeBtnColor(int index)
    {
        
        if (index == 0 && index != endIndex - 1)
        {
            Lefts[1].enabled = false;
            Lefts[2].enabled = true;
            Rights[1].enabled = true;
            Rights[2].enabled = false;
        }
        else if (index > 0 && index < endIndex - 1)
        {
            Lefts[1].enabled = true;
            Lefts[2].enabled = false;
            Rights[1].enabled = true;
            Rights[2].enabled = false;
        }
        else if (index == endIndex - 1)
        {
            Lefts[1].enabled = true;
            Lefts[2].enabled = false;
            Rights[1].enabled = false;
            Rights[2].enabled = true;
        }
        else if (index == endIndex - 1)
        {
            Lefts[1].enabled = false;
            Lefts[2].enabled = true;
            Rights[1].enabled = false;
            Rights[2].enabled = true;
        }
    }


}