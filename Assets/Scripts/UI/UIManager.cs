using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    //���óɵ���ģʽ
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //�����ֵ����ڴ�����
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvasTrans;


    private UIManager()
    {
        //�õ������е�canvans����
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        
        //���������Ƴ�
        GameObject.DontDestroyOnLoad(canvas);
        
    }
    
    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <returns></returns>
    public T ShowPanel<T>(bool isfade=true)where T:BasePanel
    {
        string panelName = typeof(T).Name;
        //�ж��ֵ����Ƿ��Ѿ��������
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //��̬������岢��ʾ
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe(isfade);
        return panel;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade"></param>
    public void HidePanel<T>(bool isFade=true) where T:BasePanel
    {
        string panelName = typeof(T).Name;
        //�жϵ�ǰ��ʾ��������Ƿ���Ҫ���ص�
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
