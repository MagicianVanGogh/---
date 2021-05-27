using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    public List<HeroData> allHeros;
    public void WriteInJson()
    {
        for (int i = 0; i < allHeros.Count; i++)
        {
            MyHeroController.Instance.WriteJson(allHeros[i]);
        }
    }
    public void ClearAllData()
    {
        MyHeroController.Instance.ClearDatas();
    }
}
