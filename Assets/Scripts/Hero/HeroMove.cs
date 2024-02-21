using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Moving,
    Idle
}

public class HeroMove : MonoBehaviour
{
    public float speed = 1;
    private MovePoint point;
    private CharacterController controller;
    public PlayerState state = PlayerState.Idle;
    private PlayerAttack attack;
    public bool isMoving = false;

   
    private void Start()
    {
        point = this.GetComponent<MovePoint>();
        controller = this.GetComponent<CharacterController>();
       
    }
    private void Awake()
    {
        attack = this.GetComponent<PlayerAttack>();
    }
    void Update()
    {
        if (attack.state==HeroState.Death)
        {
            return;
        }
        if (attack.state == HeroState.ControlWalk)
        {
            float distance = Vector3.Distance(point.targetPosition, transform.position);
            if (distance > 0.1f)
            {
                isMoving = true;
                state = PlayerState.Moving;
                //controller.SimpleMove(transform.forward * speed);         
            }
            else
            {
                isMoving = false;
                state = PlayerState.Idle;
            }
        }
        
        
    }

    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        controller.SimpleMove(transform.forward * speed);
    }
}
