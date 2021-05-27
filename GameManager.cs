using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int cost;//当前的能量
    public GameObject winGam;
    public GameObject loseGam;
    public Text costText;

    public GameObject vicUI;
    public GameObject loseUI;

    public Text getCard;
    public string username;

    public List<HeroData> prefabs;
    bool isover;

    public float times;//生成能量的频率
    string cons = "能量：";

    public static GameManager instance;

    public static GameManager Instance { get => instance;}
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        cost = 0;
        costText.text = cons + cost;
        isover = false;
        username = GetInf.Instance.loginname;
        StartCoroutine(ChangeCost());
        
    }
    void Update()
    {
        if (!isover)
        {
            if (winGam == null)
            {
                Time.timeScale = 0;
                loseUI.SetActive(true);
                isover = true;
            }
            else if (loseGam == null)
            {
                Time.timeScale = 0;
                vicUI.SetActive(true);
                int gets = Random.Range(8, 17);
                if (gets == 10)
                {
                    int move = Random.Range(0, 10);
                    if (move <= 4)
                    {
                        gets--;
                    }
                    else if (move >= 6)
                    {
                        gets++;
                    }
                }
                getCard.text = gets.ToString();
                PlayerPrefs.SetInt(username+"card", PlayerPrefs.GetInt(username+"card") + gets);
                
                isover = true;
            }
        }
        
    }

    IEnumerator ChangeCost()
    {
        while (true)
        {
            yield return new WaitForSeconds(times);
            cost++;
            costText.text = cons + cost;
        }       
    }
    
    public bool UseCost(int num)
    {
        if (cost + num >= 0)
        {
            cost = cost + num;
            costText.text = cons + cost;
            return true;
        }
        else
        {
            return false;
        }
    }
    /* Ban Cost Method
     * public bool UseCost(int num, bool isCheck)//Test
    {
        return true;
        
        if (cost + num >= 0)
        {
            if (!isCheck)
            {
                cost = cost + num;
                costText.text = cons + cost;
            }
            return true;
        }
        else
        {
            return false;
        }
    }*/
    public void Victory()
    {
        vicUI.SetActive(true);
        Time.timeScale = 0;
    }
}
