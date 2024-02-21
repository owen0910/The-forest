using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private HeroMove move;
    private Animation animation;
    private PlayerAttack attack;
    // Start is called before the first frame update
    void Start()
    {
        move = this.GetComponent<HeroMove>();
        animation = this.GetComponent<Animation>();
        
    }
    private void Awake()
    {
        attack = this.GetComponent<PlayerAttack>();
    }

    private void LateUpdate()
    {
        if (attack.state==HeroState.ControlWalk)
        {
            if (move.state == PlayerState.Moving)
            {
                PlayAnimation("Run");
            }
            else if (move.state == PlayerState.Idle)
            {
                PlayAnimation("Idle");
            }
        }
        else if (attack.state==HeroState.NormalAttack)
        {
            if (attack.attackState==AttackState.Moving)
            {
                PlayAnimation("Run");
            }
        }
       
        
    }
    private void PlayAnimation(string AnimationName)
    {
        animation.CrossFade(AnimationName);
    }
}
