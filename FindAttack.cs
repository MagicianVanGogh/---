using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAttack : MonoBehaviour
{
    public Animator anim;
    public bool NoNAV;
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

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        TestTarget();
        if (anim != null)
        {
            if (!anim.GetBool("attack") && targetReal.Count > 0)
            {
                anim.SetBool("attack", true);
            }
            if (anim.GetBool("attack") && targetReal.Count == 0)
            {
                anim.SetBool("attack", false);
            }
        }       
    }
    
}
