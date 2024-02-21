using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWepanPanel : DataBasePanel<ShopWepanPanel>
{
    public Button btnClose;
    public Transform content;
    private List<ObjectInfo> objectInfoList = new List<ObjectInfo>();
    //¹ºÂò½çÃæ
    public GameObject BuyInfo;
    public InputField input;
    public Button btnOk;
    //¹ºÂòµÄid
    private int buy_id = 0;
    public override void Init()
    {
        InitWeapon();
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
        });
        HideMe();
        BuyInfo.SetActive(false);
        btnOk.onClick.AddListener(() =>
        {
            int count = int.Parse(input.text);
            ObjectInfo objectInfo = GameDataMgr.Instance.GetObjectInfoById(buy_id);
            int price = objectInfo.price_buy;
            int total = price * count;
            bool success = BagPanel.Instance.UseCoin(total);
            if (success)
            {
                if (count > 0)
                {
                    BagPanel.Instance.AddToBag(buy_id, count);
                }
            }
            BuyInfo.SetActive(false);
        });
    }
    public void InitWeapon()
    {
        objectInfoList.Clear(); ;
        for (int i = 2001; i < 2023; i++)
        {
            objectInfoList.Add(GameDataMgr.Instance.GetObjectInfoById(i));
        }
        
        for (int i = 0; i < objectInfoList.Count; i++)
        {
            GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/WeaponItem"));
            gameObject.transform.SetParent(content, false);
            gameObject.transform.localPosition = Vector3.zero;            
            WeaponItem item = gameObject.GetComponent<WeaponItem>();
            item.SetId(objectInfoList[i].id);


        }
    }

    public void Buy(int id)
    {
        BuyInfo.SetActive(true);
        input.text = "0";
        buy_id = id;
    }

}
