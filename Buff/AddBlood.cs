using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBlood : MonoBehaviour
{
    public bool isPlayer;
    public float damage;
    public HeroController hero;
    public List<AttackedController1> atts;
    float perTime = 1f;
    float timeCount;
    private void Start()
    {
        timeCount = perTime;
    }
    private void Update()
    {
        timeCount -= Time.deltaTime;
        if (timeCount <= 0)
        {
            BloodAdd();
            timeCount = perTime;
        }
    }
    private void OnEnable()
    {
        atts.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        AttackedController1 at = other.gameObject.GetComponent<AttackedController1>();
        if (at != null && at.isPlayer != isPlayer)
        {
            atts.Add(at);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AttackedController1 at = other.gameObject.GetComponent<AttackedController1>();
        if (at != null && at.isPlayer != isPlayer)
        {
            atts.Remove(at);
        }
    }
    void BloodAdd()
    {
        for (int i = 0; i < atts.Count; i++)
        {
            if (atts[i] != null)
            {
                atts[i].ResumeBlood(damage);
                hero.GetComponent<AttackedController1>().ResumeBlood(damage * 0.5f);
            }           
        }
    }
}
