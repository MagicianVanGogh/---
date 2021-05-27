using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string playerName;
    public string backStory;
    public float health;
    public float damage;

    public float weaponDamage1, weaponDamage2;

    public string shoeName;
    public int shoeSize;
    public string shoeType;

    private void Start()
    {
        health = 50;
    }
}
