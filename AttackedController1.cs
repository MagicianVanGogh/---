using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AttackedController1 : MonoBehaviour {
    BloodUIController bloodUIController;
    public List<Action> actions;

    // Use this for initialization
    Animator anim;
    public bool isFar = true;
    public bool isTarget;
    public bool isPlayer;

    public float damageSize = 1;
    public float redamageSize = 1;

    void Start () {
        anim = GetComponent<Animator>();
        damageSize = 1;
        redamageSize = 1;
        //isTarget = false;
    }
    public void SetTarget()
    {
        isTarget = true;
    }
    Coroutine co;

    public BloodUIController BloodUIControllers { get => bloodUIController; set => bloodUIController = value; }

    public void attacked()
    {
        if(co != null)
        {
            StopCoroutine(co);
        }
        if (anim != null)
        {
            anim.SetBool("Attacked", true);
            //bloodUIController.bloodSlider.value = Mathf.Clamp(bloodUIController.bloodSlider.value- damage,0,100);
            co = StartCoroutine(delay());
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }

    public void attackedWithDamage(float damage, HeroController resource)
    {
        if (co != null)
        {
            StopCoroutine(co);
        }
        //anim.SetBool("Attacked", true);
        float bloods = bloodUIController.bloodSlider.value;
        bloodUIController.bloodSlider.value = Mathf.Clamp(bloodUIController.bloodSlider.value - damage * damageSize, 0, bloodUIController.bloodSlider.maxValue);
        co = StartCoroutine(delay());
        if (resource != null)
        {
            AttackedController1 bloodUI = resource.gameObject.GetComponent<AttackedController1>();
            bloodUI.ResumeBlood((bloods - bloodUIController.bloodSlider.value) * resource.BloodTake);
            //bloodUI.bloodUIController.bloodSlider.value = Mathf.Clamp(bloodUI.bloodUIController.bloodSlider.value + (bloods - bloodUIController.bloodSlider.value) * resource.BloodTake * bloodUI.redamageSize, 0, bloodUI.bloodUIController.bloodSlider.maxValue);
        }                                
    }
    public void ResumeBlood(float damage)
    {
        float bloods = bloodUIController.bloodSlider.value;
        bloodUIController.bloodSlider.value = Mathf.Clamp(bloodUIController.bloodSlider.value + damage * redamageSize, 0, bloodUIController.bloodSlider.maxValue);
    }
    IEnumerator delay()
    {       
        co = null;
        //anim.SetBool("Attacked", false);
        if (bloodUIController.bloodSlider.value <= 0)
        {
            if (anim!=null)
            {
                anim.SetBool("Die", true);
            }           
            isTarget = false;
            Destroy(gameObject, 1.5f);
        }
        yield return new WaitForSeconds(0.25f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
