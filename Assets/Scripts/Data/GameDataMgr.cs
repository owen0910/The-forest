using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr 
{
    private static GameDataMgr instance=new GameDataMgr();
    public static GameDataMgr Instance => instance;

    //所有角色数据
    public List<HeroInfo> heroInfoList;
    //当前选择的角色
    public HeroInfo nowHeroInfo=new HeroInfo();
    //所有角色的装备数据
    public List<ObjectInfo> objectInfoList;
    //装备字典
    public Dictionary<int, ObjectInfo> objectdic = new Dictionary<int, ObjectInfo>();
    //当前角色的装备数据
    public List<Grid> nowGridList;
    //所有技能数据
    public List<skillInfo> skillsList;
    //技能字典
    public Dictionary<int, skillInfo> skilldic = new Dictionary<int, skillInfo>();


    private GameDataMgr()
    {
        Initobjectdic();
    }

    /// <summary>
    /// 从装备字典中读取数据
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
    /// 初始化字典
    /// </summary>
    public void Initobjectdic()
    {
        //读取玩家的数据
        heroInfoList = JsonMgr.Instance.LoadData<List<HeroInfo>>("HeroInfo");
        //读取玩家的装备信息
        objectInfoList = JsonMgr.Instance.LoadData<List<ObjectInfo>>("ObjectInfo");
        for (int i = 0; i < objectInfoList.Count; i++)
        {
            objectdic.Add(objectInfoList[i].id, objectInfoList[i]);
        }
        //读取技能数据
        skillsList = JsonMgr.Instance.LoadData<List<skillInfo>>("skill");
        for (int i = 0; i < skillsList.Count; i++)
        {
            skilldic.Add(skillsList[i].id, skillsList[i]);
        }
    }
    /// <summary>
    /// 通过id读取技能信息
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
