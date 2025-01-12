using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapyrusHab : MonoBehaviour
{
    public Player papyrus;
    public GameObject habEffect;

    void Update(){
        if(papyrus.currentColdown==2){
            transform.SetParent(papyrus.transform);
            habEffect.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
