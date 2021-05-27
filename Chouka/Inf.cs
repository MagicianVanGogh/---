using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inf : MonoBehaviour
{
    public string docName;
    public string heroName;
    public int level;
    public string getString;
    public Sprite mypic;
    public string describes;


    string red = "<color=#FF0000>";
    string yellow = "<color=#FFF600>";
    string blue = "<color=#0000FF>";
    string end = "</color>";
    public string AppearString()
    {
        switch (level)
        {
            case 0:
                getString = blue + heroName + "  R" + end + "\n"; 
                break;
            case 1:
                getString = red + heroName + "  SR" + end + "\n";
                break;
            case 2:
                getString = yellow + heroName + "  SSR" + end + "\n";
                break;
        }
        return getString;
    }
}
