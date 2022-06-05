using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour 
{
    private bool _active = false;
    public List<string>InventoryObjects;

    private void Awake()
    {   
        Events.OnCatchCrystal.AddListener(OnCollectCrystal);
        Events. OnCatchHealthCrystal.AddListener(OnCollectHealth);

        gameObject.SetActive(false);
    }

    public void OnCollectCrystal() {
        InventoryObjects.Add("Crystal");
    }

    public void OnCollectHealth() {
        InventoryObjects.Add("Health");
    }

    public void OnToggleInventory() 
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
}