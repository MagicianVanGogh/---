using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REAttack : BuffType
{
    float timeCount;
    float re;
    bool goTime = false;
    AttackedController1 my;

    private void Start()
    {
        re = my.damageSize;
        my.damageSize = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (goTime)
        {
            timeCount += Time.deltaTime;
            if (timeCount > cTime)
            {
                Finished();
            }
        }
    }
    void Finished()
    {
        my.damageSize += re;
        Destroy(this);
    }
    public void SetMessage(float ti, AttackedController1 tar)
    {
        this.cTime = ti;
        this.my = tar;
        goTime = true;
    }
}
