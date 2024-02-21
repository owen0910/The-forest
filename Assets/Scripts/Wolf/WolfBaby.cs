using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death

}
public class WolfBaby : MonoBehaviour
{
    public int exp = 20;
    public WolfBabyHome home;
    public WolfState state = WolfState.Idle;
    public Animation animation;
    public string aniname_now;
    public float time = 1;
    public float timer = 0;
    public float speed = 1;
    public int hp = 100;
    public float miss_rate = 0.2f;
    //动画名称
    public string attackNormal;
    public string attackCrazy;
    public string death;
    public string Idle;
    public string walk;

    public int attack;

    public Renderer renderer;

    private Color normal;
    public float red_time = 1;
    private float attacktimer = 0;
    private bool isHurt = false;

    private CharacterController cc;
    private PlayerStatus ps;

    public float attack_time_crazy=0.733f;
    //攻击速率
    public float attack_time =0.633f;
    //攻击计时器
    public float attack_timer = 0;
    //狂暴几率
    public float crazy_rate = 0.25f;
    public float attack_rate = 1f;
    //当前的攻击状态
    public string attcknow ;

    public AudioSource audioSource;
    //目标
    public Transform target;
    //最大最小距离
    public float minDistance = 2;
    public float maxDistance = 5;

    

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Hurt(2);
        }
        if (state==WolfState.Death)
        {
            animation.CrossFade(death);
        }
        else if (state==WolfState.Attack)
        {
            //自动攻击
            AutoAttack();
        }
        else
        {
            animation.CrossFade(aniname_now);
            if (aniname_now== walk)
            {
                cc.SimpleMove(transform.forward * speed);
            }
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                RandomState();
            }
        }

    }
    private void Awake()
    {
        aniname_now = Idle;
        cc = this.GetComponent<CharacterController>();
        normal = renderer.material.color;
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        attcknow = attackNormal;
    }

    private void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value==0)
        {
            aniname_now = Idle;
        }
        else
        {
            if (aniname_now!= walk)
            {
                transform.Rotate(transform.up * Random.Range(0,360));
            }
            aniname_now = walk;
        }
    }

    public void Hurt(int attack)
    {
        if (state == WolfState.Death)
        {
            return;
        }
        target = GameObject.FindGameObjectWithTag(Tags.player).transform;
        state = WolfState.Attack;
        float value = Random.Range(0, 1f);
        if (value<miss_rate)//miss
        {
            GameObject hitInfo = Instantiate(Resources.Load<GameObject>("UI/hitInfo"));
            HitInfo info = hitInfo.GetComponent<HitInfo>();
            info.setInfo("miss", this.gameObject);
            audioSource.Play();
        }
        else
        {
            this.hp -= attack;
            StartCoroutine(ShowRed());
            GameObject hitInfo = Instantiate(Resources.Load<GameObject>("UI/hitInfo"));
            HitInfo info = hitInfo.GetComponent<HitInfo>();
            info.setInfo("-"+attack.ToString(), this.gameObject);
            if (hp<=0)
            {
                state = WolfState.Death;
                Destroy(this.gameObject, 2);
                ps.GetExp(exp);
                taskPanel.Instance.AddNum();
                home.MinusNumber();
            }
        }
    }

    IEnumerator ShowRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = normal;
    }

    /// <summary>
    /// 自动攻击
    /// </summary>
    public void AutoAttack()
    {
        
        if (target!=null)
        {
            PlayerAttack attck = target.GetComponent<PlayerAttack>();
            if (attck.state==HeroState.Death)
            {
                target = null;
                state = WolfState.Idle;
                return;
            }
            float distnace = Vector3.Distance(target.position, transform.position);
            if (distnace>maxDistance)
            {
                target = null;
                state = WolfState.Idle;
            }
            //直接攻击
            else if (distnace<=minDistance)
            {
                transform.LookAt(target);
                attack_timer += Time.deltaTime;
                animation.CrossFade(aniname_now);
                if (aniname_now == attackNormal)
                {
                    if (attack_timer>attack_time)
                    {
                        Debug.Log(2);
                        //产生伤害
                        target.GetComponent<PlayerAttack>().Hurt(attack);
                        aniname_now=Idle;
                    }
                }
                else if (aniname_now == attackCrazy)
                {
                    if (attack_timer > attack_time_crazy)
                    {
                        //产生伤害
                        target.GetComponent<PlayerAttack>().Hurt(attack);
                        aniname_now = Idle;
                    }
                }
                if (attack_timer>(1f/attack_rate))
                {
                    RandomAttack();
                    attack_timer = 0;
                }
            }
            //走到最近距离攻击
            else
            {
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                animation.CrossFade(walk);
            }
        }
        else
        {
            state = WolfState.Idle;
        }
    }
    /// <summary>
    /// 随机发起一次攻击
    /// </summary>
    public void RandomAttack()
    {
        float value = Random.Range(0f, 1f);
        if (value<crazy_rate)
        {
            //狂暴
            aniname_now = attackNormal;
        }
        else
        {
            //普通攻击
            aniname_now = attackCrazy;
        }
    }

    private void OnMouseEnter()
    {
        if (PlayerAttack.Instance.isLockingTarget==false)
        {
            CursorManager.Instance.SetAttack();
        }
        
    }

    private void OnMouseExit()
    {
        if (PlayerAttack.Instance.isLockingTarget == false)
        {
            CursorManager.Instance.SetNormal();
        }
        
    }

    
}
