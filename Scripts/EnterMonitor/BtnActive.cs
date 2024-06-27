using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnActive : MonoBehaviour
{
    private Image btnImg;
    private EventTrigger eventTrigger;
    public Coroutine currentEnableCoroutine;

    private void Start()
    {
        btnImg = GetComponentInChildren<Image>();
        eventTrigger = GetComponent<EventTrigger>();

        DisableBtn();
        EnableBtnInSeconds(3);
    }

    public void DisableBtn()
    {
        btnImg.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
        eventTrigger.enabled = false;
    }

    public void DisableBtnInSeconds(float time) =>
        StartCoroutine(CompareAndExec(() =>
        {
            btnImg.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
            eventTrigger.enabled = false;
        }, time));


    public void EnableBtn()
    {
        btnImg.color = new Color(1, 1, 1);
        eventTrigger.enabled = true;
    }

    public Coroutine EnableBtnInSeconds(float time)
    {
        return StartCoroutine(CompareAndExec(() =>
        {
            btnImg.color = new Color(1, 1, 1);
            eventTrigger.enabled = true;
        }, time));
    }


    private IEnumerator CompareAndExec(Action action, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        action();
    }
}
