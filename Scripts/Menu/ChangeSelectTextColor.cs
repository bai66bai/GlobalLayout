using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeSelectTextColor : MonoBehaviour
{
   public List<TextMeshProUGUI> TextMeshProUGUIs;

    private Color defultColor = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    private Color selectColor = new Color(3 / 255f, 117 / 255f, 230 / 255f);

    public void ChangeColor(TextMeshProUGUI texts)
    {
        TextMeshProUGUIs.ForEach(text =>
        {

            text.color = texts == text ? selectColor : defultColor;      
        });
    }

}
