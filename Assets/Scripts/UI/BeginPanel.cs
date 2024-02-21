using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnNew;
    public Button btnLoad;
    private AudioSource audioSource;
    
    

    public override void Init()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        btnNew.onClick.AddListener(() =>
        {
            //加载游戏选择页面
            audioSource.Play();
            UIManager.Instance.HidePanel<BeginPanel>();
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            

        });

        btnLoad.onClick.AddListener(() =>
        {
            //加载游戏页面
            audioSource.Play();
        });

        MovieCamera.Instance.ChangePosition(new Vector3(2.34f, 31.19f, -20));
    }

    

   
}
