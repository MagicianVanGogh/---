using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetInf : MonoBehaviour
{
    public List<GameObject> prefabs;
    public bool notGo;
    public bool login;
    public string loginname;
    public static GetInf Instance
    {
        get
        {
            return _instance;
        }
    }
    static GetInf _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        login = false;
    }
    private void Start()
    {
        if(notGo)
        DontDestroyOnLoad(this.gameObject);       
    }
    public void DataChange()
    {
        List<HerosData> herodata = HeroPriController.Instance.ReadJson();

        PlayerPrefs.SetInt(loginname + "baodi", 0);
        PlayerPrefs.SetInt(loginname + "card", 11);
        for (int i = 0; i < herodata.Count; i++)
        {
            HeroData h = new HeroData();
            h.heroname = herodata[i].heroName;
            h.maxLife = herodata[i].maxLife;
            h.attack = herodata[i].attack;
            h.reBlue = herodata[i].reblue;
            h.bloodTake = herodata[i].bloodTake;
            h.speed = herodata[i].speed;
            h.attackSpeed = herodata[i].attackSpeed;
            h.startTime = herodata[i].startTime;
            h.followRange = herodata[i].followRange;
            h.attackRange = herodata[i].attackRange;
            MyHeroController.Instance.WriteJson(h);
        }
    }
    public void GetInfInPrefab()
    {
        login = true;
        List<MyHerosData> mydata = MyHeroController.Instance.ReadJson();
        for (int i = 0; i < mydata.Count; i++)
        {
            for (int j = 0; j < prefabs.Count; j++)
            {
                if (prefabs[j].name == mydata[i].heroName)
                {
                    InfSet(prefabs[j].GetComponent<HeroData>(), mydata[i]);
                    break;
                }
            }
        }
    }
    public void InfSet(HeroData h,MyHerosData m)
    {
        h.heroname = m.heroName;
        h.maxLife = m.maxLife;
        h.attack = m.attack;
        h.reBlue = m.reblue;
        h.bloodTake = m.bloodTake;
        h.speed = m.speed;
        h.attackSpeed = m.attackSpeed;
        h.startTime = m.startTime;
        h.followRange = m.followRange;
        h.attackRange = m.attackRange;
        h.level = m.level;
    }
    public void ChangeS(string names)
    {
        SceneManager.LoadScene(names);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
