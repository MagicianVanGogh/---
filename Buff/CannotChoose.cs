using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotChoose : BuffType
{
    AttackedController1 att;
    void Start()
    {
        numBuffMax = 1;
        att = GetComponent<AttackedController1>();
        att.isTarget = false;
        Invoke("OverBuff",cTime);
    }
    public void OverBuff()
    {
        att.isTarget = true;
        Destroy(this);
    }
}
