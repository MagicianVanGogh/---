using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class HeroPriController : MonoBehaviour
{

    string jsonpath;
    public static HeroPriController Instance
    {
        get
        {
            return _instance;
        }
    }
    static HeroPriController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        jsonpath = Application.dataPath + @"/datas/Hero.json";
        if (!File.Exists(jsonpath))
        {
            Debug.Log("NO");
        }
    }
    public void WriteJson(HeroData inf)
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }
        HerosObj jsondata = new HerosObj();
        jsondata.myplayerdata = new List<HerosData>();

        string jsonRead = File.ReadAllText(jsonpath);
        HerosObj jsondataRead = new HerosObj();
        jsondataRead = JsonUtility.FromJson<HerosObj>(jsonRead);
        if (jsondataRead != null)
        {
            for (int i = 0; i < jsondataRead.myplayerdata.Count; i++)
            {
                if (jsondataRead.myplayerdata[i].heroName != inf.name)
                {
                    jsondata.myplayerdata.Add(jsondataRead.myplayerdata[i]);
                }               
            }
        }
        HerosData playdata = new HerosData();
        
        playdata.heroName = inf.name;
        playdata.maxLife = inf.maxLife;
        playdata.attack = inf.attack;
        playdata.reblue = inf.reBlue;
        playdata.bloodTake = inf.bloodTake;
        playdata.speed = inf.speed;
        playdata.attackSpeed = inf.attackSpeed;
        playdata.startTime = inf.startTime;
        playdata.followRange = inf.followRange;
        playdata.attackRange = inf.attackRange;

    jsondata.myplayerdata.Add(playdata);


        string json = JsonUtility.ToJson(jsondata, true);

        File.WriteAllText(jsonpath, json);
    }
    public List<HerosData> ReadJson()
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }
        string json = File.ReadAllText(jsonpath);
        HerosObj jsondata = new HerosObj();
        jsondata = JsonUtility.FromJson<HerosObj>(json);
        return jsondata.myplayerdata;
    }
    public void OverrideWrite(HeroData inf)
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }
        HerosObj jsondata = new HerosObj();
        jsondata.myplayerdata = new List<HerosData>();

        string jsonRead = File.ReadAllText(jsonpath);
        HerosObj jsondataRead = new HerosObj();
        jsondataRead = JsonUtility.FromJson<HerosObj>(jsonRead);
        if (jsondataRead != null)
        {
            for (int i = 0; i < jsondataRead.myplayerdata.Count; i++)
            {
                if (inf.name != jsondataRead.myplayerdata[i].heroName)
                {
                    jsondata.myplayerdata.Add(jsondataRead.myplayerdata[i]);
                }
                else
                {
                    HerosData playdata = new HerosData();

                    playdata.heroName = inf.name;
                    playdata.maxLife = inf.maxLife;
                    playdata.attack = inf.attack;
                    playdata.reblue = inf.reBlue;
                    playdata.bloodTake = inf.bloodTake;
                    playdata.speed = inf.speed;
                    playdata.attackSpeed = inf.attackSpeed;
                    playdata.startTime = inf.startTime;
                    playdata.followRange = inf.followRange;
                    playdata.attackRange = inf.attackRange;

                    jsondata.myplayerdata.Add(playdata);
                }
            }
        }



        string json = JsonUtility.ToJson(jsondata, true);

        File.WriteAllText(jsonpath, json);
    }

    public void ClearDatas()
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }
        File.Delete(jsonpath);
    }
}
[Serializable]
public class HerosData
{
    public string heroName;
    public float maxLife;
    public float attack;
    public float reblue;
    public float bloodTake;
    public float speed;
    public float attackSpeed;
    public float startTime;
    public float followRange;
    public float attackRange;
}
public class HerosObj
{
    public List<HerosData> myplayerdata;
}
