using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDestroy : MonoBehaviour
{
    public AttackedController1 hero;
    public void TestDes(TimeInspctor re)
    {
        hero.attackedWithDamage(100000, null);
        re.finished = true;
    }
}
