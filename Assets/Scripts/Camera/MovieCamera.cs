using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCamera : MonoBehaviour
{
    private static MovieCamera instance;
    public static MovieCamera Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public float speed;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (transform.position!=targetPosition)
        {
            //没达到目标位置
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime*speed);
        }
    }
    public void ChangePosition(Vector3 v)
    {
        targetPosition = v;
    }
}
