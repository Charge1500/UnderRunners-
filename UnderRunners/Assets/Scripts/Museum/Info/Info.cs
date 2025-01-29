using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class Info : MonoBehaviour
{
    public InfoObjects info;
    protected string infoTextString;

    void Awake(){
        info=GetComponentInParent<InfoObjects>();
    }
    void OnTriggerEnter2D(Collider2D someone)
    {
        info.panelInfo.SetActive(true);
        info.infoText.text = infoTextString;
    }
    void OnTriggerExit2D(Collider2D someone)
    {
        info.panelInfo.SetActive(false);
    }

}
