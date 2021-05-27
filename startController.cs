using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startController : MonoBehaviour
{
    public Text ready;
    public Text timestart;
    public int starttime;
    // Start is called before the first frame update
    void Start()
    {
        ready.text = "准备";
        timestart.text = starttime.ToString();
        StartCoroutine(Ready());
    }
    IEnumerator Ready()
    {
        while (starttime > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            starttime--;
            timestart.text = starttime.ToString();
        }
        ready.text = "游戏开始";
        ready.color = Color.red;
    }
}
