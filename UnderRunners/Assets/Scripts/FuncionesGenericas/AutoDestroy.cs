using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public void SelfDestroy(){
        Destroy(gameObject);
    }
}
