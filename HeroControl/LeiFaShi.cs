using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 人物：雷法师
 * 设计思路：拥有高攻击力的法师。
 * R：普通攻击：对范围内一名角色施加白色电击，造成 （攻击 * 1） 的伤害
 * SR：技能：对攻击范围内一名敌人释放天雷，在该敌人四周随机产生天雷，每个天雷对天雷范围
 * 内所有敌人共造成（攻击 * 2）的伤害，伤害由范围内敌人平均分摊。最多可召唤 2 次天雷。
 * SSR：被动：雷法师获得 20 % 的吸血效果。
 */
public class LeiFaShi : HeroController
{
    public GameObject damageEffect4;//技能特效
    Vector3 posInter;
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
        //Debug.Log("choose team");

        if (level >= 2)
        {
            BloodTake += 0.2f;
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
    public override void RunStart()
    {
        base.RunStart();
    }
    public override void AttackStart()
    {
        StopStart();
        NormalAttack();
    }
    public override void UltimateStart()
    {
        FindAttackTarget();
        if (ultimateBullet != null)
        {
            StartCoroutine(delayBullet());
        }
    }
    float padding = 3f;//范围
    IEnumerator delayBullet()
    {
        int count = 12;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.Instantiate(damageEffect4);
            obj.GetComponent<rangeDamage>().isPlayer = isPlayer;
            obj.GetComponent<rangeDamage>().attack = Attack * 2;
            obj.GetComponent<rangeDamage>().resource = this;
            if (GetAttackTarget != null)
            {
                AttackedController1 c = GetAttackTarget.GetComponent<AttackedController1>();
                obj.transform.position = c.transform.position + new Vector3(Random.Range(-padding, padding), 0.12f, Random.Range(-padding, padding));
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                Vector3 vec = Vector3.zero;
                vec.z += 2;
                GameObject g = Instantiate(nullAttack.gameObject);
                g.transform.SetParent(transform);
                g.transform.localPosition = vec;
                AttackedController1 c = g.GetComponent<AttackedController1>();
                obj.transform.position = c.transform.position + new Vector3(Random.Range(-padding, padding), 0.12f, Random.Range(-padding, padding));
                yield return new WaitForSeconds(0.05f);
            }
            /*if (i % 2 == 0)
            {
                c.attacked();

            }*/
        }
    }
}
