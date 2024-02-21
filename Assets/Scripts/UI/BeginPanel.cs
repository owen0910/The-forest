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
            //������Ϸѡ��ҳ��
            audioSource.Play();
            UIManager.Instance.HidePanel<BeginPanel>();
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            

        });

        btnLoad.onClick.AddListener(() =>
        {
            //������Ϸҳ��
            audioSource.Play();
        });

        MovieCamera.Instance.ChangePosition(new Vector3(2.34f, 31.19f, -20));
    }

    

   
}
