using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeDamage : MonoBehaviour
{
    public bool isPlayer;
    public float attack;
    public HeroController resource;
    public List<AttackedController1> attacks;
    public bool average;
    private void OnTriggerEnter(Collider other)
    {
        AttackedController1 att = other.gameObject.GetComponent<AttackedController1>();
        if (att != null && att.isPlayer != isPlayer)
        {
            attacks.Add(att);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AttackedController1 att = other.gameObject.GetComponent<AttackedController1>();
        if (att != null && att.isPlayer != isPlayer)
        {
            attacks.Remove(att);
        }
    }
    private void Start()
    {
        Invoke("RangeHurt", 0.2f);
        Destroy(gameObject, 1.5f);
    }
    void RangeHurt()
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            if (attacks[i] != null)
            {
                if (average)
                {
                    attacks[i].attackedWithDamage(attack / attacks.Count, resource);
                }
                else
                {
                    attacks[i].attackedWithDamage(attack, resource);
                }
            }
        }
    }
}
