using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBlood : BuffType
{
    public float timeCount;
    float re;
    bool goTime = false;
    AttackedController1 my;

    private void Start()
    {
        re = my.redamageSize * 0.6f;
        my.redamageSize -= re;
    }
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
        my.redamageSize += re;
        Destroy(this);
    }
    public void SetMessage(float ti)
    {
        this.cTime = ti;
        this.my = GetComponent<AttackedController1>();
        goTime = true;
    }
}
