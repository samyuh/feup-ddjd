using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour 
{
    private bool _active = false;

    public void OnToggleInventory() 
    {
        _active = !_active;
        Cursor.lockState = _active ? CursorLockMode.None : CursorLockMode.Locked;
        gameObject.SetActive(_active);
    }
}