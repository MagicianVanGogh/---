using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playback : MonoBehaviour
{
    Animator anim;
    float times;
    float timeCount;
    Rigidbody rig;
    public Transform target;
    bool goTime = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        if (anim != null)
        {
            anim.SetBool("attacked", true);
        }
        else
        {
            Destroy(this);
        }
    }
    public void SetMessage(float ti,Transform tar)
    {
        this.times = ti;
        this.target = tar;
        goTime = true;
    }
    private void Update()
    {
        if (goTime && anim != null)
        {
            timeCount += Time.deltaTime;
            if (timeCount > times)
            {
                anim.SetBool("attacked", false);
                Destroy(this, 0.1f);
            }
            else
            {
                if (target != null)
                {
                    Vector3 V = transform.position - target.transform.position;
                    V = V.normalized;
                    rig.AddForce(V * 20);
                }
            }
        }
    }       
}
