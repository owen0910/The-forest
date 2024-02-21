using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Image img;
    public Text txtName;
    public Text txteffect;
    public Text txtSell;
    public Button btnBuy;
    private int id;

    private void Start()
    {
        btnBuy.onClick.AddListener(() =>
        {
            ShopWepanPanel.Instance.Buy(id);
        });
    }
    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = GameDataMgr.Instance.GetObjectInfoById(id);
        img.sprite = Resources.Load<Sprite>("Img/" + info.icon_name);
        txtName.text = info.name;
        if (info.attack>0)
        {
            txteffect.text = "+ÉËº¦" + info.attack;
        }
        else if (info.def>0)
        {
            txteffect.text = "+·ÀÓù" + info.def;
        }
        else if (info.speed>0)
        {
            txteffect.text = "+ËÙ¶È" + info.speed;
        }
        txtSell.text = info.price_buy.ToString();
    }
}
