using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    //设置成单例模式
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //创建字典用于存放面板
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvasTrans;


    private UIManager()
    {
        //得到场景中的canvans对象
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        
        //过场景不移除
        GameObject.DontDestroyOnLoad(canvas);
        
    }
    
    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板名字</typeparam>
    /// <returns></returns>
    public T ShowPanel<T>(bool isfade=true)where T:BasePanel
    {
        string panelName = typeof(T).Name;
        //判断字典中是否已经存在面板
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //动态创建面板并显示
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe(isfade);
        return panel;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade"></param>
    public void HidePanel<T>(bool isFade=true) where T:BasePanel
    {
        string panelName = typeof(T).Name;
        //判断当前显示的面板中是否有要隐藏的
        if (panelDic.ContainsKey(panelName))
        {
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    public T GetPanel<T>()where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        return null;
    }


}
