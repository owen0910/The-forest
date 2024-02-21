using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNpc : NpcBase
{
    public bool isFinishTask=false;
    public AudioSource audio;
    //�����λ�����colider֮�ϵ�ʱ�򣬻���ÿһ֡�����������
    private void OnMouseOver()
    {
        //���������үү
        if (Input.GetMouseButtonDown(0))
        {
            if (!taskPanel.Instance.isFinish())
            {
                UIManager.Instance.ShowPanel<menuPanel>(false);
                audio.Play();
            }

            else
            {
                UIManager.Instance.ShowPanel<menuFinishPanel>(false);
                audio.Play();
                taskPanel.Instance.HideMe();
            }
            

        }
    }
}
