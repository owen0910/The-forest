using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    private Image img;
    private bool isHover = false;
    private void Awake()
    {
        img = this.GetComponent<Image>();
    }
    private void Update()
    {
        if (isHover)
        {
            if (Input.GetMouseButtonDown(1))
            {
                
                EquipmentPanel.Instance.TakeOff(id,this.gameObject);

            }
        }
    }

    /// <summary>
    /// 设置装备信息
    /// </summary>
    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = GameDataMgr.Instance.GetObjectInfoById(id);
        img.sprite= Resources.Load<Sprite>("Img/" + info.icon_name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
    }
}
