using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrugNpc : NpcBase
{

    public AudioSource audio;
    //�����λ�����colider֮�ϵ�ʱ�򣬻���ÿһ֡�����������
    private void OnMouseOver()
    {
        //�������ҩ��С���
        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
            ShopPanel.Instance.ShowMe();

        }
    }
}
