using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CtrBtnChange : MonoBehaviour
{
    public float duration = 0.6f;

    public float SwitchingTime = 0.15f;

    public float TargetY;

    public Image VerticalLine;

    public TextMeshProUGUI SelectedText;

    public TextMeshProUGUI UnSelectedText;

    public GameObject Label;
 

    private Vector2 UnSelectedtLocalScale = new(0.4f, 0.6f);

    private Vector2 selectedtLocalScale;

    private Vector3 selectedtLocalPosition;

    private Vector3 targetLocalPosition;

    private Image[] Labels;

    private Transform Btn; //°´Å¥

    private bool isFinish = false;

    private void Start()
    {

        Btn = transform.Find("Btn");
        selectedtLocalScale = Btn.transform.localScale;
        selectedtLocalPosition = Btn.transform.localPosition;
        targetLocalPosition = new(Btn.localPosition.x, TargetY, 0);
        Labels = Label.GetComponentsInChildren<Image>();
    }

    

    public void SelectBtn()
    {
        SelectedText.enabled = true;
        UnSelectedText.enabled = false;
        Labels[0].enabled = true;
        Labels[1].enabled = false;
        isFinish = true;
        StartCoroutine(ChangeBtnActive(selectedtLocalScale, selectedtLocalPosition, SwitchingTime));
    }


    public void UnSelectedBtn(bool isFrist)
    {
        VerticalLine.enabled = false;
        Labels[1].enabled = true;
        Labels[0].enabled = false;
        SelectedText.enabled = false;
        UnSelectedText.enabled = true;
        float time = isFrist ? duration : SwitchingTime;
        StartCoroutine(ChangeBtnActive(UnSelectedtLocalScale, targetLocalPosition, time));
    }

 


    private IEnumerator ChangeBtnActive(Vector2 newlocalScale, Vector3 newlocalPosition, float time)
    {
        Vector2 initialLocalScale = Btn.localScale;
        Vector3 initialLocalPosition = Btn.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            float targetLocalScaleX = Mathf.SmoothStep(initialLocalScale.x, newlocalScale.x, t);
            float targetLocalScaleY = Mathf.SmoothStep(initialLocalScale.y, newlocalScale.y, t);
            float targetPostionX = Mathf.SmoothStep(initialLocalPosition.x, initialLocalPosition.x, t);
            float targetPostionY = Mathf.SmoothStep(initialLocalPosition.y, newlocalPosition.y, t);
            Btn.localScale = new Vector2(targetLocalScaleX, targetLocalScaleY);
            Btn.localPosition = new Vector3(targetPostionX, targetPostionY, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (isFinish)
        {
            VerticalLine.enabled = true;
            isFinish = false;
        }
        Btn.localScale = newlocalScale;
        Btn.localPosition = newlocalPosition;
    }
}
