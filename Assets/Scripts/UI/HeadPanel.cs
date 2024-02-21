using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadPanel : DataBasePanel<HeadPanel>
{
    public Text txtName;
    public Image imgHP;
    public Image imgMP;
    private PlayerStatus ps;
    public Button btnBig;
    public Button btnSmall;
    private Camera miniCamera;
    public Image imgExp;

    public override void Init()
    {
        miniCamera = GameObject.FindGameObjectWithTag("miniMap").GetComponent<Camera>();
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow();
        btnBig.onClick.AddListener(() =>
        {
            miniCamera.orthographicSize--;
        });
        btnSmall.onClick.AddListener(() =>
        {
            miniCamera.orthographicSize++;
        });
    }

    public void UpdateShow()
    {
        txtName.text = "Lv." + ps.grade + " " + GameDataMgr.Instance.nowHeroInfo.name;
        imgHP.fillAmount = (float)ps.nowHp / ps.hp;
        imgMP.fillAmount = (float)ps.nowMp / ps.mp;

    }
    
}
