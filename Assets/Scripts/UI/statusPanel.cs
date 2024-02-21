using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusPanel : BasePanel
{
    public Button btnStatus;
    public Button btnBag;
    public Button btnEquip;
    public Button btnSkill;
    public Button btnSetting;
    private void Awake()
    {
        //GameObject go = Instantiate(Resources.Load<GameObject>("Hero/Magic"));
        //HeadPanel.Instance.txtName.text = GameDataMgr.Instance.nowHeroInfo.name;
    }
    public override void Init()
    {
        btnStatus.onClick.AddListener(() =>
        {
            StatusInfoPanel.Instance.ShowMe();

        });
        btnBag.onClick.AddListener(() =>
        {
            BagPanel.Instance.ShowMe();
        });
        btnEquip.onClick.AddListener(() =>
        {
            EquipmentPanel.Instance.ShowMe();

        });
        btnSkill.onClick.AddListener(() =>
        {
            skillPanel.Instance.ShowMe();
        });
        btnSetting.onClick.AddListener(() =>
        {
            BagPanel.Instance.AddToBag(1001);
        });
    }

    
}
