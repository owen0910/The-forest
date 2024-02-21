using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class MovePoint : MonoBehaviour
{
    public GameObject Hero;
    //��ʾ����Ƿ���
    private bool isMoving = false;
    public Vector3 targetPosition;
    private HeroMove heroMove;
    private PlayerAttack attack;

    public NavMeshAgent agent;

    private void Start()
    {
        targetPosition = transform.position;
        heroMove = this.GetComponent<HeroMove>();
        attack = this.GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (attack.state==HeroState.Death)
        {
            return;
        }
        if (attack.state==HeroState.SkillAttack)
        {
            return;
        }
        //��갴��
        if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
        {
            isMoving = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool isCollider = Physics.Raycast(ray, out hitinfo);
            if (isCollider&&hitinfo.collider.tag==Tags.ground)
            {
                ShowClick(hitinfo.point);
                LookAtTarget(hitinfo.point);
                agent.SetDestination(hitinfo.point);
            }

        }

        //���̧��
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            //�õ�Ҫ�ƶ���Ŀ��λ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool isCollider = Physics.Raycast(ray, out hitinfo);
            if (isCollider && hitinfo.collider.tag == Tags.ground)
            {
                LookAtTarget(hitinfo.point);
            }
            
        }
        else
        {
            if (heroMove.isMoving)
            {
                LookAtTarget(targetPosition);
            }
        }
        
    }
    //��ʾ���Ч��
    public void ShowClick(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z + 0.1f);
        GameObject.Instantiate(Hero, hitPoint, Quaternion.identity);
    }
    //��Ӣ�۳���Ŀ��λ��
    public void LookAtTarget(Vector3 hitPoint)
    {
        targetPosition = hitPoint;
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        this.transform.LookAt(targetPosition);
    }
}
