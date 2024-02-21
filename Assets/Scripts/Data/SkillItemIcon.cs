using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItemIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //������λ��
    public Transform parent;
    //��¼ƫ����
    private Vector3 offset;
    private skillInfo info;
    //�Ƿ�����ק��
    bool isDrag;
    bool isHover = false;
    private Image img;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        offset = Input.mousePosition - transform.position;
        transform.SetParent(skillPanel.Instance.canvas);
        this.transform.SetAsLastSibling();
        //����ʵ����һ��ͼ��
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/imgskill"));
        gameObject.transform.SetParent(parent, false);
        Image image = gameObject.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>("Img/" + info.icon_name);
        //parent = transform.GetComponentInParent<Grid>().transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition - offset;
        img.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerEnter;
        img.raycastTarget = true;
        if (target!=null&&target.tag=="shortCut")
        {
            //��ʾ����ͼ��
            shortCutGrid shortCutGrid = target.GetComponent<shortCutGrid>();
            if (shortCutGrid==null)
            {
                shortCutGrid = target.GetComponentInParent<shortCutGrid>();
            }
            shortCutGrid.SetSkill(info.id);
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
        parent = this.transform.parent;
        skillItem item = this.GetComponentInParent<skillItem>();
        info = item.info;
        

    }

    
}
