using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoukaManager : MonoBehaviour
{
    public Slider baodi;
    public Text numCard;
    public Text test;

    public Text showResult;

    public List<Inf> getList;
    public List<Inf> getMy;
    public List<HeroData> heroList;
    public Button exitb;

    public List<GameObject> platform;
    bool isCreat = false;

    GameObject created;
    public List<GameObject> createdTen;

    public string username;
 
    private void Start()
    {
        username = GetInf.Instance.loginname;
        baodi.value = PlayerPrefs.GetInt(username+"baodi");
        numCard.text = PlayerPrefs.GetInt(username + "card").ToString();
        test.text = baodi.value.ToString() + " / 100";
        Time.timeScale = 1;
    }
    public void GetOneHero()
    {
        if (PlayerPrefs.GetInt(username + "card") >= 1)
        {
            exitb.interactable = false;
            if (!isCreat)
            {
                StartCoroutine(EffectOne(platform[0]));
                PlayerPrefs.SetInt(username + "card", PlayerPrefs.GetInt(username + "card") - 1);
                numCard.text = PlayerPrefs.GetInt(username + "card").ToString();
            }
        }                    
    }
    public void GetTenHero()
    {
        if (PlayerPrefs.GetInt(username + "card") >= 10)
        {
            exitb.interactable = false;
            if (!isCreat)
            {
                StartCoroutine(EffectTen());
                PlayerPrefs.SetInt(username + "card", PlayerPrefs.GetInt(username + "card") - 10);
                numCard.text = PlayerPrefs.GetInt(username + "card").ToString();
            }
        }       
    }
    public void SetBaodi(bool isAdd)
    {
        if (isAdd)
        {
            baodi.value++;
            PlayerPrefs.SetInt(username + "baodi", (int)baodi.value);
        }
        else
        {
            baodi.value = 0;
            PlayerPrefs.SetInt(username + "baodi", 0);
        }
        test.text = baodi.value.ToString() + " / 100";
    }
    IEnumerator EffectOne(GameObject plat)
    {
        if (createdTen.Count > 0)
        {
            for (int i = 0; i < createdTen.Count; i++)
            {
                Destroy(createdTen[i]);
            }
            createdTen.Clear();
        }
        if (created != null)
        {
            Destroy(created);
        }
        isCreat = true;
        float level = Random.Range(0f, 100);//0-R,1-SR,2-SSR
        if (baodi.value == 100)
        {
            level = 0;
        }
        int getLevel = 0;
        if (level <= 1)
        {
            getLevel = 2;
            SetBaodi(false);
        }
        else if (level <= 9)
        {
            getLevel = 1;
            SetBaodi(true);
        }
        else
        {
            getLevel = 0;
            SetBaodi(true);
        }
        Transform ps = MathUtil1.findChild(plat.transform, "lusueffect1");
        ps.gameObject.SetActive(true);
        GetEffect ge = ps.GetComponent<GetEffect>();
        switch (getLevel)
        {
            case 0:
                ge.p1.startColor = Color.blue;
                ge.p2.startColor = Color.blue;
                ge.p3.startColor = Color.blue;
                ge.p4.startColor = Color.blue;
                ge.p5.startColor = Color.blue;
                break;
            case 1:
                ge.p1.startColor = Color.red;
                ge.p2.startColor = Color.red;
                ge.p3.startColor = Color.red;
                ge.p4.startColor = Color.red;
                ge.p5.startColor = Color.red;
                break;
            case 2:
                ge.p1.startColor = Color.yellow;
                ge.p2.startColor = Color.yellow;
                ge.p3.startColor = Color.yellow;
                ge.p4.startColor = Color.yellow;
                ge.p5.startColor = Color.yellow;
                break;
        }
        
        yield return new WaitForSeconds(0.5f);
        int heroChoose = Random.Range(0, getList.Count);
        Vector3 pos = plat.transform.position;
        pos.x -= 1;
        pos.z -= 1;
        GameObject newOne = Instantiate(getList[heroChoose].gameObject, pos, new Quaternion(0, 0.7071f, 0, -0.7071f));
        Inf inf = newOne.GetComponent<Inf>();
        inf.level = getLevel;
        created = newOne;
        yield return new WaitForSeconds(0.5f);
        EditData(inf);
        showResult.text = inf.AppearString();
        isCreat = false;
        exitb.interactable = true;
    }
    int[] heros;
    int []levels;
    IEnumerator EffectTen()
    {
        if (created != null)
        {
            Destroy(created);
        }
        if (createdTen.Count > 0)
        {
            for (int i = 0; i < createdTen.Count; i++)
            {
                Destroy(createdTen[i]);
            }
            createdTen.Clear();
        }
        isCreat = true;
        heros = new int[10];
        levels = new int[10];
        bool noSR = true;
        for (int i = 0; i < 10; i++)
        {
            float level = Random.Range(0f, 100);//0-R,1-SR,2-SSR
            if (baodi.value == 100)
            {
                level = 0;
            }
            if (level <= 1)
            {
                levels[i] = 2;
                noSR = false;
                SetBaodi(false);
            }
            else if (level <= 9)
            {
                levels[i] = 1;
                noSR = false;
                SetBaodi(true);
            }
            else
            {
                levels[i] = 0;
                if (i == 9 && noSR)
                {
                    levels[i] = 1;
                }                
                SetBaodi(true);
            }
            heros[i] = Random.Range(0, getList.Count);
        }
        for (int i = 0; i < 10; i++)
        {
            Transform ps = MathUtil1.findChild(platform[i].transform, "lusueffect1");
            ps.gameObject.SetActive(true);
            GetEffect ge = ps.GetComponent<GetEffect>();
            switch (levels[i])
            {
                case 0:
                    ge.p1.startColor = Color.blue;
                    ge.p2.startColor = Color.blue;
                    ge.p3.startColor = Color.blue;
                    ge.p4.startColor = Color.blue;
                    ge.p5.startColor = Color.blue;
                    break;
                case 1:
                    ge.p1.startColor = Color.red;
                    ge.p2.startColor = Color.red;
                    ge.p3.startColor = Color.red;
                    ge.p4.startColor = Color.red;
                    ge.p5.startColor = Color.red;
                    break;
                case 2:
                    ge.p1.startColor = Color.yellow;
                    ge.p2.startColor = Color.yellow;
                    ge.p3.startColor = Color.yellow;
                    ge.p4.startColor = Color.yellow;
                    ge.p5.startColor = Color.yellow;
                    break;
            }
        }
        yield return new WaitForSeconds(0.5f);
        showResult.text = "";
        for (int i = 0; i < 10; i++)
        {
            int heroChoose = Random.Range(0, getList.Count);
            Vector3 pos = platform[i].transform.position;
            pos.x -= 1;
            pos.z -= 1;
            GameObject newOne = Instantiate(getList[heroChoose].gameObject, pos, new Quaternion(0, 0.7071f, 0, -0.7071f));
            Inf inf = newOne.GetComponent<Inf>();
            inf.level = levels[i];
            createdTen.Add(newOne);
            EditData(inf);
            showResult.text += inf.AppearString();
        }
        yield return new WaitForSeconds(0.5f);
        isCreat = false;
        exitb.interactable = true;
    }

    public void EditData(Inf inf)
    {
        HeroData nowchoose = null;
        for (int i = 0; i < heroList.Count; i++)
        {
            if (inf.docName == heroList[i].name)
            {
                nowchoose = heroList[i];
                break;
            }
        }
        if (nowchoose.level < inf.level)
        {
            nowchoose.level = inf.level;
        }
        else
        {
            switch (inf.level)
            {
                case 0:
                    int chooseA = Random.Range(0, 2);
                    if (chooseA == 1)
                    {
                        nowchoose.attack++;
                        //Debug.Log("攻击+1");
                    }
                    else
                    {
                        nowchoose.maxLife += 2;
                        //Debug.Log("生命+2");
                    }
                    break;
                case 1:
                    int chooseB = Random.Range(0, 3);
                    if (chooseB == 1)
                    {
                        nowchoose.attack += 2;
                        //Debug.Log("攻击+2");
                    }
                    else if (chooseB == 2)
                    {
                        nowchoose.maxLife += 3;
                        //Debug.Log("生命+3");
                    }
                    else
                    {
                        nowchoose.followRange += 0.1f;
                        //Debug.Log("范围+0.1");
                    }
                    break;
                case 2:
                    int chooseC = Random.Range(0, 5);
                    if (chooseC == 1)
                    {
                        nowchoose.attack += 4;
                        //Debug.Log("攻击+4");
                    }
                    else if (chooseC == 2)
                    {
                        nowchoose.maxLife += 8;
                        //Debug.Log("生命+8");
                    }
                    else if (chooseC == 3)
                    {
                        nowchoose.followRange += 0.2f;
                        //Debug.Log("范围+0.2");
                    }
                    else if (chooseC == 4)
                    {
                        nowchoose.attackSpeed += 0.1f;
                        //Debug.Log("攻速+0.1");
                    }
                    else
                    {
                        nowchoose.bloodTake += 0.01f;
                        //Debug.Log("吸血+0.01");
                    }
                    break;
            }
        }
        MyHeroController.Instance.OverrideWrite(nowchoose);
    }
}
