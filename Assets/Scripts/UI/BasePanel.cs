using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    //����͸����
    private CanvasGroup canvasGroup;
    //���뵭�����ٶ�
    private float alphaSpeed = 0.25f;
    //�Ƿ�Ҫ��ʾ
    public bool isShow = false;

    protected virtual void Awake()
    {
        //��ȡ����ϵ����
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup==null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
            
        }
    }

    protected virtual void Start()
    {
        //���ڳ�ʼ���¼�
        Init();

    }

    public abstract void Init();

    //��ʾ����
    public virtual void ShowMe(bool isfade)
    {
        if (isfade)
        {
            canvasGroup.alpha = 0;
            isShow = true;
        }
        else
            canvasGroup.alpha = 1;
            isShow = true;

    }

    //���غ���
    public virtual void HideMe()
    {
        canvasGroup.alpha = 1;
        isShow = false;

    }

    protected virtual void Update()
    {
        //����
        if (isShow&&canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha>=1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //����
        //else if (!isShow && canvasGroup.alpha != 0)
        //{
        //    canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
        //    if (canvasGroup.alpha < -0)
        //    {
        //        canvasGroup.alpha = 0;
        //    }
        //}
    }
}
