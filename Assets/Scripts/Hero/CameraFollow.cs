using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    //���λ��ƫ��
    private Vector3 offsetPosition;
    //���ƫ�ƾ���
    public float distance = 0;
    //��������ٶ�
    public float scrollSpeed = 1;
    //����Ƿ񻬶�
    private bool isRotating=false;
    //�ӽǻ�������
    public float rotateSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        transform.LookAt(transform.position);
        offsetPosition = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offsetPosition + player.position;
        //�����ӽǵ���ת
        RorateView();
        //�����ӽ�������Զ
        ScrollView();
        
    }

    /// <summary>
    /// �����ӽ�������Զ
    /// </summary>
    public void ScrollView()
    {
        distance = offsetPosition.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 2, 18);
        offsetPosition = offsetPosition.normalized * distance;
    }

    public void RorateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            //����������ת��Χ
            if (x<10||x>80)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
        offsetPosition = transform.position - player.position;
    }

}
