using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuPanel : BasePanel
{
    public Text txtInfo;
    public Button btnSure;
    public Button btnCanel;
    public Button btnClose;
    public Animator animator;

    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<menuPanel>();
            taskPanel.Instance.ShowMe();
        });

        btnCanel.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<menuPanel>();
            taskPanel.Instance.HideMe();

        });

        btnClose.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<menuPanel>();
        });


    }

    public override void ShowMe(bool isfade)
    {
        base.ShowMe(isfade);
        animator.SetBool("isshow", true);
    }


}
