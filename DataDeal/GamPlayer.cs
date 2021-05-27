using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GamPlayer : MonoBehaviour
{
    public List<GameObject> herosShow;
    private int nowShow;
    private GameObject nowHero;
    //是否被拖拽
    private bool onDrag = false;
    //旋转速度
    public float speed = 6f;
    //阻尼速度
    private float zSpeed;
    //鼠标沿水平方向拖拽的增量
    private float X;
    //鼠标沿竖直方向拖拽的增量     
    //private float Y;
    //鼠标移动的距离
    private float mXY;
    /// <summary>
    /// UI类
    /// </summary>
    public Text attack, life, reblue, takeblood, speedui, aspeed, butime, follow, attrange, level;
    public Text getName;
    public Image sources;
    public Text des;
    private void Start()
    {
        nowShow = 0;
        Invoke("showAHero", 0.1f);
        //showAHero();
        Time.timeScale = 1;
    }


    void showAHero()
    {
        if (nowHero != null)
        {
            Destroy(nowHero);
        }
        
        nowHero = Instantiate(herosShow[nowShow]);
        nowHero.transform.SetParent(transform);
        nowHero.transform.localPosition = Vector3.zero;
        nowHero.transform.localRotation = Quaternion.identity;

        List<MyHerosData> l = MyHeroController.Instance.ReadJson();
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i].heroName == nowHero.GetComponent<Inf>().docName)
            {
                MyHerosData hero = l[i];
                getName.text = nowHero.GetComponent<Inf>().heroName;
                attack.text = hero.attack.ToString("0");
                life.text = hero.maxLife.ToString("0");
                reblue.text = hero.reblue.ToString("f2");
                takeblood.text = hero.bloodTake.ToString("f3");
                speedui.text = hero.speed.ToString("f2");
                aspeed.text = hero.attackSpeed.ToString("f2");
                butime.text = hero.startTime.ToString("f2");
                follow.text = hero.followRange.ToString("f2");
                attrange.text = hero.attackRange.ToString("f2");
                sources.sprite = nowHero.GetComponent<Inf>().mypic;
                des.text = nowHero.GetComponent<Inf>().describes;
                switch (hero.level)
                {
                    case 0:
                        level.text = "R";
                        level.color = Color.blue;
                        break;
                    case 1:
                        level.text = "SR";
                        level.color = Color.red;
                        break;
                    case 2:
                        level.text = "SSR";
                        level.color = Color.yellow;
                        break;
                }
            }
        }
    }
    public void nextHero()
    {
        nowShow++;
        nowShow = nowShow % herosShow.Count;
        showAHero();
    }
    public void lastHero()
    {
        nowShow--;
        if (nowShow < 0)
        {
            nowShow += herosShow.Count;
        }
        nowShow = nowShow % herosShow.Count;
        showAHero();
    }
    //接受鼠标按下的事件
    void OnMouseDown()
    {
        X = 0f;
        //Y = 0f;   
    }

    //鼠标拖拽时的操作
    void OnMouseDrag()
    {
//      Debug.Log("鼠标拖拽");
        onDrag = true;
        X = -Input.GetAxis("Mouse X");
        //获得鼠标增量 
        //Y = Input.GetAxis ("Mouse Y"); 
        //mXY = Mathf.Sqrt (X * X + Y * Y);
        //计算鼠标移动的长度
        // if(mXY == 0f){ mXY=1f;         }     }  

        //计算鼠标移动的长度//
        mXY = Mathf.Sqrt(X * X);
        if (mXY == 0f)
        {
            mXY = 1f;
        }
    }

    //获取阻尼速度 
    float RiSpeed()
    {
        if (onDrag)
        {
            zSpeed = speed;
        }
        else
        {
            //if (zSpeed> 0) 
            //{ 
            //通过除以鼠标移动长度实现拖拽越长速度减缓越慢 
            //  zSpeed -= speed*2 * Time.deltaTime / mXY; 
            //} 
            //else 
            //{ 
            zSpeed = 0;
            //}        
        }
        return zSpeed;
    }

    void LateUpdate()
    {
        transform.Rotate(new Vector3(0, X, 0) * RiSpeed(), Space.World);
        if (!Input.GetMouseButtonDown(0))
        {
            onDrag = false;
        }
    }

}
