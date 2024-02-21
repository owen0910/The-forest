using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    SwordMan,
    Magician
}
public class PlayerStatus : MonoBehaviour
{
    public HeroType heroType;
    //等级
    public string name = "默认";
    public int grade = 1;
    public int hp=100;
    public int mp=100;
    public int nowHp;
    public int nowMp;


    public float attack = 20;
    public int attack_plus = 0;
    public float def = 20;
    public int def_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;
    //剩余点数
    public int point_remain = 0;
    public float exp = 0;

    private void Start()
    {
        GetExp(0);
    }
    /// <summary>
    /// 用药
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="mp"></param>
    public void GetDrug(int hp,int mp)
    {
        nowHp += hp;
        nowMp += mp;
        if (nowHp>this.hp)
        {
            nowHp = this.hp;
        }
        if (nowMp>this.mp)
        {
            nowMp = this.mp;
        }
        HeadPanel.Instance.imgHP.fillAmount = (float)nowHp / this.hp;
        HeadPanel.Instance.imgMP.fillAmount = (float)nowMp / this.mp;
    }
    /// <summary>
    /// 是否能加点
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool GetPoint(int point=1)
    {
        if (point_remain>=point)
        {
            point_remain -= point;
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 加经验
    /// </summary>
    /// <param name="exp"></param>
    public void GetExp(int exp)
    {
        this.exp += exp;
        int total_exp = 100 + grade * 30;
        while(this.exp>=total_exp)
        {
            //升级
            this.grade++;
            this.exp -= total_exp;
            total_exp = 100 + grade * 30;
        }
        HeadPanel.Instance.imgExp.fillAmount = (float)this.exp / total_exp;

    }

    public bool TakeMP(int count)
    {
        if (nowMp>=count)
        {
            nowMp -= count;
            HeadPanel.Instance.imgMP.fillAmount = (float)nowMp / mp;
            return true;
        }
        else
        {
            return false;
        }
    }
}
