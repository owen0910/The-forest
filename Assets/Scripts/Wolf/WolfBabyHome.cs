using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBabyHome : MonoBehaviour
{
    public int maxNum = 5;
    public int nowNum = 0;

    public float time = 3;
    private float timer = 0;

    public GameObject prefab;

    private void Update()
    {
        if (nowNum<maxNum)
        {
            timer += Time.deltaTime;
            if (timer>time)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-2, 2);
                pos.z += Random.Range(-2, 2);
                GameObject gameObject = Instantiate(prefab, pos, Quaternion.identity);
                gameObject.GetComponent<WolfBaby>().home = this;
                timer = 0;
                nowNum++;
            }
        }
    }

    public void MinusNumber()
    {
        this.nowNum--;
    }
}
