using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid :MonoBehaviour
{
    //物品id
    public int id = 0;
    public ObjectInfo info = null;
    public int num = 0;
    public Text txtNum;
    

    
    /// <summary>
    /// 设置格子上的数量和图片
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num"></param>
    public void SetId(int id,int num=1)
    {
        this.id = id;
        info = GameDataMgr.Instance.GetObjectInfoById(id);
        ImgItem item = this.GetComponentInChildren<ImgItem>();
        txtNum.enabled = true;
        this.num = num;
        txtNum.text = num.ToString();
        //if (item==null)
        //{
        //    GameObject imgItem = Instantiate(Resources.Load<GameObject>("UI/imgItem"));
        //    imgItem.transform.SetParent(this.transform, false);
        //    imgItem.transform.localPosition = Vector3.zero;
        //    imgItem.transform.localScale = Vector3.one*0.7f;
        //    item = imgItem.GetComponent<ImgItem>();
        //}
        
        item.setImg(id);


    }

    /// <summary>
    /// 清空格子内容
    /// </summary>
    public void ClearInfo()
    {
        id = 0;
        info = null;
        num = 0;
        txtNum.enabled = false;
    }
    /// <summary>
    /// 增加道具个数
    /// </summary>
    /// <param name="num"></param>
    public void AddNum(int num=1)
    {
        this.num += num;
        txtNum.text = this.num.ToString();
    }

    /// <summary>
    /// 减去装备
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int MinusNum(int num=1)
    {
        if (this.num>=num)
        {
            this.num -= num;
            txtNum.text = this.num.ToString();
            if (this.num==0)
            {
                //清空物品格子
                ClearInfo();
                GameObject.Destroy(this.GetComponentInChildren<ImgItem>().gameObject);
                return 0;
            }
            return 1;
        }
        return -1;
    }

    
}
