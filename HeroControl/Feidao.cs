using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 人物：飞刀
 * 设计思路：拥有中程攻击距离和较快的攻速，同时具有爆发性的单体技能。
 * R：普通攻击：对范围内一名角色扔出一个飞镖，造成 （攻击 * 1） 的伤害
 * SR：技能：飞刀凝聚剑气，飞天向攻击范围内敌人放出五把蓝刀，每把造成（攻击 * 1.2）的伤害，
 * 对追踪范围内任意敌人放出五把白刀，每把造成（攻击 * 0.8）的伤害，飞刀每次施放技能最多可以凝聚 2 次
 * SSR：被动：飞刀放出剑气时，获得【不可选中】Buff 5 秒
 */
public class Feidao : HeroController
{
    Vector3 posInter;
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

    public override void RunStart()
    {
        base.RunStart();
    }

    //飞刀发射一枚飞镖，对指定敌人造成 攻击*1.0的伤害
    public override void AttackStart()
    {
        StopStart();
        NormalAttack();
    }

    public override void UtimatePre()
    {
        if (level >= 2)
        {
            CannotChoose cc = GetComponent<CannotChoose>();
            if (cc == null)
            {
                cc = this.gameObject.AddComponent<CannotChoose>();
                cc.cTime = 5f;
            }
            else
            {
                Destroy(cc);
                cc = this.gameObject.AddComponent<CannotChoose>();
                cc.cTime = 5f;
            }
        }
    }
    public override void UltimateStart()
    {
        FindAttackTarget();
        if (ultimateBullet != null)
        {
            CannotChoose cc = GetComponent<CannotChoose>();
            if (cc == null)
            {
                cc = this.gameObject.AddComponent<CannotChoose>();
                cc.cTime = 2f;
            }    
            else
            {
                Destroy(cc);
                cc = this.gameObject.AddComponent<CannotChoose>();
                cc.cTime = 2.2f;
            }
            StartCoroutine(delayBullet());
            StartCoroutine(delayBullet1());
        }
    }
    //------------------------------Other Methods-----------------------------------//
    

    #region IEnumerator Field
    IEnumerator delayBullet()
    {
        int count = 5;
        for (int i = 0; i < count; i++)
        {
            Vector3 vecs = transform.position;
            vecs.x += Random.Range(-2f, 2);
            vecs.y += Random.Range(0f, 2);
            vecs.z += Random.Range(-2f, 2);
            GameObject obj = GameObject.Instantiate(ultimateBullet, vecs, Quaternion.identity);

            //CurvelBullet2 bullet = obj.GetComponent<CurvelBullet2>();
            NormalBullet1 bullet = obj.GetComponent<NormalBullet1>();
            bullet.damage = Attack * 1.2f;
            if (GetAttackTarget != null)
            {
                bullet.effectObj = damageEffect1;
                AttackedController1 c = GetAttackTarget.GetComponent<AttackedController1>();
                bullet.player = transform;
                bullet.target = c.transform;
                /*if (i % 9 == 0)
                {
                    bullet.effectObj = damageEffect1;
                    //c.attacked();
                    if (damageEffect2 != null)
                    {
                        GameObject obj1 = GameObject.Instantiate(damageEffect2);
                        ParticlesEffect1 effect = obj1.AddComponent<ParticlesEffect1>();
                        Transform target = getAttackTarget.transform;
                        effect.transform.position = MathUtil1.findChild(target, "attackedPivot").position;
                        effect.play();
                    }
                }*///特殊的效果动画，暂时不需要
            }
            else
            {
                Vector3 vec = Vector3.zero;
                vec.z += Random.Range(4, 8);
                vec.x += Random.Range(-3, 3);
                vec.y += Random.Range(1, 3);
                GameObject g = Instantiate(nullAttack.gameObject);
                g.transform.SetParent(transform);
                g.transform.localPosition = vec;
                bullet.player = transform;
                bullet.target = g.transform;
                bullet.effectObj = null;
            }
            bullet.bulleting();
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator delayBullet1()
    {
        int count = 5;
        for (int i = 0; i < count; i++)
        {
            Vector3 vecs = transform.position;
            vecs.x += Random.Range(-2f, 2);
            vecs.y += Random.Range(0f, 2);
            vecs.z += Random.Range(-2f, 2);
            GameObject obj = GameObject.Instantiate(magic2Bullet, vecs, Quaternion.identity);

            NormalBullet1 bullet = obj.GetComponent<NormalBullet1>();
            bullet.damage = Attack * 0.8f;
            AttackedController1 att = null;
            if (findFollow.targetReal.Count > 0)
            {
                int choose = Random.Range(0, findFollow.targetReal.Count);
                att = findFollow.targetReal[choose];
            }
            if (att != null)
            {
                bullet.target = att.transform;
                bullet.effectObj = damageEffect1;
                AttackedController1 c = att.GetComponent<AttackedController1>();
                bullet.player = transform;
                bullet.target = c.transform;
            }
            else
            {
                Vector3 vec = Vector3.zero;
                vec.x += Random.Range(0, 2);
                vec.z += Random.Range(-1, 1);
                vec.y += Random.Range(1, 3);
                GameObject g = Instantiate(nullAttack.gameObject);
                g.transform.SetParent(transform);
                g.transform.localPosition = vec;
                bullet.player = transform;
                bullet.target = g.transform;
                bullet.effectObj = null;
            }
            bullet.bulleting();
            yield return null;
        }
    }
    #endregion
}
