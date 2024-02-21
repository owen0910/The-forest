using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShortCutType
{
    Skill,
    Drug,
    None

}
public class shortCutGrid : MonoBehaviour
{
    private int id;
    public KeyCode keyCode;
    public Image image;
    private ShortCutType type;
    private skillInfo skillInfo;
    private ObjectInfo objectInfo;
    private PlayerStatus ps;
    private PlayerAttack playerAttack;

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        image.gameObject.SetActive(false);
        playerAttack= GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
    }


    private void Update()
    {
        if (ShopWepanPanel.Instance.gameObject.activeSelf)
        {
            return;
        }
        if (ShopPanel.Instance.gameObject.activeSelf)
        {
            return;
        }
        if (playerAttack.state==HeroState.Death)
        {
            return;
        }
        if (Input.GetKeyDown(keyCode))
        {
            if (type==ShortCutType.Drug)
            {
                OnDrugUse();
            }
            else if (type==ShortCutType.Skill)
            {
                //释放技能
                bool success = ps.TakeMP(skillInfo.mp);
                if (success==false)
                {
                    //没蓝了
                }
                else
                {
                    //释放技能
                    playerAttack.UseSkill(skillInfo);
                }
            }
        }
    }
    public void SetSkill(int id)
    {
        this.id = id;
        type = ShortCutType.Skill;
        image.gameObject.SetActive(true);
        skillInfo = GameDataMgr.Instance.GetSkillInfoById(id);
        image.sprite = Resources.Load<Sprite>("Img/" + skillInfo.icon_name);
    }

    public void SetInventory(int id)
    {
        this.id = id;
        objectInfo = GameDataMgr.Instance.GetObjectInfoById(id);
        if (objectInfo.Type=="Drug")
        {
            type = ShortCutType.Drug;
            image.gameObject.SetActive(true);
            image.sprite = Resources.Load<Sprite>("Img/" + objectInfo.icon_name);
        }
        
    }
   
    public void OnDrugUse()
    {
        int success = BagPanel.Instance.MinusId(id, 1);
        if (success>=0)
        {
            ps.GetDrug(objectInfo.hp, objectInfo.mp);
            if (success==0)
            {
                type = ShortCutType.None;
                image.gameObject.SetActive(false);
                id = 0;
                skillInfo = null;
                objectInfo = null;
            }
        }
        else
        {
            type = ShortCutType.None;
            image.gameObject.SetActive(false);
            id = 0;
            skillInfo = null;
            objectInfo = null;
        }
    }
}
