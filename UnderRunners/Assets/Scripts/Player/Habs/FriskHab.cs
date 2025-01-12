using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriskHab : MonoBehaviour
{
    public Player frisk;
    public GameObject copyFrisk;

    public void Parent()
    {
        Vector3 friskCopyPos = copyFrisk.transform.position;
        copyFrisk.transform.SetParent(null);
        copyFrisk.transform.position = friskCopyPos;
    }

    public void Activate(){
        copyFrisk.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Update(){
        if(frisk.currentColdown==1){
            transform.SetParent(frisk.transform);
            transform.position=new Vector3(frisk.transform.position.x+0.5f,frisk.transform.position.y,0);
            gameObject.SetActive(false);
        }
    }
}
