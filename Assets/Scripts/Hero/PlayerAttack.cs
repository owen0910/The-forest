using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroState 
{ 
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death


}
/// <summary>
/// 攻击的时候的状态
/// </summary>
public enum AttackState
{
    Moving,
    Idle,
    Attack
}

public class PlayerAttack : MonoBehaviour
{
    private static PlayerAttack instance;
    public static PlayerAttack Instance => instance;

    public HeroState state = HeroState.ControlWalk;
    public AttackState attackState = AttackState.Idle;
    private Animation animation;
    public float time_normalattack=0.8f;//平a动画时间
    public float timer = 0;
    public float min_distance = 5;//攻击最小距离
    private Transform target;
    private HeroMove move;
    public float rate_normalAttack=0.5f;

    public string aniname_now= "Attack1";
    public string Death;
    public string Idle;
    public string attack_normal;

    private bool showEffect=false;
    public GameObject effect;
    private PlayerStatus ps;

    public float miss_rate = 0.25f;

    public AudioSource audio;
    public Renderer renderer;
    private Color normal;

    public bool isLockingTarget = false;
    private skillInfo info = null;




    private void Awake()
    {
        instance = this;
        move = this.GetComponent<HeroMove>();
        animation = this.GetComponent<Animation>();
        ps = this.GetComponent<PlayerStatus>();
        normal = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (state==HeroState.Death)
        {
            animation.CrossFade(Death);
            return;
        }
        if (isLockingTarget==false&& Input.GetMouseButtonDown(0))
        {
            //射线检测
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool isCollider = Physics.Raycast(ray, out hitinfo);
            if (isCollider&&hitinfo.collider.tag=="enemy")
            {
                //当我们点击了一个敌人,进行普通攻击
                target = hitinfo.collider.transform;
                state = HeroState.NormalAttack;
                timer = 0;
                showEffect = false;
            }
            else
            {
                state = HeroState.ControlWalk;
                target = null;
            }
        }
        if (state==HeroState.NormalAttack)
        {
            if (target==null)
            {
                state = HeroState.ControlWalk;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance <= min_distance)
                {
                    transform.LookAt(target.position);
                    attackState = AttackState.Attack;
                    //攻击
                    timer += Time.deltaTime;
                    animation.CrossFade(aniname_now);
                    if (timer >= time_normalattack)
                    {
                        aniname_now = Idle;
                        if (showEffect == false)
                        {
                            showEffect = true;
                            //播放特效
                            GameObject gameObject= Instantiate(effect, target.position, target.rotation);
                            target.GetComponent<WolfBaby>().Hurt(getAttack());
                            Destroy(gameObject, 1f);
                        }
                    }
                    if (timer >= (1f / rate_normalAttack))
                    {
                        timer = 0;
                        showEffect = false;
                        aniname_now = attack_normal;
                    }
                }
                else
                {
                    attackState = AttackState.Moving;
                    //走向敌人
                    move.SimpleMove(target.position);

                }
            }
            
        }
        if (isLockingTarget&&Input.GetMouseButtonDown(0))
        {
            OnLockTarget();
        }

    }

    public int getAttack()
    {
        return (int)(EquipmentPanel.Instance.attack + ps.attack + ps.attack_plus);
    }

    public void Hurt(int attack)
    {
        if (state == HeroState.Death) return;
        float def = EquipmentPanel.Instance.def + ps.def + ps.def_plus;
        float temp = attack * ((200 - def) / 200);
        if (temp<1)
        {
            temp = 1;
        }
        float value = Random.Range(0f, 1f);
        if (value<miss_rate)
        {
            //miss
            GameObject hitInfo = Instantiate(Resources.Load<GameObject>("UI/hitInfo"));
            HitInfo info = hitInfo.GetComponent<HitInfo>();
            info.setInfo("miss", this.gameObject);
            audio.Play();
        }
        else
        {
            StartCoroutine(ShowRed());
            GameObject hitInfo = Instantiate(Resources.Load<GameObject>("UI/hitInfo"));
            HitInfo info = hitInfo.GetComponent<HitInfo>();
            info.setInfo("-" + attack.ToString(), this.gameObject);
            ps.nowHp -= (int)temp;
            if (ps.nowHp<=0)
            {
                state = HeroState.Death;
            }

        }
        HeadPanel.Instance.UpdateShow();
    }
    IEnumerator ShowRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        renderer.material.color = normal;
    }

    public void UseSkill(skillInfo info)
    {
        if (ps.heroType==HeroType.Magician)
        {
            if (info.ApplicableRole=="Swordman")
            {
                return;
            }

        }
        if (ps.heroType == HeroType.SwordMan)
        {
            if (info.ApplicableRole == "Magician")
            {
                return;
            }

        }
        switch (info.ApplyType)
        {
            case "Passive":
                StartCoroutine(OnPassiveSkillUse(info));
                break;
            case "Buff":
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case "SingleTarget":
                OnSingleTargetSkillUse(info);
                break;
            case "MultiTarget":
                OnMultiTargetSkillUse(info);
                break;

        }
    }

    /// <summary>
    /// 加血技能
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    IEnumerator OnPassiveSkillUse(skillInfo info)
    {
        //播放动画
        state = HeroState.SkillAttack;
        animation.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = HeroState.ControlWalk;
        //加属性
        int hp = 0;
        int mp = 0;
        if (info.ApplyProperty=="HP")
        {
            hp = info.applyValue;
        }
        else if (info.ApplyProperty == "MP")
        {
            mp = info.applyValue;
        }
        ps.GetDrug(hp, mp);
        //播放特效
        GameObject prefab = Instantiate(Resources.Load<GameObject>("effect/"+info.efx_name)
            ,transform.position,Quaternion.identity);

    }

    IEnumerator OnBuffSkillUse(skillInfo info)
    {
        //播放动画
        state = HeroState.SkillAttack;
        animation.CrossFade(info.aniname);
        yield return new WaitForSeconds(info.anitime);
        state = HeroState.ControlWalk;
        switch (info.ApplyProperty)
        {
            case "Attack":
                ps.attack *= (info.applyValue / 100f);
                break;
            case "AttackSpeed":
                rate_normalAttack *= (info.applyValue / 100f);
                break;
            case "Def":
                ps.def *= (info.applyValue / 100f);
                break;
        }
        //播放特效
        GameObject prefab = Instantiate(Resources.Load<GameObject>("effect/" + info.efx_name)
            , transform.position, Quaternion.identity);
        yield return new WaitForSeconds(info.applyTime);
        switch (info.ApplyProperty)
        {
            case "Attack":
                ps.attack /= (info.applyValue / 100f);
                break;
            case "AttackSpeed":
                rate_normalAttack /= (info.applyValue / 100f);
                break;
            case "Def":
                ps.def /= (info.applyValue / 100f);
                break;
        }

    }
    /// <summary>
    /// 锁定目标
    /// </summary>
    /// <param name="info"></param>
    public void OnSingleTargetSkillUse(skillInfo info)
    {
        CursorManager.Instance.SetLockTarget();
        //播放动画
        state = HeroState.SkillAttack;
        isLockingTarget = true;
        this.info = info;

    }
    /// <summary>
    /// 技能释放
    /// </summary>
    public void OnLockTarget()
    {
        isLockingTarget = false;
        switch (info.ApplyType)
        {
            case "SingleTarget":
                StartCoroutine(OnLockSingleTarget());
                break;
            case "MultiTarget":
                StartCoroutine(OnLockMultiTarget());
                break;
        }
    }
    /// <summary>
    /// 单个目标技能
    /// </summary>
    IEnumerator OnLockSingleTarget()
    {
        //射线检测
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        bool isCollider = Physics.Raycast(ray, out hitinfo);
        if (isCollider&&hitinfo.collider.tag=="enemy")
        {
            animation.CrossFade(info.aniname);
            yield return new WaitForSeconds(info.anitime);
            state = HeroState.ControlWalk;
            //播放特效
            GameObject prefab = Instantiate(Resources.Load<GameObject>("effect/" + info.efx_name)
                , hitinfo.transform.position, Quaternion.identity);
            hitinfo.collider.GetComponent<WolfBaby>().Hurt((int)(getAttack() * (info.applyValue / 100f)));
        }
        else
        {
            state = HeroState.NormalAttack;
        }
        CursorManager.Instance.SetNormal();
    }

    IEnumerator OnLockMultiTarget()
    {

        //射线检测
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        bool isCollider = Physics.Raycast(ray, out hitinfo);
        print(hitinfo);
        //bool isCollider = Physics.Raycast(ray, out hitinfo, 1000,1<<LayerMask.NameToLayer("Gound"));
        if (isCollider)
        {
            animation.CrossFade(info.aniname);
            yield return new WaitForSeconds(info.anitime);
            state = HeroState.ControlWalk;
            //播放特效
            GameObject prefab = Instantiate(Resources.Load<GameObject>("effect/" + info.efx_name)
                , hitinfo.point+Vector3.up*0.5f, Quaternion.identity);
            prefab.GetComponent<MagicSphere>().attack=getAttack() * info.applyValue / 100f;
        }
        else
        {
            state = HeroState.NormalAttack;
        }
        CursorManager.Instance.SetNormal();
    }

    public void OnMultiTargetSkillUse(skillInfo info)
    {
        CursorManager.Instance.SetLockTarget();
        //播放动画
        state = HeroState.SkillAttack;
        isLockingTarget = true;
        this.info = info;
    }
}
