using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadGame()
    {
        Debug.Log("Not Implemented");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Isle 1 (Neutral)");
    }

    public void Demo()
    {
        Debug.Log("Not Implemented");
    }

    public void Options()
    {
        Debug.Log("Not Implemented");
    }

    public void Quit() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
