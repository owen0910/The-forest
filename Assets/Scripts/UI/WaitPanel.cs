using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitPanel : BasePanel
{
    private AudioSource audioSource;
    public Button btnPress;
    public float fadeSpeed = 1.0f;
    public CanvasGroup canvasGroup1;

   
    public override void Init()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        btnPress.onClick.AddListener(() =>
        {
            audioSource.Play();
            UIManager.Instance.HidePanel<WaitPanel>();
            UIManager.Instance.ShowPanel<BeginPanel>();
            
        });

        
    }
    protected override void Update()
    {
        base.Update();

        if (canvasGroup1.alpha != 0)
        {
            canvasGroup1.alpha = Mathf.Lerp(canvasGroup1.alpha, 0, fadeSpeed * Time.deltaTime);
            if (Mathf.Abs(canvasGroup1.alpha - 0) < 0.05f)
            {
                canvasGroup1.alpha = 0;
            }
        }
    }

}
