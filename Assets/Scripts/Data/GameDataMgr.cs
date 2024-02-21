using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance=new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //���н�ɫ����
    public List<HeroInfo> heroInfoList;
    //��ǰѡ��Ľ�ɫ
    public HeroInfo nowHeroInfo=new HeroInfo();
    //���н�ɫ��װ������
    public List<ObjectInfo> objectInfoList;
    //װ���ֵ�
    public Dictionary<int, ObjectInfo> objectdic = new Dictionary<int, ObjectInfo>();
    //��ǰ��ɫ��װ������
    public List<Grid> nowGridList;
    //���м�������
    public List<skillInfo> skillsList;
    //�����ֵ�
    public Dictionary<int, skillInfo> skilldic = new Dictionary<int, skillInfo>();


    private GameDataMgr()
    {
        Initobjectdic();
    }

    /// <summary>
    /// ��װ���ֵ��ж�ȡ����
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectdic.TryGetValue(id, out info);
        return info;
    }
    
    /// <summary>
    /// ��ʼ���ֵ�
    /// </summary>
    public void Initobjectdic()
    {
        //��ȡ��ҵ�����
        heroInfoList = JsonMgr.Instance.LoadData<List<HeroInfo>>("HeroInfo");
        //��ȡ��ҵ�װ����Ϣ
        objectInfoList = JsonMgr.Instance.LoadData<List<ObjectInfo>>("ObjectInfo");
        for (int i = 0; i < objectInfoList.Count; i++)
        {
            objectdic.Add(objectInfoList[i].id, objectInfoList[i]);
        }
        //��ȡ��������
        skillsList = JsonMgr.Instance.LoadData<List<skillInfo>>("skill");
        for (int i = 0; i < skillsList.Count; i++)
        {
            skilldic.Add(skillsList[i].id, skillsList[i]);
        }
    }
    /// <summary>
    /// ͨ��id��ȡ������Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public skillInfo GetSkillInfoById(int id)
    {
        skillInfo info = null;
        skilldic.TryGetValue(id, out info);
        return info;
    }
   

   
}
