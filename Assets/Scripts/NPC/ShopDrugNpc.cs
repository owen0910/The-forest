using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrugNpc : NpcBase
{

    public AudioSource audio;
    //当鼠标位于这个colider之上的时候，会在每一帧调用这个方法
    private void OnMouseOver()
    {
        //当点击了药店小姐姐
        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
            ShopPanel.Instance.ShowMe();

        }
    }
}
