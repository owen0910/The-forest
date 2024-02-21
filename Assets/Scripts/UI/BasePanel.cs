using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    //控制透明度
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 0.25f;
    //是否要显示
    public bool isShow = false;

    protected virtual void Awake()
    {
        //获取面板上的组件
        canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup==null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
            
        }
    }

    protected virtual void Start()
    {
        //用于初始化事件
        Init();

    }

    public abstract void Init();

    //显示函数
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

    //隐藏函数
    public virtual void HideMe()
    {
        canvasGroup.alpha = 1;
        isShow = false;

    }

    protected virtual void Update()
    {
        //淡入
        if (isShow&&canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha>=1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
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
