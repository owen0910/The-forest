using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusInfoPanel : DataBasePanel<StatusInfoPanel>
{
   
    public Text txtHit;
    public Text txtDef;
    public Text txtSpeed;

    public Button btnHit;
    public Button btnDef;
    public Button btnSpeed;
    public Button btnClose;
    //剩余点数
    public Text txtRemain;
    //总结
    public Text txtSum;
    private PlayerStatus playerStatus;

    public override void Init()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        btnHit.onClick.AddListener(() =>
        {
            bool success = playerStatus.GetPoint();
            if (success)
            {
                playerStatus.attack_plus++;
                UpdateShow();
            }
        });

        btnDef.onClick.AddListener(() =>
        {
            bool success = playerStatus.GetPoint();
            if (success)
            {
                playerStatus.def_plus++;
                UpdateShow();
            }
        });

        btnSpeed.onClick.AddListener(() =>
        {
            bool success = playerStatus.GetPoint();
            if (success)
            {
                playerStatus.speed_plus++;
                UpdateShow();
            }
        });
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
        });
        HideMe();
    }
   
    public void UpdateShow()
    {
        txtHit.text = playerStatus.attack + "+" + playerStatus.attack_plus;
        txtDef.text = playerStatus.def + "+" + playerStatus.def_plus;
        txtSpeed.text = playerStatus.speed + "+" + playerStatus.speed_plus;

        txtRemain.text = playerStatus.point_remain.ToString();
        txtSum.text = "伤害：" + (playerStatus.attack + playerStatus.attack_plus) + "  " + 
                      "防御：" +(playerStatus.def + playerStatus.def_plus) + "  " + 
                      "速度：" + (playerStatus.speed+ playerStatus.speed_plus);
        if (playerStatus.point_remain>0)
        {
            btnHit.gameObject.SetActive(true);
            btnDef.gameObject.SetActive(true);
            btnSpeed.gameObject.SetActive(true);
        }
        else
        {
            btnHit.gameObject.SetActive(false);
            btnDef.gameObject.SetActive(false);
            btnSpeed.gameObject.SetActive(false);
        }
    }

    
    
}
