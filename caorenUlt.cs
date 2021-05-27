using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caorenUlt : MonoBehaviour
{
    public bool isPlayer;
    public float attack;
    public HeroController resource;
    public List<AttackedController1> attacks;
    private void OnEnable()
    {
        Invoke("RangeHurt", 0.2f);
        attacks.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        AttackedController1 att = other.gameObject.GetComponent<AttackedController1>();
        if (att != null && att.isPlayer != isPlayer&& !att.CompareTag("tower"))
        {
            attacks.Add(att);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AttackedController1 att = other.gameObject.GetComponent<AttackedController1>();
        if (att != null && att.isPlayer != isPlayer && !att.CompareTag("tower"))
        {
            attacks.Remove(att);
        }
    }
    void RangeHurt()
    {
        for (int i = 0; i < attacks.Count; i++)
        {
            if (attacks[i] != null)
            {
                attacks[i].attackedWithDamage(attack + attacks[i].BloodUIControllers.bloodSlider.value * 0.1f, resource);
            }
        }
    }
}
