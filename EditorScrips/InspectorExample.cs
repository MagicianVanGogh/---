using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InspectorExample : MonoBehaviour
{
    public List<Character> characters;
    private void Start()
    {
        Invoke("Test",5);
    }
    public void Test()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            print(characters[i].name);
        }
    }
}
[Serializable]
public class Character
{
    [SerializeField]
    Texture icon;

    [SerializeField]
    public string name;

    [SerializeField]
    int hp;

    [SerializeField]
    int power;

    [SerializeField]
    GameObject weapon;
}
