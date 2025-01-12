using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

