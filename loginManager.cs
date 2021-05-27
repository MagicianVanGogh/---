using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loginManager : MonoBehaviour
{
    public Text text;
    public Text logonName;
    public GameObject panlenow;
    private void Start()
    {
        if (GetInf.Instance.login)
        {
            panlenow.SetActive(false);
            logonName.text = GetInf.Instance.loginname;
        }
    }

    public void login()
    {
        GetInf.Instance.loginname = text.text;
        MyHeroController.Instance.getData(text.text);
        
        logonName.text = text.text;
        panlenow.SetActive(false);
    }
}
