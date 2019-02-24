using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InfoPanel : MonoBehaviour
{
    private float height;
    private TextMeshProUGUI textUi;
    public bool showing = false;
    private float x = 0;
    private const float showSpeed = 2;
    private Vector3 showPos, hidePos;
    private RectTransform rectTrans;
    private void Awake(){
            rectTrans = GetComponent<RectTransform>();
            height = rectTrans.sizeDelta.y;
            textUi = GetComponentInChildren<TextMeshProUGUI>();
            hidePos = rectTrans.anchoredPosition;
            showPos = hidePos - Vector3.up*height;
    }
    void Update()
    {
        if(showing)
            x += showSpeed*Time.deltaTime;
        else
        x -= showSpeed*Time.deltaTime;
        x = Mathf.Clamp(x,0,1);

        Vector3 resultPos = Vector3.Lerp(hidePos, showPos, x);
        rectTrans.anchoredPosition = resultPos;
    }

    public void Show(){
        showing = true;
    }

    public void Show(string text){
        textUi.text = text;
        Show();
    }

    public void Hide(){
        showing = false;
    }
}
