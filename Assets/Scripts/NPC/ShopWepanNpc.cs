using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWepanNpc : NpcBase
{
    public AudioSource audio;
    //�����λ�����colider֮�ϵ�ʱ�򣬻���ÿһ֡�����������
    private void OnMouseOver()
    {
        //�������ҩ��С���
        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
            ShopWepanPanel.Instance.ShowMe();

        }
    }

}
