using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class MyHeroController : MonoBehaviour
{
    string jsonpath;
    public bool isnull;
    public static MyHeroController Instance
    {
        get
        {
            return _instance;
        }
    }
    static MyHeroController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        isnull = false;
    }
    public void getData(string names)
    {
        jsonpath = Application.dataPath + @"/datas/" + names + ".json";
        if (!File.Exists(jsonpath))
        {
            isnull = true;
            StreamWriter sw = new StreamWriter(jsonpath);
            sw.Write(jsonpath);
            sw.Close();
            GetInf.Instance.DataChange();
        }
        GetInf.Instance.GetInfInPrefab();
    }
    public void WriteJson(HeroData inf)
    {
        MyHerosObj jsondata = new MyHerosObj();
        jsondata.myplayerdata = new List<MyHerosData>();

        string jsonRead = File.ReadAllText(jsonpath);
        MyHerosObj jsondataRead = new MyHerosObj();
        if (!isnull)
        {
            jsondataRead = JsonUtility.FromJson<MyHerosObj>(jsonRead);
            if (jsondataRead != null)
            {
                for (int i = 0; i < jsondataRead.myplayerdata.Count; i++)
                {
                    if (inf.heroname != jsondataRead.myplayerdata[i].heroName)
                    {
                        jsondata.myplayerdata.Add(jsondataRead.myplayerdata[i]);
                    }
                }
            }
        }        
        MyHerosData playdata = new MyHerosData();

        playdata.heroName = inf.heroname;
        playdata.maxLife = inf.maxLife;
        playdata.attack = inf.attack;
        playdata.reblue = inf.reBlue;
        playdata.bloodTake = inf.bloodTake;
        playdata.speed = inf.speed;
        playdata.attackSpeed = inf.attackSpeed;
        playdata.startTime = inf.startTime;
        playdata.followRange = inf.followRange;
        playdata.attackRange = inf.attackRange;
        playdata.level = 0;

        jsondata.myplayerdata.Add(playdata);


        string json = JsonUtility.ToJson(jsondata, true);

        File.WriteAllText(jsonpath, json);
        isnull = false;
    }
    public List<MyHerosData> ReadJson()
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }

        string json = File.ReadAllText(jsonpath);
        MyHerosObj jsondata = new MyHerosObj();
        jsondata = JsonUtility.FromJson<MyHerosObj>(json);
        return jsondata.myplayerdata;
    }
    public void OverrideWrite(HeroData inf)
    {
        MyHerosObj jsondata = new MyHerosObj();
        jsondata.myplayerdata = new List<MyHerosData>();

        string jsonRead = File.ReadAllText(jsonpath);
        MyHerosObj jsondataRead = new MyHerosObj();
        jsondataRead = JsonUtility.FromJson<MyHerosObj>(jsonRead);
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
                    MyHerosData playdata = new MyHerosData();

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
                    playdata.level = inf.level;

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
public class MyHerosData
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
    public int level;
}
public class MyHerosObj
{
    public List<MyHerosData> myplayerdata;
}
