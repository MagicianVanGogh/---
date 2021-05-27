using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    #region Private Field   
    public  AttackedController1 getTarget;//追踪的目标
    AttackedController1 getAttackTarget;//攻击的目标
    public AttackedController1 nullAttack;//无攻击标记
    NavMeshAgent myNav;//自动寻路
    Animator anim;//人物动画
    bool firstIn;//入场标记
    public Image bloodColor;//血条颜色
    public GameObject vec;//默认前进点
    
    //武器/特效
    public GameObject attackBullet;//普通攻击武器
    public GameObject damageEffect1;//普攻特效
        
    public GameObject magic2Bullet;//技能武器1
    public GameObject ultimateBullet;//技能武器2
    public GameObject damageEffect2;//技能特效
    public GameObject damageEffect3;//技能特效
    #endregion
    #region Public Field   
    //碰撞检测器
    public FindFollow findFollow;
    public FindAttack findAttack;
    public int level;
    Rigidbody rig;
    //画布拾取
    public Canvas ca;
    Slider blueSlider;//蓝条拾取
    //英雄属性
    public bool isPlayer;
    float attack;//攻击力
    float blue;//英雄蓝量
    float reBlue;//回蓝速度
    float maxBlood;//最大血量
    float attackSpeed;//攻击速度
    float speed;//移动速度
    float timeWait;//开始时等待
    float bloodTake;//攻击回复
    [SerializeField]
    HeroData heroData;//读取数据

    public float Attack { get => attack; set => attack = value; }
    public float Blue { get => blue; set => blue = value; }
    public float ReBlue { get => reBlue; set => reBlue = value; }
    public float MaxBlood { get => maxBlood; set => maxBlood = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Speed { get => speed; set => speed = value; }
    public HeroData HeroData { get => heroData; set => heroData = value; }
    public NavMeshAgent MyNav { get => myNav; set => myNav = value; }
    public Animator Anim { get => anim; set => anim = value; }
    public bool FirstIn { get => firstIn; set => firstIn = value; }
    public AttackedController1 GetAttackTarget { get => getAttackTarget; set => getAttackTarget = value; }
    public Rigidbody Rig { get => rig; set => rig = value; }
    public Slider BlueSlider { get => blueSlider; set => blueSlider = value; }
    public float TimeWait { get => timeWait; set => timeWait = value; }
    public float BloodTake { get => bloodTake; set => bloodTake = value; }
    #endregion
    #region MonoBehaviour Callbacks
    private void Start()
    {
        StartSet();
    }
    public virtual void StartSet()
    {
        level = GetComponent<HeroData>().level;
        findAttack.NoNAV = GetComponent<HeroData>().NoNAV;
        FindFollow findFollow = GetComponentInChildren<FindFollow>();
        if (findFollow != null)
        {
            SphereCollider followCollider = findFollow.GetComponent<SphereCollider>();
            followCollider.radius = GetComponent<HeroData>().followRange;
        }
        FindAttack findAttacks = GetComponentInChildren<FindAttack>();
        if (findAttacks != null)
        {
            SphereCollider attackCollider = findAttacks.GetComponent<SphereCollider>();
            attackCollider.radius = GetComponent<HeroData>().attackRange;
        }
    }
    public void ResetInfo(bool isP)
    {
        isPlayer = isP;
        GetComponent<AttackedController1>().isPlayer = isPlayer;
        if (isPlayer)
        {
            bloodColor.color = Color.green;
        }
        else
        {
            bloodColor.color = Color.red;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    private void Update()
    {
        //FindTargets();
        UpdateSet();
    }
    public virtual void UpdateSet()
    {

    }
    public virtual void RunStart()
    {

    }
    private void FixedUpdate()
    {
        Rig.velocity = new Vector3(0, 0, 0);
    }
    #endregion
    #region Public Methods


    #endregion
    #region Private Methods
    //用于动画过程调用，动画开始时调用
    //actionName: 捕捉动画类目
    void HeroStart(string actionName)
    {
        switch (actionName)
        {
            case "Stand":
                if (FirstIn)
                {
                    StandStart();                    
                }
                break;
            /*case "Run":
                RunStart();
                break;*/
            case "Attack":
                AttackStart();
                break;
            case "Ultimate":
                UltimateStart();
                break;
            default:
                break;
        }
    }
    void ActionDone(string actionName)
    {
        switch (actionName)
        {
            case "Ultimate":
                Blue = 0;
                Anim.SetBool("magic", false);
                break;
        }
    }
    void PreAction(string actionName)
    {
        switch (actionName)
        {
            case "Run":
                Anim.speed = 1;
                break;
            case "Attack":
                //StopStart();
                if (getAttackTarget != null)
                {
                    transform.LookAt(GetAttackTarget.transform);
                }                
                Anim.speed = AttackSpeed;
                break;
            case "Ultimate":
                Anim.speed = 1;
                UtimatePre(); 
                //StopStart();
                break;
            case "Death":
                //StopStart();
                MyNav.enabled = false;
                Anim.speed = 1;
                break;
        }
    }
    //起始准备工作，一般包括布置时间和第一次搜寻跟踪对象
    public void StandStart()
    {
        FirstIn = false;
        //MyNav.enabled = false;
        MyNav.speed = 0;
        //FindTarget();
        Invoke("GetStart", TimeWait);//此处数值之后替换变量
    }
    /*public virtual void RunStart()
    {
        //MyNav.enabled = true;
        MyNav.speed = speed;
        //默认目标方向
        if (getTarget == null)
        {
            if (isPlayer)
            {
                MyNav.SetDestination(new Vector3(-21.73f,transform.position.y,-3.359f));
            }
            else
            {
                MyNav.SetDestination(new Vector3(-36.01f, transform.position.y, -3.359f));
            }
            
        }
        else
        {
            if (getTarget != null)
            {
                MyNav.SetDestination(getTarget.gameObject.transform.position);
            }
            else
            {
                findFollow.target.Remove(getTarget);
            }
        }
        //FindTarget();
    }*/

    //飞刀发射一枚飞镖，对指定敌人造成 攻击*1.0的伤害
    public virtual void AttackStart()
    {
        
    }


    public virtual void UltimateStart()
    {
        
    }
    //------------------------------Other Methods-----------------------------------//
    public void FindAttackTarget()
    {
        List<AttackedController1> att = findAttack.targetReal;
        if (att.Count > 0)
        {
            GetAttackTarget = att[0];
        }
        else
        {
            GetAttackTarget = null;
        }
        if (Blue >= 100 && level >= 1)
        {
            Anim.SetBool("magic", true);
        }
    }
    void GetStart()
    {
        Anim.SetBool("start", true); //可以开始行走了
    }
    //找到追踪目标
    /*public void FindTargets()
    {
        if (findFollow != null)
        {
            if (findFollow.targetReal.Count > 0)
            {
                getTarget = findFollow.targetReal[0];
            }
            else
            {
                getTarget = null;
            }
            if (Blue >= 100 && level >= 1)
            {
                Anim.SetBool("magic", true);
            }
        }       
    }*/
    //攻击之前停止的操作
    public void StopStart()
    {
        getTarget = null;
        //MyNav.enabled = false;
        MyNav.speed = 0;
        FindAttackTarget();
    }
    //普通攻击
    public virtual void NormalAttack()
    {
        GameObject obj = GameObject.Instantiate(attackBullet);
        NormalBullet1 bullet = obj.GetComponent<NormalBullet1>();
        bullet.damage = Attack;
        bullet.player = transform;
        if (GetAttackTarget != null)
        {
            
            bullet.target = GetAttackTarget.transform;
            bullet.effectObj = damageEffect1;
            GetAttackTarget = null;
        }
        else
        {
            Vector3 vec = Vector3.zero;
            vec.z += 2;
            GameObject g = Instantiate(nullAttack.gameObject);
            g.transform.SetParent(transform);
            g.transform.localPosition = vec;
            bullet.target = g.transform;
            bullet.effectObj = null;
        }
        bullet.bulleting();
    }

    public virtual void UtimatePre()
    {

    }
    #endregion
    void StopTimes(float ti)
    {
        GetComponent<Animator>().speed = 0;
        if (GetComponent<Animator>().GetBool("magic"))
        {
            ActionDone("Ultimate");
        }
        
        StartCoroutine(waitTimeResume(ti));
    }
    IEnumerator waitTimeResume(float ti)
    {
        yield return new WaitForSeconds(ti);
        GetComponent<Animator>().speed = 1;
    }
}
