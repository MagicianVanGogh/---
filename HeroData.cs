using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroData : MonoBehaviour
{
    public string heroname;
    public bool GetDataByHand;
    public bool NoNAV = false;
    public float maxLife;//最大生命          x  * 100
    public float attack;//攻击力             x
    public float reBlue;//回蓝               x  * 0.5
    public float bloodTake;//回血

    public float speed;//移动速度            x  * 0.5
    public float attackSpeed;//攻击速度      x  
    public float startTime;//开始停顿时长

    public float followRange;//跟随范围      x  * 0.5
    public float attackRange;//攻击范围      x  * 0.5   followRange >= attackRange

    SphereCollider followCollider;
    SphereCollider attackCollider;

    public int level;

    private void Awake()
    {
        if (!GetDataByHand)       
        {
            GetDataRandom();
        }
        else
        {
            GetComponent<HeroController>().findAttack.NoNAV = NoNAV;
            if (!NoNAV)
            {
                GetComponent<NavMeshAgent>().enabled = true;
                GetComponent<HeroController>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                GetComponent<AttackedController1>().SetTarget();
            }
            Dataload();
        }
        
    }
    public void Dataload()
    {
        List<MyHerosData> l = MyHeroController.Instance.ReadJson();
        for (int i = 0; i < l.Count; i++)
        {
                if (l[i].heroName == heroname)
                {
                    heroname = l[i].heroName;
                    maxLife = l[i].maxLife;
                    attack = l[i].attack;
                    reBlue = l[i].reblue;
                    bloodTake = l[i].bloodTake;
                    speed = l[i].speed;
                    attackSpeed = l[i].attackSpeed;
                    startTime = l[i].startTime;
                    followRange = l[i].followRange;
                    attackRange = l[i].attackRange;
                    level = l[i].level;
                break;
                }
            
        }
    }
    void GetDataRandom()
    {
        maxLife = Random.Range(100f, 1000);
        attack = Random.Range(1f, 50);
        reBlue = Random.Range(0f, 15);
        bloodTake = Random.Range(0f, 2);
        speed = Random.Range(0f, 3);
        attackSpeed = Random.Range(0.2f, 2.5f);
        attackRange = Random.Range(1f, 10);
        followRange = Random.Range(attackRange, 30);
    }

    
}
