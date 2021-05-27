using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 人物：酒徒
 * 设计思路：近战重装英雄，拥有极强的坦度和控制力。
 * R：普通攻击：向面前一名角色使用狼牙棒重击，造成 （攻击 * 1） 的伤害
 * SR：技能：向上跳跃，释放酒气，击退周围敌人并打断敌人当前状态，对范围内每个敌人造成 （攻击 * 1.2） 的伤害
 * SSR：被动：施放技能跃下时，使范围内随机敌人的攻速减少 50%，持续 3 秒。
 */
public class Jiutu : HeroController
{
    Vector3 posInter;
    public Attacked atts;
    public override void StartSet()
    {
        //-----------------GET DATA-----------------//
        base.StartSet();
        HeroData = GetComponent<HeroData>();
        Attack = HeroData.attack;
        ReBlue = HeroData.reBlue;
        MaxBlood = HeroData.maxLife;
        AttackSpeed = HeroData.attackSpeed;
        Speed = HeroData.speed;
        TimeWait = HeroData.startTime;
        //------------------------------------------//
        atts.isPlayer = isPlayer;
        Rig = GetComponent<Rigidbody>();
        MyNav = GetComponent<NavMeshAgent>();
        MyNav.speed = Speed;
        Anim = GetComponent<Animator>();
        FirstIn = true;
        GetComponent<AttackedController1>().BloodUIControllers = ca.GetComponent<BloodUIController>();
        BlueSlider = ca.GetComponent<BloodUIController>().blueSlider;
        ca.GetComponent<BloodUIController>().bloodSlider.maxValue = MaxBlood;
        ca.GetComponent<BloodUIController>().bloodSlider.value = MaxBlood;
        Blue = 0;
        BlueSlider.value = 0;
        posInter = MathUtil1.findChild(transform, "attackedPivot").position - ca.transform.position;

        //阵营
        GetComponent<AttackedController1>().isPlayer = isPlayer;
        findAttack.isPlayer = isPlayer;
        findFollow.isPlayer = isPlayer;
        if (isPlayer)
        {
            bloodColor.color = Color.green;
        }
        else
        {
            bloodColor.color = Color.red;
        }
    }
    public override void UpdateSet()
    {
        if (ca != null)
        {
            ca.gameObject.transform.rotation = Camera.main.transform.rotation;
            Vector3 tempVec = ca.transform.position;
            Vector3 ytemp = MathUtil1.findChild(transform, "attackedPivot").position - posInter;
            tempVec.y = ytemp.y;
            ca.transform.position = tempVec;
        }
        Blue = Mathf.Clamp(Blue + ReBlue * Time.deltaTime, 0, 100);
        BlueSlider.value = Blue;
    }
    public override void AttackStart()
    {
        StopStart();
        NormalAttack();
    }
    public override void NormalAttack()
    {
        if (GetAttackTarget != null)
        {
            GameObject obj = GameObject.Instantiate(damageEffect1);
            ParticlesEffect1 effect = obj.AddComponent<ParticlesEffect1>();
            Transform target = GetAttackTarget.transform;
            effect.transform.position = MathUtil1.findChild(target, "attackedPivot").position;
            effect.play();
        }
    }

    void Attacks()
    {
        if (GetAttackTarget != null)
        {
            GetAttackTarget.attackedWithDamage(Attack,this);
        }
    }
    public override void UltimateStart()
    {
       if (damageEffect3 != null)
       {
            if (findFollow.targetReal.Count > 0)
            {                
                if (level >= 2)
                {
                    int num = Random.Range(0, findFollow.targetReal.Count);
                    AttackedController1 attackTarget = findFollow.targetReal[num];
                    GameObject obj = GameObject.Instantiate(damageEffect3);
                    ParticlesEffect1 effect = obj.AddComponent<ParticlesEffect1>();
                    effect.time = 2f;
                    effect.transform.position = attackTarget.transform.position;
                    ReduceAttckSpeed ras = attackTarget.GetComponent<ReduceAttckSpeed>();
                    if (ras == null)
                    {
                        ras = attackTarget.gameObject.AddComponent<ReduceAttckSpeed>();
                        ras.SetMessage(3f, 0.5f);
                    }
                    else
                    {
                        ras.timeCount = 3f;
                    }
                    effect.play();
                }                
            }           
       }      
    }
    public override void UtimatePre()
    {
        atts.hero = this;
        atts.damage = Attack;
    }
}
