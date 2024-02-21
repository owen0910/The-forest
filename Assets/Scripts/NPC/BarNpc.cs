using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNpc : NpcBase
{
    public bool isFinishTask=false;
    public AudioSource audio;
    //当鼠标位于这个colider之上的时候，会在每一帧调用这个方法
    private void OnMouseOver()
    {
        //当点击了老爷爷
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
