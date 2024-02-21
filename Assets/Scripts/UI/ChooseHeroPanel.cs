using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button btnLeft;
    public Button btnRight;
    public Button btnSure;
    public InputField inputName;
    private AudioSource audioSource;


    //当前显示的英雄对象
    private GameObject heroObj;

    //当前选择的英雄的索引
    private int nowIndex=0;

    //当前选中的英雄信息
    private HeroInfo nowHeroInfo;

    private Transform heroPos;




    public override void Init()
    {

        heroPos = GameObject.Find("HeroPos").transform;
        ShowHero();
        
        //初始化音效
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //向左按钮
        btnLeft.onClick.AddListener(() =>
        {
            audioSource.Play();
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.heroInfoList.Count-1;
            ShowHero();

        });

        //向右按钮
        btnRight.onClick.AddListener(() =>
        {
            audioSource.Play();
            ++nowIndex;
            if (nowIndex > GameDataMgr.Instance.heroInfoList.Count-1)
                nowIndex = 0;
            ShowHero();
        });

        //确认按钮
        btnSure.onClick.AddListener(() =>
        {
            audioSource.Play();
            GameDataMgr.Instance.nowHeroInfo.name=inputName.text;
            GameDataMgr.Instance.nowHeroInfo.id = nowIndex;
            SceneManager.LoadScene("Game");
           

            UIManager.Instance.HidePanel<ChooseHeroPanel>();
        });


        MovieCamera.Instance.ChangePosition(new Vector3(2.34f, 30f, -18));


    }

    /// <summary>
    /// 显示英雄模型
    /// </summary>
    public void ShowHero()
    {
        //当前有显示的话就先删除
        if (heroObj!=null)
        {
            Destroy(heroObj);
            heroObj = null;
        }
        //读取英雄数据
        nowHeroInfo = GameDataMgr.Instance.heroInfoList[nowIndex];
        heroObj = Instantiate(Resources.Load<GameObject>(nowHeroInfo.res), heroPos.position, heroPos.rotation);

    }
}
