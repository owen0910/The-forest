using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitInfo : MonoBehaviour
{
    private GameObject canvas;
    private float tarheight;
    public Text txtInfo;
    private RectTransform hittransform;
    // Start is called before the first frame update

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
        hittransform = gameObject.GetComponent<Image>().transform as RectTransform;
    }
    private void Start()
    {
        
    }
    public void setInfo(string text,GameObject Target)
    {

        transform.SetParent(canvas.transform,false);
        tarheight = 1f;
        Vector3 worldPPostion = new Vector3(Target.transform.position.x,
            Target.transform.position.y + tarheight, Target.transform.position.z);
        Vector2 postion = Camera.main.WorldToScreenPoint(worldPPostion);
        hittransform.position = postion;
        txtInfo.text = text;
        Destroy(this.gameObject, 0.2f);
    }
}
