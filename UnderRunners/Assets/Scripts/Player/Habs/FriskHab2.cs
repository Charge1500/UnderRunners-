using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriskHab2 : MonoBehaviour
{
    public GameObject copyFrisk;
    public void Activate(){
        copyFrisk.SetActive(true);
        gameObject.SetActive(false);
    }
}
