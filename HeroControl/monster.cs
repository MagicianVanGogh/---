using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster : HeroController
{
    Vector3 posInter;
    public override void StartSet()
    {
        //Debug.Log("Start Begin");
        //-----------------GET DATA-----------------//
        base.StartSet();
        HeroData = GetComponent<HeroData>();
        Attack = HeroData.attack;
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
            GetAttackTarget.attackedWithDamage(Attack, this);
        }
    }
}
