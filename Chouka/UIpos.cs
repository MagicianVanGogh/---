using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIpos : MonoBehaviour
{
    public float x;
    public float y;
    RectTransform my;
    Vector3 posO;
    Vector3 target;
    bool onUI;
    bool isRun;
    bool isShow;
    private void Start()
    {
        my = GetComponent<RectTransform>();
        posO = my.position;
        Vector3 temp = posO;
        temp.x += UnityEngine.Screen.width * x;
        temp.y += UnityEngine.Screen.height * y;
        target = temp;
        onUI = false;
        isRun = false;
        isShow = false;
    }
    private void Update()
    {
        if (isRun)
        {
            if (onUI)
            {
                Gotos();
            }
            else
            {
                Returns();
            }
        }
    }
    public void ButtonTri()
    {
        if (!isShow)
        {
            OnUIMove();
        }
        else
        {
            LeaveRetuen();
        }
    }
    public void OnUIMove()
    {
        onUI = true;
        isRun = true;
        
    }
    public void LeaveRetuen()
    {
        
        onUI = false;
        isRun = true;
    }
    
    void Gotos()
    {
        isShow = true;
        if (Vector3.Distance(target, my.position) > 0.5f)
            {
                Vector3 temp = target - my.position;
                my.Translate(temp * Time.deltaTime * 3);
            }
            else
            {
                my.position = target;
                isRun = false;
            }
        
    }
    void Returns()
    {
        isShow = false;
        if (Vector3.Distance(posO, my.position) > 0.5f)
            {
                Vector3 temp = posO - my.position;
                my.Translate(temp * Time.deltaTime * 3);
            }
            else
            {
                my.position = posO;
                isRun = false;
            }

    }
}
