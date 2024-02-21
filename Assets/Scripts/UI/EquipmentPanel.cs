using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPanel : DataBasePanel<EquipmentPanel>
{
    public Button btnClose;
    public GameObject headgear;
    public GameObject Armor;
    public GameObject leftHand;
    public GameObject RightHand;
    public GameObject Shoe;
    public GameObject Accessory;

    private PlayerStatus ps;

    public int attack=0;
    public int def=0;
    public int speed=0;

    public override void Init()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
        });
        HideMe();
    }

    /// <summary>
    /// 穿装备
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool Dress(int id)
    {
        ObjectInfo info = GameDataMgr.Instance.GetObjectInfoById(id);
        //不可以穿戴的情况
        if (info.Type!="Equip")
        {
            return false;
        }
        if (ps.heroType==HeroType.Magician)
        {
            if (info.ApplicationType=="Swordman")
            {
                return false;
            }
        }
        if (ps.heroType == HeroType.SwordMan)
        {
            if (info.ApplicationType == "Magician")
            {
                return false;
            }
        }
        //处理穿戴
        GameObject parent = null;
        switch (info.DressType)
        {
            case "Headgear":
                parent = headgear;
                break;
            case "Armor":
                parent = Armor;
                break;
            case "RightHand":
                parent = RightHand;
                break;
            case "LeftHand":
                parent = leftHand;
                break;
            case "Shoe":
                parent = Shoe;
                break;
            case "Accessory":
                parent = Accessory;
                break;
        }
        EquipItem item = parent.GetComponentInChildren<EquipItem>();
        //已经穿戴了装备
        if (item!=null)
        {
            BagPanel.Instance.AddToBag(item.id);
            item.SetId(info.id);
        }
        //没有穿戴装备
        else
        {
            GameObject equip = Instantiate(Resources.Load<GameObject> ("Equipment/equipmentItem"));
            equip.transform.SetParent(parent.transform,false);
            equip.transform.localPosition = Vector3.zero;
            EquipItem item1 = equip.GetComponent<EquipItem>();
            item1.SetId(info.id);


        }
        updateProperty();

        return true;
    }

    public void TakeOff(int id,GameObject go)
    {
        BagPanel.Instance.AddToBag(id);
        GameObject.Destroy(go);
        updateProperty();
    }

    public void updateProperty()
    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;
        EquipItem headgearItem = headgear.GetComponentInChildren<EquipItem>();
        PlusProperty(headgearItem);
        EquipItem amorItem = Armor.GetComponentInChildren<EquipItem>();
        PlusProperty(amorItem);
        EquipItem leftHandItem = leftHand.GetComponentInChildren<EquipItem>();
        PlusProperty(leftHandItem);
        EquipItem rightGandItem = RightHand.GetComponentInChildren<EquipItem>();
        PlusProperty(rightGandItem);
        EquipItem shoeItem = Shoe.GetComponentInChildren<EquipItem>();
        PlusProperty(shoeItem);
        EquipItem accessoryItem = Accessory.GetComponentInChildren<EquipItem>();
        PlusProperty(accessoryItem);

    }    

    private void PlusProperty(EquipItem item)
    {
        if (item!=null)
        {
            ObjectInfo equipInfo = GameDataMgr.Instance.GetObjectInfoById(item.id);
            this.attack += equipInfo.attack;
            this.def += equipInfo.def;
            this.speed += equipInfo.speed;
        }
        
    }
}
