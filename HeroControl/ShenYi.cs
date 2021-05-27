using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 人物：神医
 * 设计思路：拥有治疗能力的辅助术士。
 * R：普通攻击：对范围内一名角色发射光球，造成 （攻击 * 1） 的伤害
 * SR：技能：在自身附近开启治愈之光，对范围内友方每秒回复 （攻击 * 1）
 * 的生命，每对一名右方治疗，自身回复 （攻击 * 0.5）的生命。
 * SSR：被动：神医施放技能时，获得免伤效果，持续5秒。
 */

public class ShenYi : HeroController
{
    Vector3 posInter;
    public AddBlood atts;
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
        atts.isPlayer = !isPlayer;
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
    public override void UtimatePre()
    {
        atts.hero = this;
        atts.damage = Attack;
        if (level >= 2)
        {
            REAttack re = this.gameObject.AddComponent<REAttack>();
            re.SetMessage(5f, this.GetComponent<AttackedController1>());
        }        
    }
}
