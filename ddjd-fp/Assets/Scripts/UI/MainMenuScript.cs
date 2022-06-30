using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject menu;

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
        options.SetActive(true);
        menu.SetActive(false);
    }

    public void Quit() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
