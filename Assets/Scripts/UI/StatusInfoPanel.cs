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
    //ʣ�����
    public Text txtRemain;
    //�ܽ�
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
        txtSum.text = "�˺���" + (playerStatus.attack + playerStatus.attack_plus) + "  " + 
                      "������" +(playerStatus.def + playerStatus.def_plus) + "  " + 
                      "�ٶȣ�" + (playerStatus.speed+ playerStatus.speed_plus);
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
