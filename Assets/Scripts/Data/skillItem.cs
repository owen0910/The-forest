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
    /// ������ʾ������Ϣ
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
                txtApplyType.text = "����";
                break;
            case "Buff":
                txtApplyType.text = "��ǿ";
                break;
            case "SingleTarget":
                txtApplyType.text = "����Ŀ��";
                break;
            case "MultiTarget":
                txtApplyType.text = "Ⱥ�弼��";
                break;

        }
        txtDes.text = info.des;
        txtMp.text = info.mp.ToString();
    }
    public void SetLevel(int level)
    {
        //���ܿ���
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
