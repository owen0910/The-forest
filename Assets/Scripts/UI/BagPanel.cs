using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : DataBasePanel<BagPanel>
{
   
    public ScrollRect svBag;
    public Text txtCoin;
    public Button btnClose;
    //װ�������б�
    public List<Grid> GridList;
    //�����������
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
    /// ��ӵ���
    /// </summary>
    /// <param name="id"></param>
    public void AddToBag(int id,int count=1)
    {
        Grid grid = null;
        //������û����ͬid����Ʒ
        foreach (var item in GridList)
        {
            if (item.id == id)
            {
                grid = item;
                break;
            }
        }
        //������Ʒ
        if (grid != null)
        {
            grid.AddNum(count);
        }
        //��������Ʒ
        else
        {
            //�ҵ���һ���յĸ���
            foreach (var item in GridList)
            {
                if (item.id == 0)
                {
                    grid = item;
                    break;
                }
            }
            //�пո���
            if (grid != null)
            {
                //ʵ����ͼƬprefab
                GameObject imgItem = Instantiate(Resources.Load<GameObject>("UI/imgItem"));
                imgItem.transform.SetParent(grid.transform, false);
                imgItem.transform.localPosition = Vector3.zero;
                imgItem.transform.localScale = Vector3.one*0.7f;
                //����ͼƬ��Ϣ������
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
                str += "���ƣ�" + info.name + "\n";
                str += "+HP��" + info.hp + "\n";
                str += "+MP��" + info.mp + "\n";
                str += "���ۼۣ�" + info.price_sell + "\n";
                str += "����ۣ�" + info.price_buy;
                break;
            case "Equip":
                str += "���ƣ�" + info.name + "\n";
                switch (info.DressType)
                {
                    case "Armor":
                        str += "��������:����\n";
                        break;
                    case "Shoe":
                        str += "��������:Ь\n";
                        break;
                    case "Headgear":
                        str += "��������:ͷ��\n";
                        break;
                    case "Accessory":
                        str += "��������:��Ʒ\n";
                        break;
                    case "LeftHand":
                        str += "��������:����\n";
                        break;
                    case "RightHand":
                        str += "��������:����\n";
                        break;
                }
                switch (info.ApplicationType)
                {
                    case "Swordman":
                        str += "��������:��ʿ\n";
                        break;
                    case "Magician":
                        str += "��������:ħ��ʦ\n";
                        break;
                    case "Common":
                        str += "��������:ͨ��\n";
                        break;
                }
                str += "�˺�ֵ��" + info.attack + "\n";
                str += "����ֵ��" + info.def + "\n";
                str += "�ٶȣ�" + info.speed + "\n";
                str += "���ۼۣ�" + info.price_sell + "\n";
                str += "����ۣ�" + info.price_buy;
                break;

        }
        
        this.infoText.text = str;
    }
    public void HideInfo()
    {
        infoPanel.SetActive(false);
    }
    /// <summary>
    /// �ܷ����Ľ��
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
        //������û����ͬid����Ʒ
        foreach (var item in GridList)
        {
            if (item.id == id)
            {
                grid = item;
                break;
            }
        }
        //û����Ʒ���� û������
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
