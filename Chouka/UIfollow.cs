using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIfollow : MonoBehaviour
{
    public Canvas ca;
    Vector3 posInter;
    // Update is called once per frame
    private void Start()
    {
        posInter = MathUtil1.findChild(transform, "attackedPivot").position - ca.transform.position;
    }
    void Update()
    {
        if (ca != null)
        {
            ca.gameObject.transform.rotation = Camera.main.transform.rotation;
            Vector3 tempVec = ca.transform.position;
            Vector3 ytemp = MathUtil1.findChild(transform, "attackedPivot").position - posInter;
            tempVec.y = ytemp.y;
            ca.transform.position = tempVec;
        }
    }
}
