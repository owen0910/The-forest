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


    //��ǰ��ʾ��Ӣ�۶���
    private GameObject heroObj;

    //��ǰѡ���Ӣ�۵�����
    private int nowIndex=0;

    //��ǰѡ�е�Ӣ����Ϣ
    private HeroInfo nowHeroInfo;

    private Transform heroPos;




    public override void Init()
    {

        heroPos = GameObject.Find("HeroPos").transform;
        ShowHero();
        
        //��ʼ����Ч
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //����ť
        btnLeft.onClick.AddListener(() =>
        {
            audioSource.Play();
            --nowIndex;
            if (nowIndex < 0)
                nowIndex = GameDataMgr.Instance.heroInfoList.Count-1;
            ShowHero();

        });

        //���Ұ�ť
        btnRight.onClick.AddListener(() =>
        {
            audioSource.Play();
            ++nowIndex;
            if (nowIndex > GameDataMgr.Instance.heroInfoList.Count-1)
                nowIndex = 0;
            ShowHero();
        });

        //ȷ�ϰ�ť
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
    /// ��ʾӢ��ģ��
    /// </summary>
    public void ShowHero()
    {
        //��ǰ����ʾ�Ļ�����ɾ��
        if (heroObj!=null)
        {
            Destroy(heroObj);
            heroObj = null;
        }
        //��ȡӢ������
        nowHeroInfo = GameDataMgr.Instance.heroInfoList[nowIndex];
        heroObj = Instantiate(Resources.Load<GameObject>(nowHeroInfo.res), heroPos.position, heroPos.rotation);

    }
}
