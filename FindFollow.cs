using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFollow : MonoBehaviour
{
    public List<AttackedController1> target;
    public List<AttackedController1> targetReal;
    public bool isPlayer;
    private void OnTriggerEnter(Collider other)
    {
        AttackedController1 at = other.gameObject.GetComponent<AttackedController1>();
        if (at != null)
        {
            target.Add(at);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AttackedController1 at = other.gameObject.GetComponent<AttackedController1>();
        if (at != null)
        {
            target.Remove(at);
        }
    }
    private void Update()
    {
        TestTarget();
    }
    public void TestTarget()
    {
        targetReal.Clear();
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i] != null && target[i].isTarget && target[i].isPlayer != isPlayer)
            {
                targetReal.Add(target[i]);
            }
        }
    }
}
