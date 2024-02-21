using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float fadeSpeed = 1.0f;
    private CanvasGroup canvasGroup;


    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha!=0)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, fadeSpeed * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha-0)<0.05f)
            {
                canvasGroup.alpha = 0;
            }
        }
    }
}
