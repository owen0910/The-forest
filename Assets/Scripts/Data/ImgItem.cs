using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImgItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    private int id;
    //父对象位置
    public Transform parent;
    //物品图片
    public Image img;
    //物品数量
    public Text txtNum;
    //记录偏移量
    private Vector3 offset;
    //是否在拖拽中
    bool isHover = false;

    private void Update()
    {
        if (isHover)
        {
            if (Input.GetMouseButtonDown(1))
            {
                
                Grid oldgrid = this.GetComponentInParent<Grid>();
                bool success = EquipmentPanel.Instance.Dress(oldgrid.id);
                if (success)
                {
                    oldgrid.MinusNum();
                }
                BagPanel.Instance.infoPanel.SetActive(false);
                isHover = false;
            }
        }
    }

    /// <summary>
    /// 显示图片
    /// </summary>
    /// <param name="id"></param>
    public void setImg(int id)
    {
        this.id = id;
        ObjectInfo info = GameDataMgr.Instance.GetObjectInfoById(id);
        img.sprite = Resources.Load<Sprite>("Img/" + info.icon_name);

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = Input.mousePosition - transform.position;
        parent = transform.GetComponentInParent<Grid>().transform;
        transform.SetParent(skillPanel.Instance.canvas);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - offset;
        img.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        img.raycastTarget = true;
        //将UI设置为可穿透，这样鼠标就不会被挡住，可以获得我们目标位置的信息
        //鼠标结束位置信息获取
        GameObject target = eventData.pointerEnter;
        //目标位置为空
        if (!target)
        {
            ResetPoint();
            return;
        }

        //目标点有道具
        else if (target.GetComponent<ImgItem>())
        {
            ChangePoint(target);
        }
        //目标点为格子
        else if (target.GetComponent<Grid>())
        {
            
            //目标点下没有道具
            if (!target.GetComponentInChildren<ImgItem>())
            {
                Grid oldgrid = this.GetComponentInParent<Grid>();
                transform.SetParent(target.transform);
                transform.localPosition = Vector3.zero;
                Grid newgrid = target.GetComponent<Grid>();                        
                newgrid.SetId(oldgrid.id, oldgrid.num);
                oldgrid.ClearInfo();
            }
            //目标点下有道具
            else
            {
                target = target.transform.GetChild(0).gameObject;
                ChangePoint(target);
            }
        }
        else if (target != null && target.tag == "shortCut")
        {

            Debug.Log(1);
            //显示技能图标
            shortCutGrid shortCutGrid = target.GetComponent<shortCutGrid>();
            if (shortCutGrid == null)
            {
                shortCutGrid = target.GetComponentInParent<shortCutGrid>();
            }
            shortCutGrid.SetInventory(id);
            ResetPoint();

        }

        else
        {
            ResetPoint();
        }

    }
    /// <summary>
    /// 移到图标上面
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        img.raycastTarget = true;
        //将UI设置为可穿透，这样鼠标就不会被挡住，可以获得我们目标位置的信息
        //鼠标结束位置信息获取
        GameObject target = eventData.pointerEnter;
        if (target.GetComponent<ImgItem>())
        {
            isHover = true;
            Grid gird = target.GetComponentInParent<Grid>();
            BagPanel.Instance.ShowInfo(gird.info, target.transform);
            
        }
    }
    /// <summary>
    /// 离开图标上面
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        BagPanel.Instance.HideInfo();
    }

    /// <summary>
    /// 复位，图标返回原格子
    /// </summary>
    public void ResetPoint()
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one*0.7f;
    }
    /// <summary>
    /// 交换图标信息
    /// </summary>
    /// <param name="target"></param>
    public void ChangePoint(GameObject target)
    {
        transform.SetParent(target.transform.parent);
        transform.localPosition = Vector3.zero;
        target.transform.SetParent(parent);
        target.transform.localPosition = Vector3.zero;
        parent = transform.parent;
        Grid newgrid = target.GetComponentInParent<Grid>();
        Grid oldgrid = this.GetComponentInParent<Grid>();
        int id = newgrid.id;
        int num = newgrid.num;
        newgrid.SetId(oldgrid.id, oldgrid.num);
        oldgrid.SetId(id, num);

    }

    
}
