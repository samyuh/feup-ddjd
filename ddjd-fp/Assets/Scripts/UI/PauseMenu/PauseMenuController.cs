using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController: MonoBehaviour 
{
    private bool _active = false;
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject Items;

    public void OnTogglePauseMenu() 
    {
        _active = !_active;
        Cursor.lockState = _active ? CursorLockMode.None : CursorLockMode.Locked;
        gameObject.SetActive(_active);

        if(_active) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}