using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateParent : MonoBehaviour
{
    public void DesactivarObjetoPadre()
    {

        transform.parent.gameObject.SetActive(false);
        
    }
}
