using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskPanel : DataBasePanel<taskPanel>
{
    public Text txtInfo;
    public Text taskNum;
    public int nowNum = 0;
    public int MaxNum = 10;

    public void SetInfo(string textInfo,int now,int max)
    {
        this.txtInfo.text = textInfo;
        nowNum = now;
        MaxNum = max;
        this.taskNum.text = nowNum + "/" + MaxNum;
    }

    public void AddNum(int num=1)
    {
        nowNum += num;
        this.taskNum.text = nowNum+"/"+MaxNum;
    }

    public override void Init()
    {
        HideMe();
    }

    public bool isFinish()
    {
        if (nowNum>=MaxNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
