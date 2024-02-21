using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuFinishPanel : BasePanel
{
    public Text txtInfo;
    public Button btnSure;
    public Button btnClose;
    public Animator animator;

    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<menuFinishPanel>();
            //¼ÓÇ®
            BagPanel.Instance.AddCoin(1000);
        });


        btnClose.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<menuFinishPanel>();
        });


    }

    public override void ShowMe(bool isfade)
    {
        base.ShowMe(isfade);
        animator.SetBool("isshow", true);
    }


}
