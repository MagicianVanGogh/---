using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeS : MonoBehaviour
{
    public void ChangeS(string names)
    {
        SceneManager.LoadScene(names);
    }
}
