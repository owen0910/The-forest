using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImgItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    private int id;
    //������λ��
    public Transform parent;
    //��ƷͼƬ
    public Image img;
    //��Ʒ����
    public Text txtNum;
    //��¼ƫ����
    private Vector3 offset;
    //�Ƿ�����ק��
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
    /// ��ʾͼƬ
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
        //��UI����Ϊ�ɴ�͸���������Ͳ��ᱻ��ס�����Ի������Ŀ��λ�õ���Ϣ
        //������λ����Ϣ��ȡ
        GameObject target = eventData.pointerEnter;
        //Ŀ��λ��Ϊ��
        if (!target)
        {
            ResetPoint();
            return;
        }

        //Ŀ����е���
        else if (target.GetComponent<ImgItem>())
        {
            ChangePoint(target);
        }
        //Ŀ���Ϊ����
        else if (target.GetComponent<Grid>())
        {
            
            //Ŀ�����û�е���
            if (!target.GetComponentInChildren<ImgItem>())
            {
                Grid oldgrid = this.GetComponentInParent<Grid>();
                transform.SetParent(target.transform);
                transform.localPosition = Vector3.zero;
                Grid newgrid = target.GetComponent<Grid>();                        
                newgrid.SetId(oldgrid.id, oldgrid.num);
                oldgrid.ClearInfo();
            }
            //Ŀ������е���
            else
            {
                target = target.transform.GetChild(0).gameObject;
                ChangePoint(target);
            }
        }
        else if (target != null && target.tag == "shortCut")
        {

            Debug.Log(1);
            //��ʾ����ͼ��
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
    /// �Ƶ�ͼ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        img.raycastTarget = true;
        //��UI����Ϊ�ɴ�͸���������Ͳ��ᱻ��ס�����Ի������Ŀ��λ�õ���Ϣ
        //������λ����Ϣ��ȡ
        GameObject target = eventData.pointerEnter;
        if (target.GetComponent<ImgItem>())
        {
            isHover = true;
            Grid gird = target.GetComponentInParent<Grid>();
            BagPanel.Instance.ShowInfo(gird.info, target.transform);
            
        }
    }
    /// <summary>
    /// �뿪ͼ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        BagPanel.Instance.HideInfo();
    }

    /// <summary>
    /// ��λ��ͼ�귵��ԭ����
    /// </summary>
    public void ResetPoint()
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one*0.7f;
    }
    /// <summary>
    /// ����ͼ����Ϣ
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
