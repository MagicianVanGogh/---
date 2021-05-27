using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 人物：铁甲
 * 设计思路：近战法术英雄，拥有极强的爆发和群攻输出。
 * R：普通攻击：在敌人其周围随机位置产生一个魔法光球，对其中的敌人造成 （攻击 * 1） 的伤害
 * SR：技能：铁甲将盾重击于地，对四周敌人造成 （攻击 * 1 + 敌人当前生命 * 10%） 伤害，对前方敌人会造成 2 次
 * 伤害
 * SSR：被动：攻击时对前方一名敌人施加黑雾，使其受到的治疗效果减少 60 %，持续 5 秒
 */

public class TieJia : HeroController
{
    public GameObject damageEffect4;
    public GameObject damageEffect5;
    Vector3 posInter;
    public caorenUlt Udamage1;
    public caorenUlt Udamage2;
    public override void StartSet()
    {
        //Debug.Log("Start Begin");
        //-----------------GET DATA-----------------//
        base.StartSet();
        HeroData = GetComponent<HeroData>();
        Attack = HeroData.attack;
        ReBlue = HeroData.reBlue;
        BloodTake = HeroData.bloodTake;
        MaxBlood = HeroData.maxLife;
        AttackSpeed = HeroData.attackSpeed;
        Speed = HeroData.speed;
        TimeWait = HeroData.startTime;
        //Debug.Log("Give Data over");
        //------------------------------------------//
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
        //Debug.Log("collider and slider");
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
        Udamage1.isPlayer = isPlayer;
        Udamage2.isPlayer = isPlayer;
        //Debug.Log("choose team");
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
        if (damageEffect1 != null && GetAttackTarget != null)
        {
            
            StartCoroutine(delayBullet());
            if (level >= 2)
            {
                GameObject obj = GameObject.Instantiate(damageEffect1);
                ParticlesEffect1 effect = obj.AddComponent<ParticlesEffect1>();
                Transform target = GetAttackTarget.transform;
                effect.transform.position = target.position;
                effect.play();

                CutBlood ras = GetAttackTarget.GetComponent<CutBlood>();
                if (ras == null)
                {
                    ras = GetAttackTarget.gameObject.AddComponent<CutBlood>();
                    ras.SetMessage(5f);
                }
                else
                {
                    ras.timeCount = 0f;
                }
            }            
        }        
    }
    float padding = 1f;
    IEnumerator delayBullet()
    {
        int count = 1;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.Instantiate(damageEffect5);
            obj.GetComponent<rangeDamage>().isPlayer = isPlayer;
            obj.GetComponent<rangeDamage>().attack = Attack * 2;
            obj.GetComponent<rangeDamage>().resource = this;
            if (GetAttackTarget != null)
            {
                AttackedController1 c = GetAttackTarget;
                obj.transform.position = c.transform.position + new Vector3(Random.Range(-padding, padding), 0.12f, Random.Range(-padding, padding));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    public override void UtimatePre()
    {
        Udamage1.resource = this;
        Udamage2.resource = this;
        Udamage1.attack = Attack;
        Udamage2.attack = Attack;
    }
}
