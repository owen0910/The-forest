using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillItem : MonoBehaviour
{
    private int id;
    public skillInfo info;
    public Image imgSkill;
    public Text txtName;
    public Text txtApplyType;
    public Text txtDes;
    public Text txtMp;
    public GameObject skillMask;
    /// <summary>
    /// 更新显示技能信息
    /// </summary>
    /// <param name="id"></param>
    public void SetId(int id)
    {
        skillMask.gameObject.SetActive(false);
        this.id = id;
        info = GameDataMgr.Instance.GetSkillInfoById(id);
        imgSkill.sprite = Resources.Load<Sprite>("Img/" + info.icon_name);
        txtName.text = info.name;
        switch (info.ApplyType)
        {
            case "Passive":
                txtApplyType.text = "增益";
                break;
            case "Buff":
                txtApplyType.text = "增强";
                break;
            case "SingleTarget":
                txtApplyType.text = "单个目标";
                break;
            case "MultiTarget":
                txtApplyType.text = "群体技能";
                break;

        }
        txtDes.text = info.des;
        txtMp.text = info.mp.ToString();
    }
    public void SetLevel(int level)
    {
        //技能可用
        if (info.level<=level)
        {
            skillMask.gameObject.SetActive(false);
            imgSkill.GetComponent<SkillItemIcon>().enabled = true;

        }
        else
        {
            skillMask.gameObject.SetActive(true);
            imgSkill.GetComponent<SkillItemIcon>().enabled = false
                ;
        }
    }

}
