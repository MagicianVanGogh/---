using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Castle : HeroController
{
    bool isOccupied;
    public GameObject BulletP;
    public GameObject Light;
    public override void StartSet()
    {
        //-----------------GET DATA-----------------//
        base.StartSet();
        HeroData = GetComponent<HeroData>();
        Attack = HeroData.attack;
        //ReBlue = HeroData.reBlue;
        MaxBlood = HeroData.maxLife;
        AttackSpeed = HeroData.attackSpeed;
        Speed = 0;
        //------------------------------------------//
        isOccupied = false;

        Rig = GetComponent<Rigidbody>();
        MyNav = GetComponent<NavMeshAgent>();
        MyNav.speed = 0;
        //Anim = GetComponent<Animator>();
        //FirstIn = true;
        GetComponent<AttackedController1>().BloodUIControllers = ca.GetComponent<BloodUIController>();
        // BlueSlider = ca.GetComponent<BloodUIController>().blueSlider;
        ca.GetComponent<BloodUIController>().bloodSlider.maxValue = MaxBlood;
        ca.GetComponent<BloodUIController>().bloodSlider.value = MaxBlood;
        //Blue = 0;
        //BlueSlider.value = 0;

        GetComponent<AttackedController1>().SetTarget();
        //阵营
        GetComponent<AttackedController1>().isPlayer = isPlayer;
        findAttack.isPlayer = isPlayer;
        //findFollow.isPlayer = isPlayer;
        if (isPlayer)
        {
            bloodColor.color = Color.green;
        }
        else
        {
            bloodColor.color = Color.red;
        }
        TimeWait = 0;       
    }
    public override void UpdateSet()
    {
        if (ca != null)
            ca.gameObject.transform.rotation = Camera.main.transform.rotation;
        if (findAttack.targetReal.Count > 0)
        {
            if (!isOccupied)
            {
                GetAttackTarget = findAttack.targetReal[0];
                StartCoroutine(AttackMove());
            }
        }
        if (ca.GetComponent<BloodUIController>().bloodSlider.value <= 0)
        {
            Light.SetActive(false);
        }
    }
    IEnumerator AttackMove()
    {
        isOccupied = true;
        while (true)
        {
            Vector3 vec = GetAttackTarget.transform.position - BulletP.transform.position;
            //print(Vector3.Angle(vec, BulletP.transform.forward));
            if (Vector3.Angle(vec, BulletP.transform.forward) < 10f)
            {            
                NormalAttack();
                break;
            }           
            Quaternion rotate = Quaternion.LookRotation(vec);
            BulletP.transform.rotation = Quaternion.Slerp(BulletP.transform.rotation, rotate, 2 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(AttackSpeed);
        isOccupied = false;
    }
}
