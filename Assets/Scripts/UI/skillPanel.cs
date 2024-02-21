using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillPanel : DataBasePanel<skillPanel>
{
    public Transform content;
    private List<skillInfo> skillInfoList = new List<skillInfo>();
    private PlayerStatus ps;
    public Transform canvas;
    public Button btnClose;
    public override void Init()
    {
        InitSkill();
        HideMe();
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
        });
    }

    public void InitSkill()
    {
        skillInfoList.Clear(); ;
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        switch (ps.heroType)
        {
            case HeroType.SwordMan:
                for (int i = 4001; i < 4007; i++)
                {
                    skillInfoList.Add(GameDataMgr.Instance.GetSkillInfoById(i));
                }
                break;
            case HeroType.Magician:
                for (int i = 5001; i < 5007; i++)
                {
                    skillInfoList.Add(GameDataMgr.Instance.GetSkillInfoById(i));
                }
                break;
        }
        for (int i = 0; i < skillInfoList.Count; i++)
        {
            GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/skillItem"));
            gameObject.transform.SetParent(content,false);
            gameObject.transform.localPosition = Vector3.zero;
            //gameObject.transform.localScale = Vector3.one;
            skillItem item = gameObject.GetComponent<skillItem>();
            item.SetId(skillInfoList[i].id);
            item.SetLevel(ps.grade);
            
        }
    }

    

    
}
