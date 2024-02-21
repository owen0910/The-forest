using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : DataBasePanel<ShopPanel>
{
    public Button btnBuyhp1;
    public Button btnBuyhp2;
    public Button btnBuymp1;

    public InputField input;
    public Button btnOk;

    public Button btnClose;
    //¹ºÂò½çÃæ
    public GameObject BuyInfo;
    //¹ºÂòµÄid
    private int buy_id=0;
    public override void Init()
    {
        btnBuyhp1.onClick.AddListener(() =>
        {
            Buy(1001);
        });
        btnBuyhp2.onClick.AddListener(() =>
        {
            Buy(1002);

        });
        btnBuymp1.onClick.AddListener(() =>
        {
            Buy(1003);
        });
        btnOk.onClick.AddListener(() =>
        {
            int count = int.Parse(input.text);
            ObjectInfo objectInfo = GameDataMgr.Instance.GetObjectInfoById(buy_id);
            int price = objectInfo.price_buy;
            int total = price * count;
            bool success = BagPanel.Instance.UseCoin(total);
            if (success)
            {
                if (count>0)
                {
                    BagPanel.Instance.AddToBag(buy_id, count);
                }
            }
            BuyInfo.SetActive(false);
        });
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
        });
        BuyInfo.SetActive(false);
        HideMe();
    }

    public void Buy(int id)
    {
        ShowBuyInfo();
        buy_id = id;
    }

    public void ShowBuyInfo()
    {
        BuyInfo.SetActive(true);
        input.text = "0";
    }
    
}
