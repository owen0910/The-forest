using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : DataBasePanel<BagPanel>
{
   
    public ScrollRect svBag;
    public Text txtCoin;
    public Button btnClose;
    //装备格子列表
    public List<Grid> GridList;
    //背包金币数量
    private int coinNum = 1000;

    public GameObject infoPanel;
    public Text infoText;

    public override void Init()
    {
        this.HideMe();
        infoPanel.SetActive(false);
        btnClose.onClick.AddListener(() =>
        {
            this.HideMe();
        });
    }


    /// <summary>
    /// 添加道具
    /// </summary>
    /// <param name="id"></param>
    public void AddToBag(int id,int count=1)
    {
        Grid grid = null;
        //查找有没有相同id的物品
        foreach (var item in GridList)
        {
            if (item.id == id)
            {
                grid = item;
                break;
            }
        }
        //存在物品
        if (grid != null)
        {
            grid.AddNum(count);
        }
        //不存在物品
        else
        {
            //找到第一个空的格子
            foreach (var item in GridList)
            {
                if (item.id == 0)
                {
                    grid = item;
                    break;
                }
            }
            //有空格子
            if (grid != null)
            {
                //实例化图片prefab
                GameObject imgItem = Instantiate(Resources.Load<GameObject>("UI/imgItem"));
                imgItem.transform.SetParent(grid.transform, false);
                imgItem.transform.localPosition = Vector3.zero;
                imgItem.transform.localScale = Vector3.one*0.7f;
                //设置图片信息和数量
                grid.SetId(id,count);
            }
        }


    }
   

    public void ShowInfo(ObjectInfo info,Transform pos)
    {
        infoPanel.SetActive(true);
        infoPanel.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z);
        string str = "";
        switch (info.Type)
        {
            case "Drug":
                str += "名称：" + info.name + "\n";
                str += "+HP：" + info.hp + "\n";
                str += "+MP：" + info.mp + "\n";
                str += "出售价：" + info.price_sell + "\n";
                str += "购买价：" + info.price_buy;
                break;
            case "Equip":
                str += "名称：" + info.name + "\n";
                switch (info.DressType)
                {
                    case "Armor":
                        str += "穿戴类型:盔甲\n";
                        break;
                    case "Shoe":
                        str += "穿戴类型:鞋\n";
                        break;
                    case "Headgear":
                        str += "穿戴类型:头盔\n";
                        break;
                    case "Accessory":
                        str += "穿戴类型:饰品\n";
                        break;
                    case "LeftHand":
                        str += "穿戴类型:左手\n";
                        break;
                    case "RightHand":
                        str += "穿戴类型:右手\n";
                        break;
                }
                switch (info.ApplicationType)
                {
                    case "Swordman":
                        str += "适用类型:剑士\n";
                        break;
                    case "Magician":
                        str += "适用类型:魔法师\n";
                        break;
                    case "Common":
                        str += "适用类型:通用\n";
                        break;
                }
                str += "伤害值：" + info.attack + "\n";
                str += "防御值：" + info.def + "\n";
                str += "速度：" + info.speed + "\n";
                str += "出售价：" + info.price_sell + "\n";
                str += "购买价：" + info.price_buy;
                break;

        }
        
        this.infoText.text = str;
    }
    public void HideInfo()
    {
        infoPanel.SetActive(false);
    }
    /// <summary>
    /// 能否消耗金币
    /// </summary>
    /// <returns></returns>
    public bool UseCoin(int count)
    {
        if (coinNum>=count)
        {
            coinNum -= count;
            txtCoin.text = coinNum.ToString();
            return true;
        }
        return false;
    }

    public void AddCoin(int count)
    {
        coinNum += count;
        txtCoin.text = coinNum.ToString();
    }
    
    public int  MinusId(int id,int count=1)
    {
        Grid grid = null;
        //查找有没有相同id的物品
        foreach (var item in GridList)
        {
            if (item.id == id)
            {
                grid = item;
                break;
            }
        }
        //没有物品存在 没法减少
        if (grid==null)
        {
            return -1;
        }
        else
        {
            int isSuccess = grid.MinusNum(count);
            return isSuccess;
        }
    }
}
