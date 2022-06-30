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
        Debug.Log("Not Implemented");
    }

    public void Demo()
    {
        SceneManager.LoadScene("demo_full_isles");
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
