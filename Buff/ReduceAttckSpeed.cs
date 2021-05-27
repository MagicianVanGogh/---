using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceAttckSpeed : BuffType
{
    float readd;
    public float timeCount;
    bool isGo = false;
    void Resumes()
    {
        GetComponent<HeroController>().AttackSpeed = GetComponent<HeroController>().AttackSpeed + readd;
        Destroy(this, 0.1f);
    }
    public void SetMessage(float ti,float reduce)
    {
        cTime = ti;
        readd = GetComponent<HeroData>().attackSpeed * reduce;
        GetComponent<HeroController>().AttackSpeed = GetComponent<HeroController>().AttackSpeed - readd;
        timeCount = cTime;
        isGo = true;
    }
    private void Update()
    {
        if (isGo)
        {
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
            {
                Resumes();
                isGo = false;
            }
        }       
    }
}
