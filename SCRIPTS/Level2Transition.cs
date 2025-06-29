using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Transition : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Quit()
    {
        Application.Quit();
    }
}