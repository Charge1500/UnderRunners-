using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansHab : Habs
{
    public GameObject sansPortal1;
    public GameObject sansPortal2;
    public Player sansPlayer;
    public Animator animatorPortal1;
    public Animator animatorPortal2;
    public Animator sans;

    public void Parent(){
        
        Vector3 portal2Pos = sansPortal2.transform.position;
        
        sansPortal2.transform.SetParent(null);
        
        sansPortal2.transform.position = portal2Pos;
    }
    public void SansPortal1(){
        sans.SetBool("SansPortalTrue", true);
        sans.SetTrigger("SansPortal");
    }
    public void SansPortal1Bye(){
        animatorPortal1.SetTrigger("Bye");
    }
    public void SansTP(){
        sansPlayer.transform.position=new Vector2(sansPortal2.transform.position.x,sansPortal2.transform.position.y-0.25f);
        sans.SetTrigger("SansPortal2");
    }
    public void SansPortal2Bye(){
        sans.SetBool("SansPortalTrue",false);
        animatorPortal2.SetTrigger("Bye");
        sansPortal2.transform.SetParent(sansPlayer.transform);
        sansPortal2.transform.position= new Vector3(0,0,0);
    }
}
